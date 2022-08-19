using System;

namespace KBEngine
{
	// Token: 0x02000BB4 RID: 2996
	public struct SKILLID
	{
		// Token: 0x0600539E RID: 21406 RVA: 0x00233D76 File Offset: 0x00231F76
		private SKILLID(int value)
		{
			this.value = value;
		}

		// Token: 0x0600539F RID: 21407 RVA: 0x00233D7F File Offset: 0x00231F7F
		public static implicit operator int(SKILLID value)
		{
			return value.value;
		}

		// Token: 0x060053A0 RID: 21408 RVA: 0x00233D87 File Offset: 0x00231F87
		public static implicit operator SKILLID(int value)
		{
			return new SKILLID(value);
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x060053A1 RID: 21409 RVA: 0x002339E0 File Offset: 0x00231BE0
		public static int MaxValue
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x060053A2 RID: 21410 RVA: 0x002339E7 File Offset: 0x00231BE7
		public static int MinValue
		{
			get
			{
				return int.MinValue;
			}
		}

		// Token: 0x04005040 RID: 20544
		private int value;
	}
}
