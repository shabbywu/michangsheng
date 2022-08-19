using System;

namespace KBEngine
{
	// Token: 0x02000BA0 RID: 2976
	public struct INT32
	{
		// Token: 0x06005332 RID: 21298 RVA: 0x002339C7 File Offset: 0x00231BC7
		private INT32(int value)
		{
			this.value = value;
		}

		// Token: 0x06005333 RID: 21299 RVA: 0x002339D0 File Offset: 0x00231BD0
		public static implicit operator int(INT32 value)
		{
			return value.value;
		}

		// Token: 0x06005334 RID: 21300 RVA: 0x002339D8 File Offset: 0x00231BD8
		public static implicit operator INT32(int value)
		{
			return new INT32(value);
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06005335 RID: 21301 RVA: 0x002339E0 File Offset: 0x00231BE0
		public static int MaxValue
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06005336 RID: 21302 RVA: 0x002339E7 File Offset: 0x00231BE7
		public static int MinValue
		{
			get
			{
				return int.MinValue;
			}
		}

		// Token: 0x0400502C RID: 20524
		private int value;
	}
}
