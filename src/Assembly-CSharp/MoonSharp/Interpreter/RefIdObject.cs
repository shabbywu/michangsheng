using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x02000CA0 RID: 3232
	public class RefIdObject
	{
		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06005A6A RID: 23146 RVA: 0x00258374 File Offset: 0x00256574
		public int ReferenceID
		{
			get
			{
				return this.m_RefID;
			}
		}

		// Token: 0x06005A6B RID: 23147 RVA: 0x0025837C File Offset: 0x0025657C
		public string FormatTypeString(string typeString)
		{
			return string.Format("{0}: {1:X8}", typeString, this.m_RefID);
		}

		// Token: 0x0400527E RID: 21118
		private static int s_RefIDCounter;

		// Token: 0x0400527F RID: 21119
		private int m_RefID = ++RefIdObject.s_RefIDCounter;
	}
}
