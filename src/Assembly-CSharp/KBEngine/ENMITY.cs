using System;

namespace KBEngine
{
	// Token: 0x02000BC2 RID: 3010
	public struct ENMITY
	{
		// Token: 0x060053EC RID: 21484 RVA: 0x00233F8B File Offset: 0x0023218B
		private ENMITY(int value)
		{
			this.value = value;
		}

		// Token: 0x060053ED RID: 21485 RVA: 0x00233F94 File Offset: 0x00232194
		public static implicit operator int(ENMITY value)
		{
			return value.value;
		}

		// Token: 0x060053EE RID: 21486 RVA: 0x00233F9C File Offset: 0x0023219C
		public static implicit operator ENMITY(int value)
		{
			return new ENMITY(value);
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x060053EF RID: 21487 RVA: 0x002339E0 File Offset: 0x00231BE0
		public static int MaxValue
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x060053F0 RID: 21488 RVA: 0x002339E7 File Offset: 0x00231BE7
		public static int MinValue
		{
			get
			{
				return int.MinValue;
			}
		}

		// Token: 0x0400504E RID: 20558
		private int value;
	}
}
