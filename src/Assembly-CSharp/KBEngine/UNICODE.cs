using System;

namespace KBEngine
{
	// Token: 0x02000F26 RID: 3878
	public struct UNICODE
	{
		// Token: 0x06005D7D RID: 23933 RVA: 0x00041B97 File Offset: 0x0003FD97
		private UNICODE(string value)
		{
			this.value = value;
		}

		// Token: 0x06005D7E RID: 23934 RVA: 0x00041BA0 File Offset: 0x0003FDA0
		public static implicit operator string(UNICODE value)
		{
			return value.value;
		}

		// Token: 0x06005D7F RID: 23935 RVA: 0x00041BA8 File Offset: 0x0003FDA8
		public static implicit operator UNICODE(string value)
		{
			return new UNICODE(value);
		}

		// Token: 0x04005AD0 RID: 23248
		private string value;
	}
}
