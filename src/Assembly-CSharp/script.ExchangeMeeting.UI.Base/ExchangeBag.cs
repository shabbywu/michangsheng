using System;
using Bag;
using JSONClass;
using KBEngine;
using UnityEngine;
using script.ExchangeMeeting.UI.Interface;

namespace script.ExchangeMeeting.UI.Base;

public class ExchangeBag : BaseBag2, IESCClose
{
	protected bool _init;

	public ExchangeSlot ToSlot;

	public bool TryEscClose()
	{
		Hide();
		return true;
	}

	public void Open()
	{
		if (!_init)
		{
			Init(0);
			_init = true;
		}
		UpdateItem();
		ESCCloseManager.Inst.RegisterClose(this);
		((Component)this).gameObject.SetActive(true);
	}

	public override void Init(int npcId, bool isPlayer = false)
	{
		_player = Tools.instance.getPlayer();
		ItemType = Bag.ItemType.丹药;
		NpcId = npcId;
		CreateTempList();
		MLoopListView.InitListView(GetCount(MItemTotalCount), base.OnGetItemByIndex);
		ItemType = Bag.ItemType.全部;
		ItemQuality = ItemQuality.全部;
		JiaoYiSkillType = JiaoYiSkillType.全部;
		LianQiCaiLiaoYinYang = LianQiCaiLiaoYinYang.全部;
		LianQiCaiLiaoType = LianQiCaiLiaoType.全部;
		InitLeftFilter();
		UpdateLeftFilter(LeftList[0]);
	}

	public void UpdateLeftFilter(BaseFilterLeft filterLeft)
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

	public override void UpdateTopFilter()
	{
		if (TopList != null && TopList.Count > 0)
		{
			base.UpdateTopFilter();
		}
	}

	public override void CreateTempList()
	{
		if (IsPlayer)
		{
			foreach (ITEM_INFO value in _player.itemList.values)
			{
				if (value.itemCount != 0 && _ItemJsonData.DataDict.ContainsKey(value.itemId) && _ItemJsonData.DataDict[value.itemId].CanSale != 1)
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
			return;
		}
		foreach (int canGetItem in IExchangeUIMag.Inst.GetCanGetItemList())
		{
			try
			{
				ITEM_INFO item = new ITEM_INFO
				{
					itemCount = 1u,
					itemId = canGetItem,
					uuid = Tools.getUUID(),
					Seid = Tools.CreateItemSeid(canGetItem)
				};
				TempBagList.Add(item);
			}
			catch (Exception)
			{
				Debug.LogError((object)("物品id:" + canGetItem + "不存在"));
			}
		}
	}

	public override void OpenType()
	{
		TopList[1].Clear();
		string text = "";
		if (ItemType == Bag.ItemType.材料)
		{
			text = LianQiCaiLiaoYinYang.ToString();
			JiaoYiSkillType = JiaoYiSkillType.全部;
			EquipType = EquipType.全部;
			TopList[1].Init(this, FilterType.类型, text);
		}
	}

	public void Hide()
	{
		ESCCloseManager.Inst.UnRegisterClose(this);
		((Component)this).gameObject.SetActive(false);
	}
}
