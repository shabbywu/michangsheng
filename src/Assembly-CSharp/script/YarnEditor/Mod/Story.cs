using System;
using System.Collections.Generic;

namespace script.YarnEditor.Mod
{
	// Token: 0x020009C5 RID: 2501
	[Serializable]
	public class Story
	{
		// Token: 0x04004710 RID: 18192
		public string ModName;

		// Token: 0x04004711 RID: 18193
		public Dictionary<string, Yarn> YarnDict = new Dictionary<string, Yarn>();
	}
}
