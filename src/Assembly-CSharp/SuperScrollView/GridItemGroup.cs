using System;

namespace SuperScrollView
{
	// Token: 0x020009EF RID: 2543
	public class GridItemGroup
	{
		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x060040D8 RID: 16600 RVA: 0x0002E9EF File Offset: 0x0002CBEF
		public int Count
		{
			get
			{
				return this.mCount;
			}
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x060040D9 RID: 16601 RVA: 0x0002E9F7 File Offset: 0x0002CBF7
		public LoopGridViewItem First
		{
			get
			{
				return this.mFirst;
			}
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x060040DA RID: 16602 RVA: 0x0002E9FF File Offset: 0x0002CBFF
		public LoopGridViewItem Last
		{
			get
			{
				return this.mLast;
			}
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x060040DB RID: 16603 RVA: 0x0002EA07 File Offset: 0x0002CC07
		// (set) Token: 0x060040DC RID: 16604 RVA: 0x0002EA0F File Offset: 0x0002CC0F
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

		// Token: 0x060040DD RID: 16605 RVA: 0x001BEDD4 File Offset: 0x001BCFD4
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

		// Token: 0x060040DE RID: 16606 RVA: 0x001BEE08 File Offset: 0x001BD008
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

		// Token: 0x060040DF RID: 16607 RVA: 0x001BEE3C File Offset: 0x001BD03C
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

		// Token: 0x060040E0 RID: 16608 RVA: 0x001BEEC0 File Offset: 0x001BD0C0
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

		// Token: 0x060040E1 RID: 16609 RVA: 0x001BEF54 File Offset: 0x001BD154
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

		// Token: 0x060040E2 RID: 16610 RVA: 0x001BEFE8 File Offset: 0x001BD1E8
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

		// Token: 0x060040E3 RID: 16611 RVA: 0x001BF06C File Offset: 0x001BD26C
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

		// Token: 0x060040E4 RID: 16612 RVA: 0x001BF0F0 File Offset: 0x001BD2F0
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

		// Token: 0x040039C5 RID: 14789
		private int mCount;

		// Token: 0x040039C6 RID: 14790
		private int mGroupIndex = -1;

		// Token: 0x040039C7 RID: 14791
		private LoopGridViewItem mFirst;

		// Token: 0x040039C8 RID: 14792
		private LoopGridViewItem mLast;
	}
}
