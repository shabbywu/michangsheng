using System;

namespace KBEngine
{
	// Token: 0x02000BAF RID: 2991
	public struct OBJECT_ID
	{
		// Token: 0x06005385 RID: 21381 RVA: 0x00233CF9 File Offset: 0x00231EF9
		private OBJECT_ID(int value)
		{
			this.value = value;
		}

		// Token: 0x06005386 RID: 21382 RVA: 0x00233D02 File Offset: 0x00231F02
		public static implicit operator int(OBJECT_ID value)
		{
			return value.value;
		}

		// Token: 0x06005387 RID: 21383 RVA: 0x00233D0A File Offset: 0x00231F0A
		public static implicit operator OBJECT_ID(int value)
		{
			return new OBJECT_ID(value);
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06005388 RID: 21384 RVA: 0x002339E0 File Offset: 0x00231BE0
		public static int MaxValue
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06005389 RID: 21385 RVA: 0x002339E7 File Offset: 0x00231BE7
		public static int MinValue
		{
			get
			{
				return int.MinValue;
			}
		}

		// Token: 0x0400503B RID: 20539
		private int value;
	}
}
