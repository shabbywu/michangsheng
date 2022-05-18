using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x0200106D RID: 4205
	public class RefIdObject
	{
		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x06006554 RID: 25940 RVA: 0x00045B89 File Offset: 0x00043D89
		public int ReferenceID
		{
			get
			{
				return this.m_RefID;
			}
		}

		// Token: 0x06006555 RID: 25941 RVA: 0x00045B91 File Offset: 0x00043D91
		public string FormatTypeString(string typeString)
		{
			return string.Format("{0}: {1:X8}", typeString, this.m_RefID);
		}

		// Token: 0x04005E4D RID: 24141
		private static int s_RefIDCounter;

		// Token: 0x04005E4E RID: 24142
		private int m_RefID = ++RefIdObject.s_RefIDCounter;
	}
}
