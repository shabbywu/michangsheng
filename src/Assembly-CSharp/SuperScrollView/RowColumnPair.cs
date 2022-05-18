using System;

namespace SuperScrollView
{
	// Token: 0x020009EC RID: 2540
	public struct RowColumnPair
	{
		// Token: 0x060040C2 RID: 16578 RVA: 0x0002E8E1 File Offset: 0x0002CAE1
		public RowColumnPair(int row1, int column1)
		{
			this.mRow = row1;
			this.mColumn = column1;
		}

		// Token: 0x060040C3 RID: 16579 RVA: 0x0002E8F1 File Offset: 0x0002CAF1
		public bool Equals(RowColumnPair other)
		{
			return this.mRow == other.mRow && this.mColumn == other.mColumn;
		}

		// Token: 0x060040C4 RID: 16580 RVA: 0x0002E8F1 File Offset: 0x0002CAF1
		public static bool operator ==(RowColumnPair a, RowColumnPair b)
		{
			return a.mRow == b.mRow && a.mColumn == b.mColumn;
		}

		// Token: 0x060040C5 RID: 16581 RVA: 0x0002E911 File Offset: 0x0002CB11
		public static bool operator !=(RowColumnPair a, RowColumnPair b)
		{
			return a.mRow != b.mRow || a.mColumn != b.mColumn;
		}

		// Token: 0x060040C6 RID: 16582 RVA: 0x00004050 File Offset: 0x00002250
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x060040C7 RID: 16583 RVA: 0x0002E934 File Offset: 0x0002CB34
		public override bool Equals(object obj)
		{
			return obj != null && obj is RowColumnPair && this.Equals((RowColumnPair)obj);
		}

		// Token: 0x040039B3 RID: 14771
		public int mRow;

		// Token: 0x040039B4 RID: 14772
		public int mColumn;
	}
}
