using System;

namespace Fungus
{
	// Token: 0x02001460 RID: 5216
	[Serializable]
	public class FungusVariable
	{
		// Token: 0x06007DC3 RID: 32195 RVA: 0x00055014 File Offset: 0x00053214
		public FungusVariable(VariableScope scope, string key)
		{
			this.scope = scope;
			this.key = key;
		}

		// Token: 0x04006B43 RID: 27459
		public VariableScope scope;

		// Token: 0x04006B44 RID: 27460
		public string key = "";
	}
}
