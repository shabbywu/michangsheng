using System;

namespace KBEngine
{
	// Token: 0x02000BA2 RID: 2978
	public struct STRING
	{
		// Token: 0x0600533C RID: 21308 RVA: 0x00233A1D File Offset: 0x00231C1D
		private STRING(string value)
		{
			this.value = value;
		}

		// Token: 0x0600533D RID: 21309 RVA: 0x00233A26 File Offset: 0x00231C26
		public static implicit operator string(STRING value)
		{
			return value.value;
		}

		// Token: 0x0600533E RID: 21310 RVA: 0x00233A2E File Offset: 0x00231C2E
		public static implicit operator STRING(string value)
		{
			return new STRING(value);
		}

		// Token: 0x0400502E RID: 20526
		private string value;
	}
}
