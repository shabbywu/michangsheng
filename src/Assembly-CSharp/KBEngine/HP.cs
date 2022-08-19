using System;

namespace KBEngine
{
	// Token: 0x02000BC3 RID: 3011
	public struct HP
	{
		// Token: 0x060053F1 RID: 21489 RVA: 0x00233FA4 File Offset: 0x002321A4
		private HP(int value)
		{
			this.value = value;
		}

		// Token: 0x060053F2 RID: 21490 RVA: 0x00233FAD File Offset: 0x002321AD
		public static implicit operator int(HP value)
		{
			return value.value;
		}

		// Token: 0x060053F3 RID: 21491 RVA: 0x00233FB5 File Offset: 0x002321B5
		public static implicit operator HP(int value)
		{
			return new HP(value);
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x060053F4 RID: 21492 RVA: 0x002339E0 File Offset: 0x00231BE0
		public static int MaxValue
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x060053F5 RID: 21493 RVA: 0x002339E7 File Offset: 0x00231BE7
		public static int MinValue
		{
			get
			{
				return int.MinValue;
			}
		}

		// Token: 0x0400504F RID: 20559
		private int value;
	}
}
