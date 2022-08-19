using System;

namespace KBEngine
{
	// Token: 0x02000B9B RID: 2971
	public struct UINT16
	{
		// Token: 0x06005319 RID: 21273 RVA: 0x00233922 File Offset: 0x00231B22
		private UINT16(ushort value)
		{
			this.value = value;
		}

		// Token: 0x0600531A RID: 21274 RVA: 0x0023392B File Offset: 0x00231B2B
		public static implicit operator ushort(UINT16 value)
		{
			return value.value;
		}

		// Token: 0x0600531B RID: 21275 RVA: 0x00233933 File Offset: 0x00231B33
		public static implicit operator UINT16(ushort value)
		{
			return new UINT16(value);
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x0600531C RID: 21276 RVA: 0x0023393B File Offset: 0x00231B3B
		public static ushort MaxValue
		{
			get
			{
				return ushort.MaxValue;
			}
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x0600531D RID: 21277 RVA: 0x0000280F File Offset: 0x00000A0F
		public static ushort MinValue
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x04005027 RID: 20519
		private ushort value;
	}
}
