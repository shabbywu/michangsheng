using System;
using System.Collections;
using System.Collections.Generic;
using Bag;
using JSONClass;
using KBEngine;
using SuperScrollView;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Tab;

[Serializable]
public class TabBag : UIBase
{
	private Avatar _player;

	private BagType _bagType;

	public Bag.ItemType ItemType;

	public ItemQuality ItemQuality;

	public LianQiCaiLiaoYinYang LianQiCaiLiaoYinYang;

	public LianQiCaiLiaoType LianQiCaiLiaoType;

	public SkIllType SkIllType = SkIllType.全部;

	public SkillQuality SkillQuality;

	public StaticSkIllType StaticSkIllType = StaticSkIllType.全部;

	public BagFilter BagFilter;

	public Text MoneyText;

	public Image MoneyIcon;

	public GameObject UtilsPanel;

	public List<ITEM_INFO> ItemList = new List<ITEM_INFO>();

	public List<SkillItem> ActiveSkillList = new List<SkillItem>();

	public List<SkillItem> PassiveSkillList = new List<SkillItem>();

	public bool CanSort;

	public LoopListView2 mLoopListView;

	public List<ISlot> SlotList = new List<ISlot>();

	private bool _isInit;

	private const int mItemCountPerRow = 5;

	private int mItemTotalCount;

	public TabBag(GameObject go)
	{
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Expected O, but got Unknown
		_go = go;
		mLoopListView = Get<LoopListView2>("ItemList");
		BagFilter = go.GetComponent<BagFilter>();
		Get<FpBtn>("工具/ZhengLiBtn").mouseUpEvent.AddListener((UnityAction)delegate
		{
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Expected O, but got Unknown
			BagFilter.Sort(new UnityAction(UpdateItem));
		});
		_player = Tools.instance.getPlayer();
		CanSort = true;
	}

	private void Init()
	{
		_isInit = true;
		MoneyText = Get<Text>("工具/MoneyText");
		MoneyIcon = Get<Image>("工具/MoneyText/MoneyIcon");
		UtilsPanel = Get("工具");
		mLoopListView.InitListView(GetCount(mItemTotalCount), OnGetItemByIndex);
	}

	public void OpenBag(BagType bagType)
	{
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Expected O, but got Unknown
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Expected O, but got Unknown
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Expected O, but got Unknown
		//IL_0228: Expected O, but got Unknown
		//IL_01ee: Expected O, but got Unknown
		ItemType = Bag.ItemType.全部;
		ItemQuality = ItemQuality.全部;
		LianQiCaiLiaoYinYang = LianQiCaiLiaoYinYang.全部;
		LianQiCaiLiaoType = LianQiCaiLiaoType.全部;
		_bagType = bagType;
		switch (bagType)
		{
		case BagType.功法:
			PassiveSkillList = new List<SkillItem>(_player.hasStaticSkillList);
			mItemTotalCount = PassiveSkillList.Count;
			break;
		case BagType.技能:
			ActiveSkillList = new List<SkillItem>(_player.hasSkillList);
			mItemTotalCount = ActiveSkillList.Count;
			break;
		case BagType.背包:
			ItemList = new List<ITEM_INFO>(_player.itemList.values);
			mItemTotalCount = ItemList.Count;
			break;
		}
		SlotList = new List<ISlot>();
		if (!_isInit)
		{
			Init();
			MessageMag.Instance.Register(MessageName.MSG_PLAYER_USE_ITEM, UseItemCallBack);
			UpdateMoney();
		}
		else
		{
			UpdateItem();
		}
		switch (bagType)
		{
		case BagType.背包:
			BagFilter.AddBigTypeBtn((UnityAction)delegate
			{
				//IL_0042: Unknown result type (might be due to invalid IL or missing references)
				//IL_005d: Expected O, but got Unknown
				foreach (ItemQuality itemQuality in Enum.GetValues(typeof(ItemQuality)))
				{
					BagFilter.AddSmallTypeBtn((UnityAction)delegate
					{
						ItemQuality = itemQuality;
						BagFilter.CloseSmallSelect();
						UpdateItem();
					}, itemQuality.ToString());
				}
			}, (ItemQuality == ItemQuality.全部) ? "品阶" : ItemQuality.ToString());
			BagFilter.AddBigTypeBtn((UnityAction)delegate
			{
				//IL_0042: Unknown result type (might be due to invalid IL or missing references)
				//IL_005d: Expected O, but got Unknown
				foreach (Bag.ItemType itemType in Enum.GetValues(typeof(Bag.ItemType)))
				{
					BagFilter.AddSmallTypeBtn((UnityAction)delegate
					{
						//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
						//IL_00d9: Expected O, but got Unknown
						//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
						//IL_0124: Expected O, but got Unknown
						ItemType = itemType;
						BagFilter.CloseSmallSelect();
						UpdateItem();
						if (itemType == Bag.ItemType.材料)
						{
							if (BagFilter.BigTypeIndex >= 3)
							{
								((Component)BagFilter.BigFilterBtnList[2]).gameObject.SetActive(true);
								((Component)BagFilter.BigFilterBtnList[3]).gameObject.SetActive(true);
							}
							else
							{
								BagFilter.AddBigTypeBtn((UnityAction)delegate
								{
									//IL_0042: Unknown result type (might be due to invalid IL or missing references)
									//IL_005d: Expected O, but got Unknown
									IEnumerator enumerator3 = Enum.GetValues(typeof(LianQiCaiLiaoYinYang)).GetEnumerator();
									try
									{
										while (enumerator3.MoveNext())
										{
											TabBag tabBag2 = this;
											LianQiCaiLiaoYinYang yinYang = (LianQiCaiLiaoYinYang)enumerator3.Current;
											BagFilter.AddSmallTypeBtn((UnityAction)delegate
											{
												tabBag2.LianQiCaiLiaoYinYang = yinYang;
												tabBag2.BagFilter.CloseSmallSelect();
												tabBag2.UpdateItem();
											}, yinYang.ToString());
										}
									}
									finally
									{
										IDisposable disposable = enumerator3 as IDisposable;
										if (disposable != null)
										{
											disposable.Dispose();
										}
									}
								}, (LianQiCaiLiaoYinYang == LianQiCaiLiaoYinYang.全部) ? "阴阳" : LianQiCaiLiaoYinYang.ToString());
								BagFilter.AddBigTypeBtn((UnityAction)delegate
								{
									//IL_0042: Unknown result type (might be due to invalid IL or missing references)
									//IL_005d: Expected O, but got Unknown
									IEnumerator enumerator2 = Enum.GetValues(typeof(LianQiCaiLiaoType)).GetEnumerator();
									try
									{
										while (enumerator2.MoveNext())
										{
											TabBag tabBag = this;
											LianQiCaiLiaoType lianQiCaiLiaoType = (LianQiCaiLiaoType)enumerator2.Current;
											BagFilter.AddSmallTypeBtn((UnityAction)delegate
											{
												tabBag.LianQiCaiLiaoType = lianQiCaiLiaoType;
												tabBag.BagFilter.CloseSmallSelect();
												tabBag.UpdateItem();
											}, lianQiCaiLiaoType.ToString());
										}
									}
									finally
									{
										IDisposable disposable2 = enumerator2 as IDisposable;
										if (disposable2 != null)
										{
											disposable2.Dispose();
										}
									}
								}, (LianQiCaiLiaoType == LianQiCaiLiaoType.全部) ? "属性" : LianQiCaiLiaoType.ToString());
							}
						}
						else if (BagFilter.BigTypeIndex >= 2)
						{
							((Component)BagFilter.BigFilterBtnList[2]).gameObject.SetActive(false);
							((Component)BagFilter.BigFilterBtnList[3]).gameObject.SetActive(false);
						}
					}, itemType.ToString());
				}
			}, (ItemType == Bag.ItemType.全部) ? "类型" : ItemType.ToString());
			break;
		case BagType.功法:
		case BagType.技能:
			BagFilter.AddBigTypeBtn((UnityAction)delegate
			{
				//IL_0042: Unknown result type (might be due to invalid IL or missing references)
				//IL_005d: Expected O, but got Unknown
				foreach (SkillQuality skillQuality in Enum.GetValues(typeof(SkillQuality)))
				{
					BagFilter.AddSmallTypeBtn((UnityAction)delegate
					{
						SkillQuality = skillQuality;
						BagFilter.CloseSmallSelect();
						UpdateItem();
					}, skillQuality.ToString());
				}
			}, (SkillQuality == SkillQuality.全部) ? "品阶" : SkillQuality.ToString());
			if (bagType == BagType.功法)
			{
				BagFilter.AddBigTypeBtn((UnityAction)delegate
				{
					//IL_008b: Unknown result type (might be due to invalid IL or missing references)
					//IL_00a7: Expected O, but got Unknown
					List<StaticSkIllType> list2 = new List<StaticSkIllType> { StaticSkIllType.全部 };
					foreach (StaticSkIllType value in Enum.GetValues(typeof(SkIllType)))
					{
						if (value != StaticSkIllType.全部)
						{
							list2.Add(value);
						}
					}
					foreach (StaticSkIllType staticSkIllType in list2)
					{
						BagFilter.AddSmallTypeBtn((UnityAction)delegate
						{
							StaticSkIllType = staticSkIllType;
							BagFilter.CloseSmallSelect();
							UpdateItem();
						}, staticSkIllType.ToString());
					}
				}, (StaticSkIllType == StaticSkIllType.全部) ? "属性" : StaticSkIllType.ToString());
				break;
			}
			BagFilter.AddBigTypeBtn((UnityAction)delegate
			{
				//IL_008b: Unknown result type (might be due to invalid IL or missing references)
				//IL_00a7: Expected O, but got Unknown
				List<SkIllType> list = new List<SkIllType> { SkIllType.全部 };
				foreach (SkIllType value2 in Enum.GetValues(typeof(SkIllType)))
				{
					if (value2 != SkIllType.全部)
					{
						list.Add(value2);
					}
				}
				foreach (SkIllType skIllType in list)
				{
					BagFilter.AddSmallTypeBtn((UnityAction)delegate
					{
						SkIllType = skIllType;
						BagFilter.CloseSmallSelect();
						UpdateItem();
					}, skIllType.ToString());
				}
			}, (SkIllType == SkIllType.全部) ? "属性" : SkIllType.ToString());
			break;
		}
		_go.SetActive(true);
		SingletonMono<TabUIMag>.Instance.TabBag.BagFilter.PlayHideAn();
		SingletonMono<TabUIMag>.Instance.TabFangAnPanel.Show();
	}

	public void UpdateMoney()
	{
		if (_bagType == BagType.背包)
		{
			MoneyText.SetText(Tools.instance.getPlayer().money);
			UtilsPanel.gameObject.SetActive(true);
		}
		else
		{
			UtilsPanel.gameObject.SetActive(false);
		}
	}

	public void Close()
	{
		BagFilter.ResetData();
		_go.SetActive(false);
	}

	private LoopListViewItem2 OnGetItemByIndex(LoopListView2 listView, int rowIndex)
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
		for (int i = 0; i < 5; i++)
		{
			int num = rowIndex * 5 + i;
			switch (_bagType)
			{
			case BagType.功法:
				component.mItemList[i].SetAccptType(CanSlotType.功法);
				break;
			case BagType.技能:
				component.mItemList[i].SetAccptType(CanSlotType.技能);
				break;
			case BagType.背包:
				component.mItemList[i].SetAccptType(CanSlotType.全部物品);
				break;
			}
			if (num >= mItemTotalCount)
			{
				component.mItemList[i].SetNull();
				if (!SlotList.Contains((SlotBase)component.mItemList[i]))
				{
					SlotList.Add((SlotBase)component.mItemList[i]);
				}
				continue;
			}
			switch (_bagType)
			{
			case BagType.功法:
			{
				PassiveSkill passiveSkill = new PassiveSkill();
				passiveSkill.SetSkill(PassiveSkillList[num].itemId, PassiveSkillList[num].level);
				component.mItemList[i].SetSlotData(passiveSkill);
				break;
			}
			case BagType.技能:
			{
				ActiveSkill activeSkill = new ActiveSkill();
				activeSkill.SetSkill(ActiveSkillList[num].itemId, Tools.instance.getPlayer().getLevelType());
				component.mItemList[i].SetSlotData(activeSkill);
				break;
			}
			case BagType.背包:
			{
				BaseItem slotData = BaseItem.Create(ItemList[num].itemId, (int)ItemList[num].itemCount, ItemList[num].uuid, ItemList[num].Seid);
				component.mItemList[i].SetSlotData(slotData);
				break;
			}
			}
			if (!SlotList.Contains(component.mItemList[i]))
			{
				SlotList.Add(component.mItemList[i]);
			}
		}
		return loopListViewItem;
	}

	public int GetCount(int itemCout)
	{
		int num = itemCout / 5;
		if (itemCout % 5 > 0)
		{
			num++;
		}
		return num + 1;
	}

	public bool FiddlerItem(BaseItem baseItem)
	{
		if (ItemQuality != 0 && baseItem.GetImgQuality() != (int)ItemQuality)
		{
			return false;
		}
		if (ItemType != 0 && baseItem.ItemType != ItemType)
		{
			return false;
		}
		if (ItemType == Bag.ItemType.材料)
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
		return true;
	}

	public bool FiddlerSkill(BaseSkill baseSkill)
	{
		if (SkillQuality != 0 && baseSkill.GetImgQuality() != (int)SkillQuality)
		{
			return false;
		}
		if (SkIllType != SkIllType.全部 && !baseSkill.SkillTypeIsEqual((int)SkIllType))
		{
			return false;
		}
		return true;
	}

	public bool FiddlerStaticSkill(BaseSkill baseSkill)
	{
		if (SkillQuality != 0 && baseSkill.GetImgQuality() != (int)SkillQuality)
		{
			return false;
		}
		if (StaticSkIllType != StaticSkIllType.全部 && !baseSkill.SkillTypeIsEqual((int)StaticSkIllType))
		{
			return false;
		}
		return true;
	}

	public void UpdateItem()
	{
		if (_bagType == BagType.背包)
		{
			ItemList = new List<ITEM_INFO>();
			foreach (ITEM_INFO value in _player.itemList.values)
			{
				if (_ItemJsonData.DataDict.ContainsKey(value.itemId))
				{
					BaseItem baseItem = BaseItem.Create(value.itemId, (int)value.itemCount, value.uuid, value.Seid);
					if (FiddlerItem(baseItem))
					{
						ItemList.Add(value);
					}
				}
			}
			mItemTotalCount = ItemList.Count;
		}
		else if (_bagType == BagType.技能)
		{
			ActiveSkillList = new List<SkillItem>();
			foreach (SkillItem hasSkill in _player.hasSkillList)
			{
				BaseSkill baseSkill = new ActiveSkill();
				baseSkill.SetSkill(hasSkill.itemId, Tools.instance.getPlayer().getLevelType());
				if (FiddlerSkill(baseSkill))
				{
					ActiveSkillList.Add(hasSkill);
				}
			}
			mItemTotalCount = ActiveSkillList.Count;
		}
		else if (_bagType == BagType.功法)
		{
			PassiveSkillList = new List<SkillItem>();
			foreach (SkillItem hasStaticSkill in _player.hasStaticSkillList)
			{
				BaseSkill baseSkill2 = new PassiveSkill();
				baseSkill2.SetSkill(hasStaticSkill.itemId, hasStaticSkill.level);
				if (FiddlerStaticSkill(baseSkill2))
				{
					PassiveSkillList.Add(hasStaticSkill);
				}
			}
			mItemTotalCount = PassiveSkillList.Count;
		}
		mLoopListView.SetListItemCount(GetCount(mItemTotalCount));
		mLoopListView.RefreshAllShownItem();
		UpdateMoney();
	}

	public BagType GetCurBagType()
	{
		return _bagType;
	}

	public void UpDateSlotList()
	{
		if (_bagType == BagType.背包)
		{
			ItemList = new List<ITEM_INFO>();
			foreach (ITEM_INFO value in _player.itemList.values)
			{
				BaseItem baseItem = BaseItem.Create(value.itemId, (int)value.itemCount, value.uuid, value.Seid);
				if (FiddlerItem(baseItem))
				{
					ItemList.Add(value);
				}
			}
			mItemTotalCount = ItemList.Count;
		}
		else if (_bagType == BagType.技能)
		{
			ActiveSkillList = new List<SkillItem>();
			foreach (SkillItem hasSkill in _player.hasSkillList)
			{
				BaseSkill baseSkill = new ActiveSkill();
				baseSkill.SetSkill(hasSkill.itemId, hasSkill.level);
				if (FiddlerSkill(baseSkill))
				{
					ActiveSkillList.Add(hasSkill);
				}
			}
			mItemTotalCount = ActiveSkillList.Count;
		}
		else if (_bagType == BagType.功法)
		{
			PassiveSkillList = new List<SkillItem>();
			foreach (SkillItem hasStaticSkill in _player.hasStaticSkillList)
			{
				BaseSkill baseSkill2 = new PassiveSkill();
				baseSkill2.SetSkill(hasStaticSkill.itemId, hasStaticSkill.level);
				if (FiddlerStaticSkill(baseSkill2))
				{
					PassiveSkillList.Add(hasStaticSkill);
				}
			}
			mItemTotalCount = PassiveSkillList.Count;
		}
		mLoopListView.SetListItemCount(GetCount(mItemTotalCount), resetPos: false);
		mLoopListView.RefreshAllShownItem();
	}

	public SlotBase GetNullSlot()
	{
		SlotBase slotBase = null;
		foreach (SlotBase slot in SlotList)
		{
			if (((Component)((Component)slot).transform.parent).gameObject.activeSelf && slot.IsNull())
			{
				return slot;
			}
		}
		return null;
	}

	public void UseItemCallBack(MessageData messageData)
	{
		UpDateSlotList();
	}
}
