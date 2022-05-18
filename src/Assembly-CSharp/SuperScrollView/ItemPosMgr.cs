using System;
using System.Collections.Generic;

namespace SuperScrollView
{
	// Token: 0x020009EE RID: 2542
	public class ItemPosMgr
	{
		// Token: 0x060040D2 RID: 16594 RVA: 0x0002E9BF File Offset: 0x0002CBBF
		public ItemPosMgr(float itemDefaultSize)
		{
			this.mItemDefaultSize = itemDefaultSize;
		}

		// Token: 0x060040D3 RID: 16595 RVA: 0x001BEA48 File Offset: 0x001BCC48
		public void SetItemMaxCount(int maxCount)
		{
			this.mDirtyBeginIndex = 0;
			this.mTotalSize = 0f;
			int itemCount;
			int num = itemCount = maxCount % 100;
			int num2 = maxCount / 100;
			if (num > 0)
			{
				num2++;
			}
			else
			{
				itemCount = 100;
			}
			int count = this.mItemSizeGroupList.Count;
			if (count > num2)
			{
				int count2 = count - num2;
				this.mItemSizeGroupList.RemoveRange(num2, count2);
			}
			else if (count < num2)
			{
				if (count > 0)
				{
					this.mItemSizeGroupList[count - 1].ClearOldData();
				}
				int num3 = num2 - count;
				for (int i = 0; i < num3; i++)
				{
					ItemSizeGroup item = new ItemSizeGroup(count + i, this.mItemDefaultSize);
					this.mItemSizeGroupList.Add(item);
				}
			}
			else if (count > 0)
			{
				this.mItemSizeGroupList[count - 1].ClearOldData();
			}
			count = this.mItemSizeGroupList.Count;
			if (count - 1 < this.mMaxNotEmptyGroupIndex)
			{
				this.mMaxNotEmptyGroupIndex = count - 1;
			}
			if (this.mMaxNotEmptyGroupIndex < 0)
			{
				this.mMaxNotEmptyGroupIndex = 0;
			}
			if (count == 0)
			{
				return;
			}
			for (int j = 0; j < count - 1; j++)
			{
				this.mItemSizeGroupList[j].SetItemCount(100);
			}
			this.mItemSizeGroupList[count - 1].SetItemCount(itemCount);
			for (int k = 0; k < count; k++)
			{
				this.mTotalSize += this.mItemSizeGroupList[k].mGroupSize;
			}
		}

		// Token: 0x060040D4 RID: 16596 RVA: 0x001BEBA8 File Offset: 0x001BCDA8
		public void SetItemSize(int itemIndex, float size)
		{
			int num = itemIndex / 100;
			int index = itemIndex % 100;
			float num2 = this.mItemSizeGroupList[num].SetItemSize(index, size);
			if (num2 != 0f && num < this.mDirtyBeginIndex)
			{
				this.mDirtyBeginIndex = num;
			}
			this.mTotalSize += num2;
			if (num > this.mMaxNotEmptyGroupIndex && size > 0f)
			{
				this.mMaxNotEmptyGroupIndex = num;
			}
		}

		// Token: 0x060040D5 RID: 16597 RVA: 0x001BEC14 File Offset: 0x001BCE14
		public float GetItemPos(int itemIndex)
		{
			this.Update(true);
			int index = itemIndex / 100;
			int index2 = itemIndex % 100;
			return this.mItemSizeGroupList[index].GetItemStartPos(index2);
		}

		// Token: 0x060040D6 RID: 16598 RVA: 0x001BEC44 File Offset: 0x001BCE44
		public bool GetItemIndexAndPosAtGivenPos(float pos, ref int index, ref float itemPos)
		{
			this.Update(true);
			index = 0;
			itemPos = 0f;
			int count = this.mItemSizeGroupList.Count;
			if (count == 0)
			{
				return true;
			}
			ItemSizeGroup itemSizeGroup = null;
			int i = 0;
			int num = count - 1;
			if (this.mItemDefaultSize == 0f)
			{
				if (this.mMaxNotEmptyGroupIndex < 0)
				{
					this.mMaxNotEmptyGroupIndex = 0;
				}
				num = this.mMaxNotEmptyGroupIndex;
			}
			while (i <= num)
			{
				int num2 = (i + num) / 2;
				ItemSizeGroup itemSizeGroup2 = this.mItemSizeGroupList[num2];
				if (itemSizeGroup2.mGroupStartPos <= pos && itemSizeGroup2.mGroupEndPos >= pos)
				{
					itemSizeGroup = itemSizeGroup2;
					break;
				}
				if (pos > itemSizeGroup2.mGroupEndPos)
				{
					i = num2 + 1;
				}
				else
				{
					num = num2 - 1;
				}
			}
			if (itemSizeGroup == null)
			{
				return false;
			}
			int itemIndexByPos = itemSizeGroup.GetItemIndexByPos(pos - itemSizeGroup.mGroupStartPos);
			if (itemIndexByPos < 0)
			{
				return false;
			}
			index = itemIndexByPos + itemSizeGroup.mGroupIndex * 100;
			itemPos = itemSizeGroup.GetItemStartPos(itemIndexByPos);
			return true;
		}

		// Token: 0x060040D7 RID: 16599 RVA: 0x001BED24 File Offset: 0x001BCF24
		public void Update(bool updateAll)
		{
			int count = this.mItemSizeGroupList.Count;
			if (count == 0)
			{
				return;
			}
			if (this.mDirtyBeginIndex >= count)
			{
				return;
			}
			int num = 0;
			for (int i = this.mDirtyBeginIndex; i < count; i++)
			{
				num++;
				ItemSizeGroup itemSizeGroup = this.mItemSizeGroupList[i];
				this.mDirtyBeginIndex++;
				itemSizeGroup.UpdateAllItemStartPos();
				if (i == 0)
				{
					itemSizeGroup.mGroupStartPos = 0f;
					itemSizeGroup.mGroupEndPos = itemSizeGroup.mGroupSize;
				}
				else
				{
					itemSizeGroup.mGroupStartPos = this.mItemSizeGroupList[i - 1].mGroupEndPos;
					itemSizeGroup.mGroupEndPos = itemSizeGroup.mGroupStartPos + itemSizeGroup.mGroupSize;
				}
				if (!updateAll && num > 1)
				{
					return;
				}
			}
		}

		// Token: 0x040039BF RID: 14783
		public const int mItemMaxCountPerGroup = 100;

		// Token: 0x040039C0 RID: 14784
		private List<ItemSizeGroup> mItemSizeGroupList = new List<ItemSizeGroup>();

		// Token: 0x040039C1 RID: 14785
		private int mDirtyBeginIndex = int.MaxValue;

		// Token: 0x040039C2 RID: 14786
		public float mTotalSize;

		// Token: 0x040039C3 RID: 14787
		public float mItemDefaultSize = 20f;

		// Token: 0x040039C4 RID: 14788
		private int mMaxNotEmptyGroupIndex;
	}
}
