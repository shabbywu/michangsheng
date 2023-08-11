using System;
using System.Collections;
using System.Collections.Generic;
using KBEngine;
using SuperScrollView;
using Tab;
using UnityEngine;
using UnityEngine.Events;

namespace Bag;

public class BaseBag : MonoBehaviour, IBaseBag
{
	public int MItemTotalCount;

	public bool IsInit;

	public List<ITEM_INFO> ItemList = new List<ITEM_INFO>();

	public BagFilter BagFilter;

	public ItemType ItemType;

	public ItemQuality ItemQuality;

	public LianQiCaiLiaoYinYang LianQiCaiLiaoYinYang;

	public LianQiCaiLiaoType LianQiCaiLiaoType;

	public SkIllType SkIllType = SkIllType.全部;

	public SkillQuality SkillQuality;

	public StaticSkIllType StaticSkIllType = StaticSkIllType.全部;

	public LoopListView2 MLoopListView;

	public int mItemCountPerRow = 5;

	public List<ISlot> SlotList = new List<ISlot>();

	public virtual void Init()
	{
		MLoopListView.InitListView(GetCount(MItemTotalCount), OnGetItemByIndex);
	}

	public virtual void OpenBag(List<ITEM_INFO> itemList)
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Expected O, but got Unknown
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Expected O, but got Unknown
		ItemType = ItemType.全部;
		ItemQuality = ItemQuality.全部;
		LianQiCaiLiaoYinYang = LianQiCaiLiaoYinYang.全部;
		LianQiCaiLiaoType = LianQiCaiLiaoType.全部;
		ItemList = new List<ITEM_INFO>(itemList);
		MItemTotalCount = ItemList.Count;
		SlotList = new List<ISlot>();
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
			foreach (ItemType itemType in Enum.GetValues(typeof(ItemType)))
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
					if (itemType == ItemType.材料)
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
										BaseBag baseBag2 = this;
										LianQiCaiLiaoYinYang yinYang = (LianQiCaiLiaoYinYang)enumerator3.Current;
										BagFilter.AddSmallTypeBtn((UnityAction)delegate
										{
											baseBag2.LianQiCaiLiaoYinYang = yinYang;
											baseBag2.BagFilter.CloseSmallSelect();
											baseBag2.UpdateItem();
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
										BaseBag baseBag = this;
										LianQiCaiLiaoType lianQiCaiLiaoType = (LianQiCaiLiaoType)enumerator2.Current;
										BagFilter.AddSmallTypeBtn((UnityAction)delegate
										{
											baseBag.LianQiCaiLiaoType = lianQiCaiLiaoType;
											baseBag.BagFilter.CloseSmallSelect();
											baseBag.UpdateItem();
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
		}, (ItemType == ItemType.全部) ? "类型" : ItemType.ToString());
		if (!IsInit)
		{
			Init();
		}
		UpdateItem();
	}

	public virtual void UpdateItem()
	{
		ItemList = new List<ITEM_INFO>();
		foreach (ITEM_INFO item in ItemList)
		{
			BaseItem baseItem = BaseItem.Create(item.itemId, (int)item.itemCount, item.uuid, item.Seid);
			if (FiddlerItem(baseItem))
			{
				ItemList.Add(item);
			}
		}
		MItemTotalCount = ItemList.Count;
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
		return true;
	}

	public int GetCount(int itemCout)
	{
		int num = itemCout / mItemCountPerRow;
		if (itemCout % mItemCountPerRow > 0)
		{
			num++;
		}
		return num + 1;
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
		for (int i = 0; i < mItemCountPerRow; i++)
		{
			int num = rowIndex * mItemCountPerRow + i;
			component.mItemList[i].SetAccptType(CanSlotType.全部物品);
			if (num >= MItemTotalCount)
			{
				component.mItemList[i].SetNull();
				if (!SlotList.Contains((SlotBase)component.mItemList[i]))
				{
					SlotList.Add((SlotBase)component.mItemList[i]);
				}
				continue;
			}
			BaseItem slotData = BaseItem.Create(ItemList[num].itemId, (int)ItemList[num].itemCount, ItemList[num].uuid, ItemList[num].Seid);
			component.mItemList[i].SetSlotData(slotData);
			if (!SlotList.Contains(component.mItemList[i]))
			{
				SlotList.Add(component.mItemList[i]);
			}
		}
		return loopListViewItem;
	}
}
