using System;

namespace KBEngine
{
	// Token: 0x02000F25 RID: 3877
	public struct STRING
	{
		// Token: 0x06005D7A RID: 23930 RVA: 0x00041B7E File Offset: 0x0003FD7E
		private STRING(string value)
		{
			this.value = value;
		}

		// Token: 0x06005D7B RID: 23931 RVA: 0x00041B87 File Offset: 0x0003FD87
		public static implicit operator string(STRING value)
		{
			return value.value;
		}

		// Token: 0x06005D7C RID: 23932 RVA: 0x00041B8F File Offset: 0x0003FD8F
		public static implicit operator STRING(string value)
		{
			return new STRING(value);
		}

		// Token: 0x04005ACF RID: 23247
		private string value;
	}
}
