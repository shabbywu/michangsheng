using System;

namespace KBEngine
{
	// Token: 0x02000F21 RID: 3873
	public struct INT8
	{
		// Token: 0x06005D66 RID: 23910 RVA: 0x00041AE0 File Offset: 0x0003FCE0
		private INT8(sbyte value)
		{
			this.value = value;
		}

		// Token: 0x06005D67 RID: 23911 RVA: 0x00041AE9 File Offset: 0x0003FCE9
		public static implicit operator sbyte(INT8 value)
		{
			return value.value;
		}

		// Token: 0x06005D68 RID: 23912 RVA: 0x00041AF1 File Offset: 0x0003FCF1
		public static implicit operator INT8(sbyte value)
		{
			return new INT8(value);
		}

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x06005D69 RID: 23913 RVA: 0x00041AF9 File Offset: 0x0003FCF9
		public static sbyte MaxValue
		{
			get
			{
				return sbyte.MaxValue;
			}
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x06005D6A RID: 23914 RVA: 0x00041AFD File Offset: 0x0003FCFD
		public static sbyte MinValue
		{
			get
			{
				return sbyte.MinValue;
			}
		}

		// Token: 0x04005ACB RID: 23243
		private sbyte value;
	}
}
