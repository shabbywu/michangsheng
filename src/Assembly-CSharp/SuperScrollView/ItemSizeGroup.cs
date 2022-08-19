using System;

namespace SuperScrollView
{
	// Token: 0x020006C2 RID: 1730
	public class ItemSizeGroup
	{
		// Token: 0x060036AB RID: 13995 RVA: 0x00175F07 File Offset: 0x00174107
		public ItemSizeGroup(int index, float itemDefaultSize)
		{
			this.mGroupIndex = index;
			this.mItemDefaultSize = itemDefaultSize;
			this.Init();
		}

		// Token: 0x060036AC RID: 13996 RVA: 0x00175F2C File Offset: 0x0017412C
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

		// Token: 0x060036AD RID: 13997 RVA: 0x00175FC9 File Offset: 0x001741C9
		public float GetItemStartPos(int index)
		{
			return this.mGroupStartPos + this.mItemStartPosArray[index];
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x060036AE RID: 13998 RVA: 0x00175FDA File Offset: 0x001741DA
		public bool IsDirty
		{
			get
			{
				return this.mDirtyBeginIndex < this.mItemCount;
			}
		}

		// Token: 0x060036AF RID: 13999 RVA: 0x00175FEC File Offset: 0x001741EC
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

		// Token: 0x060036B0 RID: 14000 RVA: 0x00176050 File Offset: 0x00174250
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

		// Token: 0x060036B1 RID: 14001 RVA: 0x0017607C File Offset: 0x0017427C
		public void RecalcGroupSize()
		{
			this.mGroupSize = 0f;
			for (int i = 0; i < this.mItemCount; i++)
			{
				this.mGroupSize += this.mItemSizeArray[i];
			}
		}

		// Token: 0x060036B2 RID: 14002 RVA: 0x001760BC File Offset: 0x001742BC
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

		// Token: 0x060036B3 RID: 14003 RVA: 0x0017613C File Offset: 0x0017433C
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

		// Token: 0x060036B4 RID: 14004 RVA: 0x001761A4 File Offset: 0x001743A4
		public void ClearOldData()
		{
			for (int i = this.mItemCount; i < 100; i++)
			{
				this.mItemSizeArray[i] = 0f;
			}
		}

		// Token: 0x04002FB0 RID: 12208
		public float[] mItemSizeArray;

		// Token: 0x04002FB1 RID: 12209
		public float[] mItemStartPosArray;

		// Token: 0x04002FB2 RID: 12210
		public int mItemCount;

		// Token: 0x04002FB3 RID: 12211
		private int mDirtyBeginIndex = 100;

		// Token: 0x04002FB4 RID: 12212
		public float mGroupSize;

		// Token: 0x04002FB5 RID: 12213
		public float mGroupStartPos;

		// Token: 0x04002FB6 RID: 12214
		public float mGroupEndPos;

		// Token: 0x04002FB7 RID: 12215
		public int mGroupIndex;

		// Token: 0x04002FB8 RID: 12216
		private float mItemDefaultSize;

		// Token: 0x04002FB9 RID: 12217
		private int mMaxNoZeroIndex;
	}
}
