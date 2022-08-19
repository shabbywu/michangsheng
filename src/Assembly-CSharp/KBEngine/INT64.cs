using System;

namespace KBEngine
{
	// Token: 0x02000BA1 RID: 2977
	public struct INT64
	{
		// Token: 0x06005337 RID: 21303 RVA: 0x002339EE File Offset: 0x00231BEE
		private INT64(long value)
		{
			this.value = value;
		}

		// Token: 0x06005338 RID: 21304 RVA: 0x002339F7 File Offset: 0x00231BF7
		public static implicit operator long(INT64 value)
		{
			return value.value;
		}

		// Token: 0x06005339 RID: 21305 RVA: 0x002339FF File Offset: 0x00231BFF
		public static implicit operator INT64(long value)
		{
			return new INT64(value);
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x0600533A RID: 21306 RVA: 0x00233A07 File Offset: 0x00231C07
		public static long MaxValue
		{
			get
			{
				return long.MaxValue;
			}
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x0600533B RID: 21307 RVA: 0x00233A12 File Offset: 0x00231C12
		public static long MinValue
		{
			get
			{
				return long.MinValue;
			}
		}

		// Token: 0x0400502D RID: 20525
		private long value;
	}
}
