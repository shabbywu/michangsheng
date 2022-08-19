using System;

namespace Fungus
{
	// Token: 0x02000FAA RID: 4010
	[Serializable]
	public class FungusVariable
	{
		// Token: 0x06006FD5 RID: 28629 RVA: 0x002A810F File Offset: 0x002A630F
		public FungusVariable(VariableScope scope, string key)
		{
			this.scope = scope;
			this.key = key;
		}

		// Token: 0x04005C4F RID: 23631
		public VariableScope scope;

		// Token: 0x04005C50 RID: 23632
		public string key = "";
	}
}
