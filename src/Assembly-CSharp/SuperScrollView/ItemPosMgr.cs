using System;
using System.Collections.Generic;

namespace SuperScrollView
{
	// Token: 0x020006C3 RID: 1731
	public class ItemPosMgr
	{
		// Token: 0x060036B5 RID: 14005 RVA: 0x001761D0 File Offset: 0x001743D0
		public ItemPosMgr(float itemDefaultSize)
		{
			this.mItemDefaultSize = itemDefaultSize;
		}

		// Token: 0x060036B6 RID: 14006 RVA: 0x00176200 File Offset: 0x00174400
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

		// Token: 0x060036B7 RID: 14007 RVA: 0x00176360 File Offset: 0x00174560
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

		// Token: 0x060036B8 RID: 14008 RVA: 0x001763CC File Offset: 0x001745CC
		public float GetItemPos(int itemIndex)
		{
			this.Update(true);
			int index = itemIndex / 100;
			int index2 = itemIndex % 100;
			return this.mItemSizeGroupList[index].GetItemStartPos(index2);
		}

		// Token: 0x060036B9 RID: 14009 RVA: 0x001763FC File Offset: 0x001745FC
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

		// Token: 0x060036BA RID: 14010 RVA: 0x001764DC File Offset: 0x001746DC
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

		// Token: 0x04002FBA RID: 12218
		public const int mItemMaxCountPerGroup = 100;

		// Token: 0x04002FBB RID: 12219
		private List<ItemSizeGroup> mItemSizeGroupList = new List<ItemSizeGroup>();

		// Token: 0x04002FBC RID: 12220
		private int mDirtyBeginIndex = int.MaxValue;

		// Token: 0x04002FBD RID: 12221
		public float mTotalSize;

		// Token: 0x04002FBE RID: 12222
		public float mItemDefaultSize = 20f;

		// Token: 0x04002FBF RID: 12223
		private int mMaxNotEmptyGroupIndex;
	}
}
