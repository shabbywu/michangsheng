using System;

namespace KBEngine
{
	// Token: 0x02000BB1 RID: 2993
	public struct CONTROLLER_ID
	{
		// Token: 0x0600538F RID: 21391 RVA: 0x00233D2B File Offset: 0x00231F2B
		private CONTROLLER_ID(int value)
		{
			this.value = value;
		}

		// Token: 0x06005390 RID: 21392 RVA: 0x00233D34 File Offset: 0x00231F34
		public static implicit operator int(CONTROLLER_ID value)
		{
			return value.value;
		}

		// Token: 0x06005391 RID: 21393 RVA: 0x00233D3C File Offset: 0x00231F3C
		public static implicit operator CONTROLLER_ID(int value)
		{
			return new CONTROLLER_ID(value);
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06005392 RID: 21394 RVA: 0x002339E0 File Offset: 0x00231BE0
		public static int MaxValue
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06005393 RID: 21395 RVA: 0x002339E7 File Offset: 0x00231BE7
		public static int MinValue
		{
			get
			{
				return int.MinValue;
			}
		}

		// Token: 0x0400503D RID: 20541
		private int value;
	}
}
