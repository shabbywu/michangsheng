using System;
using System.Collections;
using System.Collections.Generic;
using KBEngine;
using SuperScrollView;
using Tab;
using UnityEngine;

namespace Bag
{
	// Token: 0x02000D18 RID: 3352
	public class BaseBag : MonoBehaviour, IBaseBag
	{
		// Token: 0x06004FD7 RID: 20439 RVA: 0x00039770 File Offset: 0x00037970
		public virtual void Init()
		{
			this.MLoopListView.InitListView(this.GetCount(this.MItemTotalCount), new Func<LoopListView2, int, LoopListViewItem2>(this.OnGetItemByIndex), null);
		}

		// Token: 0x06004FD8 RID: 20440 RVA: 0x002178F0 File Offset: 0x00215AF0
		public virtual void OpenBag(List<ITEM_INFO> itemList)
		{
			this.ItemType = ItemType.全部;
			this.ItemQuality = ItemQuality.全部;
			this.LianQiCaiLiaoYinYang = LianQiCaiLiaoYinYang.全部;
			this.LianQiCaiLiaoType = LianQiCaiLiaoType.全部;
			this.ItemList = new List<ITEM_INFO>(itemList);
			this.MItemTotalCount = this.ItemList.Count;
			this.SlotList = new List<ISlot>();
			this.BagFilter.AddBigTypeBtn(delegate
			{
				using (IEnumerator enumerator = Enum.GetValues(typeof(ItemQuality)).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ItemQuality itemQuality = (ItemQuality)enumerator.Current;
						this.BagFilter.AddSmallTypeBtn(delegate
						{
							this.ItemQuality = itemQuality;
							this.BagFilter.CloseSmallSelect();
							this.UpdateItem();
						}, itemQuality.ToString());
					}
				}
			}, (this.ItemQuality == ItemQuality.全部) ? "品阶" : this.ItemQuality.ToString());
			this.BagFilter.AddBigTypeBtn(delegate
			{
				using (IEnumerator enumerator = Enum.GetValues(typeof(ItemType)).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ItemType itemType = (ItemType)enumerator.Current;
						this.BagFilter.AddSmallTypeBtn(delegate
						{
							this.ItemType = itemType;
							this.BagFilter.CloseSmallSelect();
							this.UpdateItem();
							if (itemType != ItemType.材料)
							{
								if (this.BagFilter.BigTypeIndex >= 2)
								{
									this.BagFilter.BigFilterBtnList[2].gameObject.SetActive(false);
									this.BagFilter.BigFilterBtnList[3].gameObject.SetActive(false);
								}
								return;
							}
							if (this.BagFilter.BigTypeIndex >= 3)
							{
								this.BagFilter.BigFilterBtnList[2].gameObject.SetActive(true);
								this.BagFilter.BigFilterBtnList[3].gameObject.SetActive(true);
								return;
							}
							this.BagFilter.AddBigTypeBtn(delegate
							{
								using (IEnumerator enumerator2 = Enum.GetValues(typeof(LianQiCaiLiaoYinYang)).GetEnumerator())
								{
									while (enumerator2.MoveNext())
									{
										LianQiCaiLiaoYinYang yinYang = (LianQiCaiLiaoYinYang)enumerator2.Current;
										this.BagFilter.AddSmallTypeBtn(delegate
										{
											this.LianQiCaiLiaoYinYang = yinYang;
											this.BagFilter.CloseSmallSelect();
											this.UpdateItem();
										}, yinYang.ToString());
									}
								}
							}, (this.LianQiCaiLiaoYinYang == LianQiCaiLiaoYinYang.全部) ? "阴阳" : this.LianQiCaiLiaoYinYang.ToString());
							this.BagFilter.AddBigTypeBtn(delegate
							{
								using (IEnumerator enumerator2 = Enum.GetValues(typeof(LianQiCaiLiaoType)).GetEnumerator())
								{
									while (enumerator2.MoveNext())
									{
										LianQiCaiLiaoType lianQiCaiLiaoType = (LianQiCaiLiaoType)enumerator2.Current;
										this.BagFilter.AddSmallTypeBtn(delegate
										{
											this.LianQiCaiLiaoType = lianQiCaiLiaoType;
											this.BagFilter.CloseSmallSelect();
											this.UpdateItem();
										}, lianQiCaiLiaoType.ToString());
									}
								}
							}, (this.LianQiCaiLiaoType == LianQiCaiLiaoType.全部) ? "属性" : this.LianQiCaiLiaoType.ToString());
						}, itemType.ToString());
					}
				}
			}, (this.ItemType == ItemType.全部) ? "类型" : this.ItemType.ToString());
			if (!this.IsInit)
			{
				this.Init();
			}
			this.UpdateItem();
		}

		// Token: 0x06004FD9 RID: 20441 RVA: 0x002179C4 File Offset: 0x00215BC4
		public virtual void UpdateItem()
		{
			this.ItemList = new List<ITEM_INFO>();
			foreach (ITEM_INFO item_INFO in this.ItemList)
			{
				BaseItem baseItem = BaseItem.Create(item_INFO.itemId, (int)item_INFO.itemCount, item_INFO.uuid, item_INFO.Seid);
				if (this.FiddlerItem(baseItem))
				{
					this.ItemList.Add(item_INFO);
				}
			}
			this.MItemTotalCount = this.ItemList.Count;
		}

		// Token: 0x06004FDA RID: 20442 RVA: 0x00217A60 File Offset: 0x00215C60
		public bool FiddlerItem(BaseItem baseItem)
		{
			if (this.ItemQuality != ItemQuality.全部 && baseItem.GetImgQuality() != (int)this.ItemQuality)
			{
				return false;
			}
			if (this.ItemType != ItemType.全部 && baseItem.ItemType != this.ItemType)
			{
				return false;
			}
			if (this.ItemType == ItemType.材料)
			{
				CaiLiaoItem caiLiaoItem = (CaiLiaoItem)baseItem;
				if (this.LianQiCaiLiaoYinYang != LianQiCaiLiaoYinYang.全部 && caiLiaoItem.GetYinYang() != this.LianQiCaiLiaoYinYang)
				{
					return false;
				}
				if (this.LianQiCaiLiaoType != LianQiCaiLiaoType.全部 && caiLiaoItem.GetLianQiCaiLiaoType() != this.LianQiCaiLiaoType)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06004FDB RID: 20443 RVA: 0x00217AE0 File Offset: 0x00215CE0
		public int GetCount(int itemCout)
		{
			int num = itemCout / this.mItemCountPerRow;
			if (itemCout % this.mItemCountPerRow > 0)
			{
				num++;
			}
			return num + 1;
		}

		// Token: 0x06004FDC RID: 20444 RVA: 0x00217B0C File Offset: 0x00215D0C
		private LoopListViewItem2 OnGetItemByIndex(LoopListView2 listView, int rowIndex)
		{
			if (rowIndex < 0)
			{
				return null;
			}
			LoopListViewItem2 loopListViewItem = listView.NewListViewItem("Prefab");
			SlotList component = loopListViewItem.GetComponent<SlotList>();
			if (!loopListViewItem.IsInitHandlerCalled)
			{
				loopListViewItem.IsInitHandlerCalled = true;
				component.Init();
			}
			for (int i = 0; i < this.mItemCountPerRow; i++)
			{
				int num = rowIndex * this.mItemCountPerRow + i;
				component.mItemList[i].SetAccptType(CanSlotType.全部物品);
				if (num >= this.MItemTotalCount)
				{
					component.mItemList[i].SetNull();
					if (!this.SlotList.Contains((SlotBase)component.mItemList[i]))
					{
						this.SlotList.Add((SlotBase)component.mItemList[i]);
					}
				}
				else
				{
					BaseItem slotData = BaseItem.Create(this.ItemList[num].itemId, (int)this.ItemList[num].itemCount, this.ItemList[num].uuid, this.ItemList[num].Seid);
					component.mItemList[i].SetSlotData(slotData);
					if (!this.SlotList.Contains(component.mItemList[i]))
					{
						this.SlotList.Add(component.mItemList[i]);
					}
				}
			}
			return loopListViewItem;
		}

		// Token: 0x0400512A RID: 20778
		public int MItemTotalCount;

		// Token: 0x0400512B RID: 20779
		public bool IsInit;

		// Token: 0x0400512C RID: 20780
		public List<ITEM_INFO> ItemList = new List<ITEM_INFO>();

		// Token: 0x0400512D RID: 20781
		public BagFilter BagFilter;

		// Token: 0x0400512E RID: 20782
		public ItemType ItemType;

		// Token: 0x0400512F RID: 20783
		public ItemQuality ItemQuality;

		// Token: 0x04005130 RID: 20784
		public LianQiCaiLiaoYinYang LianQiCaiLiaoYinYang;

		// Token: 0x04005131 RID: 20785
		public LianQiCaiLiaoType LianQiCaiLiaoType;

		// Token: 0x04005132 RID: 20786
		public SkIllType SkIllType = SkIllType.全部;

		// Token: 0x04005133 RID: 20787
		public SkillQuality SkillQuality;

		// Token: 0x04005134 RID: 20788
		public StaticSkIllType StaticSkIllType = StaticSkIllType.全部;

		// Token: 0x04005135 RID: 20789
		public LoopListView2 MLoopListView;

		// Token: 0x04005136 RID: 20790
		public int mItemCountPerRow = 5;

		// Token: 0x04005137 RID: 20791
		public List<ISlot> SlotList = new List<ISlot>();
	}
}
