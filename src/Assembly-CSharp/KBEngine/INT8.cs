using System;

namespace KBEngine
{
	// Token: 0x02000B9E RID: 2974
	public struct INT8
	{
		// Token: 0x06005328 RID: 21288 RVA: 0x0023397F File Offset: 0x00231B7F
		private INT8(sbyte value)
		{
			this.value = value;
		}

		// Token: 0x06005329 RID: 21289 RVA: 0x00233988 File Offset: 0x00231B88
		public static implicit operator sbyte(INT8 value)
		{
			return value.value;
		}

		// Token: 0x0600532A RID: 21290 RVA: 0x00233990 File Offset: 0x00231B90
		public static implicit operator INT8(sbyte value)
		{
			return new INT8(value);
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x0600532B RID: 21291 RVA: 0x00233998 File Offset: 0x00231B98
		public static sbyte MaxValue
		{
			get
			{
				return sbyte.MaxValue;
			}
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x0600532C RID: 21292 RVA: 0x0023399C File Offset: 0x00231B9C
		public static sbyte MinValue
		{
			get
			{
				return sbyte.MinValue;
			}
		}

		// Token: 0x0400502A RID: 20522
		private sbyte value;
	}
}
