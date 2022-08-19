using System;

namespace SuperScrollView
{
	// Token: 0x020006C4 RID: 1732
	public class GridItemGroup
	{
		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x060036BB RID: 14011 RVA: 0x0017658B File Offset: 0x0017478B
		public int Count
		{
			get
			{
				return this.mCount;
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x060036BC RID: 14012 RVA: 0x00176593 File Offset: 0x00174793
		public LoopGridViewItem First
		{
			get
			{
				return this.mFirst;
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x060036BD RID: 14013 RVA: 0x0017659B File Offset: 0x0017479B
		public LoopGridViewItem Last
		{
			get
			{
				return this.mLast;
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x060036BE RID: 14014 RVA: 0x001765A3 File Offset: 0x001747A3
		// (set) Token: 0x060036BF RID: 14015 RVA: 0x001765AB File Offset: 0x001747AB
		public int GroupIndex
		{
			get
			{
				return this.mGroupIndex;
			}
			set
			{
				this.mGroupIndex = value;
			}
		}

		// Token: 0x060036C0 RID: 14016 RVA: 0x001765B4 File Offset: 0x001747B4
		public LoopGridViewItem GetItemByColumn(int column)
		{
			LoopGridViewItem nextItem = this.mFirst;
			while (nextItem != null)
			{
				if (nextItem.Column == column)
				{
					return nextItem;
				}
				nextItem = nextItem.NextItem;
			}
			return null;
		}

		// Token: 0x060036C1 RID: 14017 RVA: 0x001765E8 File Offset: 0x001747E8
		public LoopGridViewItem GetItemByRow(int row)
		{
			LoopGridViewItem nextItem = this.mFirst;
			while (nextItem != null)
			{
				if (nextItem.Row == row)
				{
					return nextItem;
				}
				nextItem = nextItem.NextItem;
			}
			return null;
		}

		// Token: 0x060036C2 RID: 14018 RVA: 0x0017661C File Offset: 0x0017481C
		public void ReplaceItem(LoopGridViewItem curItem, LoopGridViewItem newItem)
		{
			newItem.PrevItem = curItem.PrevItem;
			newItem.NextItem = curItem.NextItem;
			if (newItem.PrevItem != null)
			{
				newItem.PrevItem.NextItem = newItem;
			}
			if (newItem.NextItem != null)
			{
				newItem.NextItem.PrevItem = newItem;
			}
			if (this.mFirst == curItem)
			{
				this.mFirst = newItem;
			}
			if (this.mLast == curItem)
			{
				this.mLast = newItem;
			}
		}

		// Token: 0x060036C3 RID: 14019 RVA: 0x001766A0 File Offset: 0x001748A0
		public void AddFirst(LoopGridViewItem newItem)
		{
			newItem.PrevItem = null;
			newItem.NextItem = null;
			if (this.mFirst == null)
			{
				this.mFirst = newItem;
				this.mLast = newItem;
				this.mFirst.PrevItem = null;
				this.mFirst.NextItem = null;
				this.mCount++;
				return;
			}
			this.mFirst.PrevItem = newItem;
			newItem.PrevItem = null;
			newItem.NextItem = this.mFirst;
			this.mFirst = newItem;
			this.mCount++;
		}

		// Token: 0x060036C4 RID: 14020 RVA: 0x00176734 File Offset: 0x00174934
		public void AddLast(LoopGridViewItem newItem)
		{
			newItem.PrevItem = null;
			newItem.NextItem = null;
			if (this.mFirst == null)
			{
				this.mFirst = newItem;
				this.mLast = newItem;
				this.mFirst.PrevItem = null;
				this.mFirst.NextItem = null;
				this.mCount++;
				return;
			}
			this.mLast.NextItem = newItem;
			newItem.PrevItem = this.mLast;
			newItem.NextItem = null;
			this.mLast = newItem;
			this.mCount++;
		}

		// Token: 0x060036C5 RID: 14021 RVA: 0x001767C8 File Offset: 0x001749C8
		public LoopGridViewItem RemoveFirst()
		{
			LoopGridViewItem result = this.mFirst;
			if (this.mFirst == null)
			{
				return result;
			}
			if (this.mFirst == this.mLast)
			{
				this.mFirst = null;
				this.mLast = null;
				this.mCount--;
				return result;
			}
			this.mFirst = this.mFirst.NextItem;
			this.mFirst.PrevItem = null;
			this.mCount--;
			return result;
		}

		// Token: 0x060036C6 RID: 14022 RVA: 0x0017684C File Offset: 0x00174A4C
		public LoopGridViewItem RemoveLast()
		{
			LoopGridViewItem result = this.mLast;
			if (this.mFirst == null)
			{
				return result;
			}
			if (this.mFirst == this.mLast)
			{
				this.mFirst = null;
				this.mLast = null;
				this.mCount--;
				return result;
			}
			this.mLast = this.mLast.PrevItem;
			this.mLast.NextItem = null;
			this.mCount--;
			return result;
		}

		// Token: 0x060036C7 RID: 14023 RVA: 0x001768D0 File Offset: 0x00174AD0
		public void Clear()
		{
			LoopGridViewItem nextItem = this.mFirst;
			while (nextItem != null)
			{
				nextItem.PrevItem = null;
				nextItem.NextItem = null;
				nextItem = nextItem.NextItem;
			}
			this.mFirst = null;
			this.mLast = null;
			this.mCount = 0;
		}

		// Token: 0x04002FC0 RID: 12224
		private int mCount;

		// Token: 0x04002FC1 RID: 12225
		private int mGroupIndex = -1;

		// Token: 0x04002FC2 RID: 12226
		private LoopGridViewItem mFirst;

		// Token: 0x04002FC3 RID: 12227
		private LoopGridViewItem mLast;
	}
}
