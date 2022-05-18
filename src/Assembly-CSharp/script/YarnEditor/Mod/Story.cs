using System;
using System.Collections.Generic;

namespace script.YarnEditor.Mod
{
	// Token: 0x02000AAA RID: 2730
	[Serializable]
	public class Story
	{
		// Token: 0x04003E1C RID: 15900
		public string ModName;

		// Token: 0x04003E1D RID: 15901
		public Dictionary<string, Yarn> YarnDict = new Dictionary<string, Yarn>();
	}
}
