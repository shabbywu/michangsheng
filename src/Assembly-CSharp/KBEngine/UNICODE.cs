using System;

namespace KBEngine
{
	// Token: 0x02000BA3 RID: 2979
	public struct UNICODE
	{
		// Token: 0x0600533F RID: 21311 RVA: 0x00233A36 File Offset: 0x00231C36
		private UNICODE(string value)
		{
			this.value = value;
		}

		// Token: 0x06005340 RID: 21312 RVA: 0x00233A3F File Offset: 0x00231C3F
		public static implicit operator string(UNICODE value)
		{
			return value.value;
		}

		// Token: 0x06005341 RID: 21313 RVA: 0x00233A47 File Offset: 0x00231C47
		public static implicit operator UNICODE(string value)
		{
			return new UNICODE(value);
		}

		// Token: 0x0400502F RID: 20527
		private string value;
	}
}
