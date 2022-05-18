using System;

namespace SuperScrollView
{
	// Token: 0x020009ED RID: 2541
	public class ItemSizeGroup
	{
		// Token: 0x060040C8 RID: 16584 RVA: 0x0002E951 File Offset: 0x0002CB51
		public ItemSizeGroup(int index, float itemDefaultSize)
		{
			this.mGroupIndex = index;
			this.mItemDefaultSize = itemDefaultSize;
			this.Init();
		}

		// Token: 0x060040C9 RID: 16585 RVA: 0x001BE7F0 File Offset: 0x001BC9F0
		public void Init()
		{
			this.mItemSizeArray = new float[100];
			if (this.mItemDefaultSize != 0f)
			{
				for (int i = 0; i < this.mItemSizeArray.Length; i++)
				{
					this.mItemSizeArray[i] = this.mItemDefaultSize;
				}
			}
			this.mItemStartPosArray = new float[100];
			this.mItemStartPosArray[0] = 0f;
			this.mItemCount = 100;
			this.mGroupSize = this.mItemDefaultSize * (float)this.mItemSizeArray.Length;
			if (this.mItemDefaultSize != 0f)
			{
				this.mDirtyBeginIndex = 0;
				return;
			}
			this.mDirtyBeginIndex = 100;
		}

		// Token: 0x060040CA RID: 16586 RVA: 0x0002E975 File Offset: 0x0002CB75
		public float GetItemStartPos(int index)
		{
			return this.mGroupStartPos + this.mItemStartPosArray[index];
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x060040CB RID: 16587 RVA: 0x0002E986 File Offset: 0x0002CB86
		public bool IsDirty
		{
			get
			{
				return this.mDirtyBeginIndex < this.mItemCount;
			}
		}

		// Token: 0x060040CC RID: 16588 RVA: 0x001BE890 File Offset: 0x001BCA90
		public float SetItemSize(int index, float size)
		{
			if (index > this.mMaxNoZeroIndex && size > 0f)
			{
				this.mMaxNoZeroIndex = index;
			}
			float num = this.mItemSizeArray[index];
			if (num == size)
			{
				return 0f;
			}
			this.mItemSizeArray[index] = size;
			if (index < this.mDirtyBeginIndex)
			{
				this.mDirtyBeginIndex = index;
			}
			float num2 = size - num;
			this.mGroupSize += num2;
			return num2;
		}

		// Token: 0x060040CD RID: 16589 RVA: 0x0002E996 File Offset: 0x0002CB96
		public void SetItemCount(int count)
		{
			if (count < this.mMaxNoZeroIndex)
			{
				this.mMaxNoZeroIndex = count;
			}
			if (this.mItemCount == count)
			{
				return;
			}
			this.mItemCount = count;
			this.RecalcGroupSize();
		}

		// Token: 0x060040CE RID: 16590 RVA: 0x001BE8F4 File Offset: 0x001BCAF4
		public void RecalcGroupSize()
		{
			this.mGroupSize = 0f;
			for (int i = 0; i < this.mItemCount; i++)
			{
				this.mGroupSize += this.mItemSizeArray[i];
			}
		}

		// Token: 0x060040CF RID: 16591 RVA: 0x001BE934 File Offset: 0x001BCB34
		public int GetItemIndexByPos(float pos)
		{
			if (this.mItemCount == 0)
			{
				return -1;
			}
			int i = 0;
			int num = this.mItemCount - 1;
			if (this.mItemDefaultSize == 0f)
			{
				if (this.mMaxNoZeroIndex < 0)
				{
					this.mMaxNoZeroIndex = 0;
				}
				num = this.mMaxNoZeroIndex;
			}
			while (i <= num)
			{
				int num2 = (i + num) / 2;
				float num3 = this.mItemStartPosArray[num2];
				float num4 = num3 + this.mItemSizeArray[num2];
				if (num3 <= pos && num4 >= pos)
				{
					return num2;
				}
				if (pos > num4)
				{
					i = num2 + 1;
				}
				else
				{
					num = num2 - 1;
				}
			}
			return -1;
		}

		// Token: 0x060040D0 RID: 16592 RVA: 0x001BE9B4 File Offset: 0x001BCBB4
		public void UpdateAllItemStartPos()
		{
			if (this.mDirtyBeginIndex >= this.mItemCount)
			{
				return;
			}
			for (int i = (this.mDirtyBeginIndex < 1) ? 1 : this.mDirtyBeginIndex; i < this.mItemCount; i++)
			{
				this.mItemStartPosArray[i] = this.mItemStartPosArray[i - 1] + this.mItemSizeArray[i - 1];
			}
			this.mDirtyBeginIndex = this.mItemCount;
		}

		// Token: 0x060040D1 RID: 16593 RVA: 0x001BEA1C File Offset: 0x001BCC1C
		public void ClearOldData()
		{
			for (int i = this.mItemCount; i < 100; i++)
			{
				this.mItemSizeArray[i] = 0f;
			}
		}

		// Token: 0x040039B5 RID: 14773
		public float[] mItemSizeArray;

		// Token: 0x040039B6 RID: 14774
		public float[] mItemStartPosArray;

		// Token: 0x040039B7 RID: 14775
		public int mItemCount;

		// Token: 0x040039B8 RID: 14776
		private int mDirtyBeginIndex = 100;

		// Token: 0x040039B9 RID: 14777
		public float mGroupSize;

		// Token: 0x040039BA RID: 14778
		public float mGroupStartPos;

		// Token: 0x040039BB RID: 14779
		public float mGroupEndPos;

		// Token: 0x040039BC RID: 14780
		public int mGroupIndex;

		// Token: 0x040039BD RID: 14781
		private float mItemDefaultSize;

		// Token: 0x040039BE RID: 14782
		private int mMaxNoZeroIndex;
	}
}
