using System;

namespace KBEngine
{
	// Token: 0x02000F4A RID: 3914
	public struct ENTITY_FORBIDS
	{
		// Token: 0x06005E43 RID: 24131 RVA: 0x00042169 File Offset: 0x00040369
		private ENTITY_FORBIDS(int value)
		{
			this.value = value;
		}

		// Token: 0x06005E44 RID: 24132 RVA: 0x00042172 File Offset: 0x00040372
		public static implicit operator int(ENTITY_FORBIDS value)
		{
			return value.value;
		}

		// Token: 0x06005E45 RID: 24133 RVA: 0x0004217A File Offset: 0x0004037A
		public static implicit operator ENTITY_FORBIDS(int value)
		{
			return new ENTITY_FORBIDS(value);
		}

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x06005E46 RID: 24134 RVA: 0x00041B41 File Offset: 0x0003FD41
		public static int MaxValue
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x06005E47 RID: 24135 RVA: 0x00041B48 File Offset: 0x0003FD48
		public static int MinValue
		{
			get
			{
				return int.MinValue;
			}
		}

		// Token: 0x04005AF4 RID: 23284
		private int value;
	}
}
