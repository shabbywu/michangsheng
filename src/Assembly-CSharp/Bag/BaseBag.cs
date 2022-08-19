using System;
using System.Collections;
using System.Collections.Generic;
using KBEngine;
using SuperScrollView;
using Tab;
using UnityEngine;

namespace Bag
{
	// Token: 0x02000997 RID: 2455
	public class BaseBag : MonoBehaviour, IBaseBag
	{
		// Token: 0x0600447F RID: 17535 RVA: 0x001D2CEB File Offset: 0x001D0EEB
		public virtual void Init()
		{
			this.MLoopListView.InitListView(this.GetCount(this.MItemTotalCount), new Func<LoopListView2, int, LoopListViewItem2>(this.OnGetItemByIndex), null);
		}

		// Token: 0x06004480 RID: 17536 RVA: 0x001D2D14 File Offset: 0x001D0F14
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

		// Token: 0x06004481 RID: 17537 RVA: 0x001D2DE8 File Offset: 0x001D0FE8
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

		// Token: 0x06004482 RID: 17538 RVA: 0x001D2E84 File Offset: 0x001D1084
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

		// Token: 0x06004483 RID: 17539 RVA: 0x001D2F04 File Offset: 0x001D1104
		public int GetCount(int itemCout)
		{
			int num = itemCout / this.mItemCountPerRow;
			if (itemCout % this.mItemCountPerRow > 0)
			{
				num++;
			}
			return num + 1;
		}

		// Token: 0x06004484 RID: 17540 RVA: 0x001D2F30 File Offset: 0x001D1130
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

		// Token: 0x04004634 RID: 17972
		public int MItemTotalCount;

		// Token: 0x04004635 RID: 17973
		public bool IsInit;

		// Token: 0x04004636 RID: 17974
		public List<ITEM_INFO> ItemList = new List<ITEM_INFO>();

		// Token: 0x04004637 RID: 17975
		public BagFilter BagFilter;

		// Token: 0x04004638 RID: 17976
		public ItemType ItemType;

		// Token: 0x04004639 RID: 17977
		public ItemQuality ItemQuality;

		// Token: 0x0400463A RID: 17978
		public LianQiCaiLiaoYinYang LianQiCaiLiaoYinYang;

		// Token: 0x0400463B RID: 17979
		public LianQiCaiLiaoType LianQiCaiLiaoType;

		// Token: 0x0400463C RID: 17980
		public SkIllType SkIllType = SkIllType.全部;

		// Token: 0x0400463D RID: 17981
		public SkillQuality SkillQuality;

		// Token: 0x0400463E RID: 17982
		public StaticSkIllType StaticSkIllType = StaticSkIllType.全部;

		// Token: 0x0400463F RID: 17983
		public LoopListView2 MLoopListView;

		// Token: 0x04004640 RID: 17984
		public int mItemCountPerRow = 5;

		// Token: 0x04004641 RID: 17985
		public List<ISlot> SlotList = new List<ISlot>();
	}
}
