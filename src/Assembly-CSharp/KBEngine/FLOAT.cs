using System;

namespace KBEngine
{
	// Token: 0x02000BA4 RID: 2980
	public struct FLOAT
	{
		// Token: 0x06005342 RID: 21314 RVA: 0x00233A4F File Offset: 0x00231C4F
		private FLOAT(float value)
		{
			this.value = value;
		}

		// Token: 0x06005343 RID: 21315 RVA: 0x00233A58 File Offset: 0x00231C58
		public static implicit operator float(FLOAT value)
		{
			return value.value;
		}

		// Token: 0x06005344 RID: 21316 RVA: 0x00233A60 File Offset: 0x00231C60
		public static implicit operator FLOAT(float value)
		{
			return new FLOAT(value);
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06005345 RID: 21317 RVA: 0x00233A69 File Offset: 0x00231C69
		public static float MaxValue
		{
			get
			{
				return float.MaxValue;
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06005346 RID: 21318 RVA: 0x00233A70 File Offset: 0x00231C70
		public static float MinValue
		{
			get
			{
				return float.MinValue;
			}
		}

		// Token: 0x04005030 RID: 20528
		private float value;
	}
}
