using System;

namespace KBEngine
{
	// Token: 0x02000BC1 RID: 3009
	public struct DAMAGE_TYPE
	{
		// Token: 0x060053E7 RID: 21479 RVA: 0x00233F72 File Offset: 0x00232172
		private DAMAGE_TYPE(int value)
		{
			this.value = value;
		}

		// Token: 0x060053E8 RID: 21480 RVA: 0x00233F7B File Offset: 0x0023217B
		public static implicit operator int(DAMAGE_TYPE value)
		{
			return value.value;
		}

		// Token: 0x060053E9 RID: 21481 RVA: 0x00233F83 File Offset: 0x00232183
		public static implicit operator DAMAGE_TYPE(int value)
		{
			return new DAMAGE_TYPE(value);
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x060053EA RID: 21482 RVA: 0x002339E0 File Offset: 0x00231BE0
		public static int MaxValue
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x060053EB RID: 21483 RVA: 0x002339E7 File Offset: 0x00231BE7
		public static int MinValue
		{
			get
			{
				return int.MinValue;
			}
		}

		// Token: 0x0400504D RID: 20557
		private int value;
	}
}
