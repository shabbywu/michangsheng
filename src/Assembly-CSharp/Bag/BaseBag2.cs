using System.Collections.Generic;
using JSONClass;
using JiaoYi;
using KBEngine;
using SuperScrollView;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Bag;

public class BaseBag2 : MonoBehaviour
{
	public Text Money;

	public int NpcId;

	public bool IsPlayer;

	public List<ITEM_INFO> TempBagList = new List<ITEM_INFO>();

	public List<BaseFilterLeft> LeftList;

	public List<JiaoYiFilterTop> TopList;

	public ItemType ItemType;

	public ItemQuality ItemQuality;

	public JiaoYiSkillType JiaoYiSkillType;

	public SkIllType SkIllType = SkIllType.全部;

	public StaticSkIllType StaticSkIllType = StaticSkIllType.全部;

	public LianQiCaiLiaoYinYang LianQiCaiLiaoYinYang;

	public LianQiCaiLiaoType LianQiCaiLiaoType;

	public EquipType EquipType;

	public int MItemTotalCount;

	public List<ITEM_INFO> ItemList = new List<ITEM_INFO>();

	public LoopListView2 MLoopListView;

	public int mItemCountPerRow = 3;

	protected Avatar _player;

	public virtual void Init(int npcId, bool isPlayer = false)
	{
		_player = Tools.instance.getPlayer();
		NpcId = npcId;
		IsPlayer = isPlayer;
		CreateTempList();
		MLoopListView.InitListView(GetCount(MItemTotalCount), OnGetItemByIndex);
		ItemType = ItemType.全部;
		ItemQuality = ItemQuality.全部;
		JiaoYiSkillType = JiaoYiSkillType.全部;
		LianQiCaiLiaoYinYang = LianQiCaiLiaoYinYang.全部;
		LianQiCaiLiaoType = LianQiCaiLiaoType.全部;
		InitLeftFilter();
		UpdateLeftFilter(LeftList[0]);
	}

	public virtual void CreateTempList()
	{
		TempBagList = new List<ITEM_INFO>();
		List<ITEM_INFO> list = new List<ITEM_INFO>();
		if (IsPlayer)
		{
			foreach (ITEM_INFO value in _player.itemList.values)
			{
				if (value.itemCount == 0)
				{
					list.Add(value);
				}
				else if (_ItemJsonData.DataDict.ContainsKey(value.itemId))
				{
					ITEM_INFO iTEM_INFO = new ITEM_INFO();
					iTEM_INFO.itemId = value.itemId;
					iTEM_INFO.itemCount = value.itemCount;
					iTEM_INFO.uuid = value.uuid;
					if (value.Seid != null)
					{
						iTEM_INFO.Seid = value.Seid.Copy();
					}
					TempBagList.Add(iTEM_INFO);
				}
			}
		}
		else
		{
			JSONObject jSONObject = jsonData.instance.AvatarBackpackJsonData[NpcId.ToString()]["Backpack"];
			if (jSONObject.Count < 1)
			{
				return;
			}
			foreach (JSONObject item in jSONObject.list)
			{
				if (item["Num"].I > 0 && _ItemJsonData.DataDict.ContainsKey(item["ItemID"].I))
				{
					ITEM_INFO iTEM_INFO2 = new ITEM_INFO();
					iTEM_INFO2.itemId = item["ItemID"].I;
					iTEM_INFO2.itemCount = (uint)item["Num"].I;
					iTEM_INFO2.uuid = item["UUID"].Str;
					if (item.HasField("Seid"))
					{
						iTEM_INFO2.Seid = item["Seid"].Copy();
					}
					TempBagList.Add(iTEM_INFO2);
				}
			}
		}
		foreach (ITEM_INFO item2 in list)
		{
			_player.itemList.values.Remove(item2);
		}
	}

	public virtual void UpdateItem(bool flag = false)
	{
		ItemList = new List<ITEM_INFO>();
		foreach (ITEM_INFO tempBag in TempBagList)
		{
			BaseItem baseItem = BaseItem.Create(tempBag.itemId, (int)tempBag.itemCount, tempBag.uuid, tempBag.Seid);
			if (FiddlerItem(baseItem))
			{
				ItemList.Add(tempBag);
			}
		}
		MItemTotalCount = ItemList.Count;
		MLoopListView.SetListItemCount(GetCount(MItemTotalCount), flag);
		MLoopListView.RefreshAllShownItem();
	}

	public void UpdateMoney()
	{
		if (IsPlayer)
		{
			Money.SetText(Tools.instance.getPlayer().money);
		}
		else
		{
			Money.SetText(jsonData.instance.AvatarBackpackJsonData[NpcId.ToString()]["money"].I);
		}
	}

	public int GetMoney()
	{
		if (IsPlayer)
		{
			return (int)Tools.instance.getPlayer().money;
		}
		return jsonData.instance.AvatarBackpackJsonData[NpcId.ToString()]["money"].I;
	}

	public void InitLeftFilter()
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Expected O, but got Unknown
		if (LeftList.Count < 1)
		{
			return;
		}
		foreach (BaseFilterLeft filterLeft in LeftList)
		{
			filterLeft.Add((UnityAction)delegate
			{
				if (!filterLeft.IsSelect)
				{
					UpdateLeftFilter(filterLeft);
				}
			});
		}
	}

	private void UpdateLeftFilter(BaseFilterLeft filterLeft)
	{
		foreach (BaseFilterLeft left in LeftList)
		{
			if ((Object)(object)left == (Object)(object)filterLeft)
			{
				left.IsSelect = true;
				ItemType = left.ItemType;
				UpdateItem();
				UpdateTopFilter();
			}
			else
			{
				left.IsSelect = false;
			}
			left.UpdateState();
		}
	}

	public void CloseAllTopFilter()
	{
		foreach (JiaoYiFilterTop top in TopList)
		{
			top.Select.SetActive(false);
			((Component)top.Unselect).gameObject.SetActive(true);
		}
	}

	public virtual void UpdateTopFilter()
	{
		OpenQuality();
		CloseType();
		CloseShuXing();
		if (ItemType == ItemType.法宝)
		{
			OpenType();
		}
		else if (ItemType == ItemType.材料)
		{
			OpenType();
			OpenShuXing();
		}
		else if (ItemType == ItemType.秘籍)
		{
			OpenType();
		}
	}

	public void OpenQuality()
	{
		TopList[0].Clear();
		string title = ItemQuality.ToString();
		TopList[0].Init(this, FilterType.品阶, title);
	}

	public virtual void OpenType()
	{
		TopList[1].Clear();
		string text = "";
		if (ItemType == ItemType.材料)
		{
			text = LianQiCaiLiaoYinYang.ToString();
			JiaoYiSkillType = JiaoYiSkillType.全部;
			EquipType = EquipType.全部;
			TopList[1].Init(this, FilterType.类型, text);
		}
		else if (ItemType == ItemType.法宝)
		{
			text = EquipType.ToString();
			LianQiCaiLiaoYinYang = LianQiCaiLiaoYinYang.全部;
			JiaoYiSkillType = JiaoYiSkillType.全部;
			TopList[1].Init(this, FilterType.类型, text);
		}
		else if (ItemType == ItemType.秘籍)
		{
			EquipType = EquipType.全部;
			LianQiCaiLiaoYinYang = LianQiCaiLiaoYinYang.全部;
			text = JiaoYiSkillType.ToString();
			TopList[1].Init(this, FilterType.类型, text);
		}
	}

	public void OpenShuXing()
	{
		string title = "";
		if (ItemType == ItemType.材料)
		{
			SkIllType = SkIllType.全部;
			StaticSkIllType = StaticSkIllType.全部;
			title = LianQiCaiLiaoType.ToString();
		}
		else if (JiaoYiSkillType == JiaoYiSkillType.功法)
		{
			SkIllType = SkIllType.全部;
			LianQiCaiLiaoType = LianQiCaiLiaoType.全部;
			title = StaticSkIllType.ToString();
		}
		else if (JiaoYiSkillType == JiaoYiSkillType.神通)
		{
			StaticSkIllType = StaticSkIllType.全部;
			LianQiCaiLiaoType = LianQiCaiLiaoType.全部;
			title = SkIllType.ToString();
		}
		TopList[2].Clear();
		TopList[2].Init(this, FilterType.属性, title);
	}

	public void CloseType()
	{
		TopList[1].Clear();
		JiaoYiSkillType = JiaoYiSkillType.全部;
		EquipType = EquipType.全部;
		LianQiCaiLiaoYinYang = LianQiCaiLiaoYinYang.全部;
	}

	public void CloseShuXing()
	{
		TopList[2].Clear();
		SkIllType = SkIllType.全部;
		StaticSkIllType = StaticSkIllType.全部;
		LianQiCaiLiaoType = LianQiCaiLiaoType.全部;
	}

	protected int GetCount(int itemCout)
	{
		int num = itemCout / mItemCountPerRow;
		if (itemCout % mItemCountPerRow > 0)
		{
			num++;
		}
		return num + 1;
	}

	protected LoopListViewItem2 OnGetItemByIndex(LoopListView2 listView, int rowIndex)
	{
		if (rowIndex < 0)
		{
			return null;
		}
		LoopListViewItem2 loopListViewItem = listView.NewListViewItem("Prefab");
		SlotList component = ((Component)loopListViewItem).GetComponent<SlotList>();
		if (!loopListViewItem.IsInitHandlerCalled)
		{
			loopListViewItem.IsInitHandlerCalled = true;
			component.Init();
		}
		for (int i = 0; i < mItemCountPerRow; i++)
		{
			int num = rowIndex * mItemCountPerRow + i;
			component.mItemList[i].SetAccptType(CanSlotType.全部物品);
			if (num >= MItemTotalCount)
			{
				component.mItemList[i].SetNull();
				continue;
			}
			BaseItem slotData = BaseItem.Create(ItemList[num].itemId, (int)ItemList[num].itemCount, ItemList[num].uuid, ItemList[num].Seid);
			component.mItemList[i].SetSlotData(slotData);
		}
		return loopListViewItem;
	}

	protected virtual bool FiddlerItem(BaseItem baseItem)
	{
		if (ItemQuality != 0 && baseItem.GetImgQuality() != (int)ItemQuality)
		{
			return false;
		}
		if (ItemType != 0 && baseItem.ItemType != ItemType)
		{
			return false;
		}
		if (ItemType == ItemType.材料)
		{
			CaiLiaoItem caiLiaoItem = (CaiLiaoItem)baseItem;
			if (LianQiCaiLiaoYinYang != 0 && caiLiaoItem.GetYinYang() != LianQiCaiLiaoYinYang)
			{
				return false;
			}
			if (LianQiCaiLiaoType != 0 && caiLiaoItem.GetLianQiCaiLiaoType() != LianQiCaiLiaoType)
			{
				return false;
			}
		}
		else if (ItemType == ItemType.法宝)
		{
			EquipItem equipItem = (EquipItem)baseItem;
			if (EquipType != 0 && !equipItem.EquipTypeIsEqual(EquipType))
			{
				return false;
			}
		}
		else if (ItemType == ItemType.秘籍)
		{
			JiaoYiMiJi jiaoYiMiJi = new JiaoYiMiJi();
			jiaoYiMiJi.SetItem(baseItem.Id, baseItem.Count);
			if (JiaoYiSkillType != 0 && jiaoYiMiJi.GetJiaoYiType() != JiaoYiSkillType)
			{
				return false;
			}
			if (JiaoYiSkillType == JiaoYiSkillType.神通)
			{
				if (SkIllType != SkIllType.全部 && !jiaoYiMiJi.SkillTypeIsEqual((int)SkIllType))
				{
					return false;
				}
			}
			else if (JiaoYiSkillType == JiaoYiSkillType.功法 && StaticSkIllType != StaticSkIllType.全部 && !jiaoYiMiJi.SkillTypeIsEqual((int)StaticSkIllType))
			{
				return false;
			}
		}
		return true;
	}

	public void RemoveTempItem(string uuid, int num)
	{
		ITEM_INFO iTEM_INFO = null;
		foreach (ITEM_INFO tempBag in TempBagList)
		{
			if (tempBag.uuid == uuid && tempBag.itemCount >= num)
			{
				tempBag.itemCount -= (uint)num;
				if (tempBag.itemCount == 0)
				{
					iTEM_INFO = tempBag;
				}
				break;
			}
		}
		if (iTEM_INFO != null)
		{
			TempBagList.Remove(iTEM_INFO);
			ItemList.Remove(iTEM_INFO);
			MItemTotalCount = ItemList.Count;
		}
	}

	public void AddTempItem(BaseItem baseItem, int num)
	{
		int num2 = 0;
		foreach (ITEM_INFO tempBag in TempBagList)
		{
			if (tempBag.uuid == baseItem.Uid)
			{
				tempBag.itemCount += (uint)num;
				num2 = 1;
				break;
			}
		}
		if (num2 == 0)
		{
			ITEM_INFO iTEM_INFO = new ITEM_INFO();
			iTEM_INFO.itemId = baseItem.Id;
			iTEM_INFO.itemCount = (uint)num;
			iTEM_INFO.uuid = baseItem.Uid;
			if (baseItem.Seid != null)
			{
				iTEM_INFO.Seid = baseItem.Seid.Copy();
			}
			TempBagList.Add(iTEM_INFO);
			ItemList.Add(iTEM_INFO);
			MItemTotalCount = ItemList.Count;
		}
	}

	public SlotBase GetNullBagSlot(string uuid)
	{
		foreach (LoopListViewItem2 item in MLoopListView.ItemList)
		{
			foreach (SlotBase mItem in ((Component)item).GetComponent<SlotList>().mItemList)
			{
				if (((Component)((Component)mItem).transform.parent).gameObject.activeSelf)
				{
					if (mItem.IsNull())
					{
						return mItem;
					}
					if (mItem.Item.Uid == uuid && mItem.Item.MaxNum > 1)
					{
						return mItem;
					}
				}
			}
		}
		return null;
	}
}
