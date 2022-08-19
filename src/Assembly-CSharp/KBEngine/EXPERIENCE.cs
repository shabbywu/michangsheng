using System;

namespace KBEngine
{
	// Token: 0x02000BB2 RID: 2994
	public struct EXPERIENCE
	{
		// Token: 0x06005394 RID: 21396 RVA: 0x00233D44 File Offset: 0x00231F44
		private EXPERIENCE(int value)
		{
			this.value = value;
		}

		// Token: 0x06005395 RID: 21397 RVA: 0x00233D4D File Offset: 0x00231F4D
		public static implicit operator int(EXPERIENCE value)
		{
			return value.value;
		}

		// Token: 0x06005396 RID: 21398 RVA: 0x00233D55 File Offset: 0x00231F55
		public static implicit operator EXPERIENCE(int value)
		{
			return new EXPERIENCE(value);
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06005397 RID: 21399 RVA: 0x002339E0 File Offset: 0x00231BE0
		public static int MaxValue
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06005398 RID: 21400 RVA: 0x002339E7 File Offset: 0x00231BE7
		public static int MinValue
		{
			get
			{
				return int.MinValue;
			}
		}

		// Token: 0x0400503E RID: 20542
		private int value;
	}
}
