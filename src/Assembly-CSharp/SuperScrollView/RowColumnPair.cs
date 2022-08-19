using System;

namespace SuperScrollView
{
	// Token: 0x020006C1 RID: 1729
	public struct RowColumnPair
	{
		// Token: 0x060036A5 RID: 13989 RVA: 0x00175E97 File Offset: 0x00174097
		public RowColumnPair(int row1, int column1)
		{
			this.mRow = row1;
			this.mColumn = column1;
		}

		// Token: 0x060036A6 RID: 13990 RVA: 0x00175EA7 File Offset: 0x001740A7
		public bool Equals(RowColumnPair other)
		{
			return this.mRow == other.mRow && this.mColumn == other.mColumn;
		}

		// Token: 0x060036A7 RID: 13991 RVA: 0x00175EA7 File Offset: 0x001740A7
		public static bool operator ==(RowColumnPair a, RowColumnPair b)
		{
			return a.mRow == b.mRow && a.mColumn == b.mColumn;
		}

		// Token: 0x060036A8 RID: 13992 RVA: 0x00175EC7 File Offset: 0x001740C7
		public static bool operator !=(RowColumnPair a, RowColumnPair b)
		{
			return a.mRow != b.mRow || a.mColumn != b.mColumn;
		}

		// Token: 0x060036A9 RID: 13993 RVA: 0x0000280F File Offset: 0x00000A0F
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x060036AA RID: 13994 RVA: 0x00175EEA File Offset: 0x001740EA
		public override bool Equals(object obj)
		{
			return obj != null && obj is RowColumnPair && this.Equals((RowColumnPair)obj);
		}

		// Token: 0x04002FAE RID: 12206
		public int mRow;

		// Token: 0x04002FAF RID: 12207
		public int mColumn;
	}
}
