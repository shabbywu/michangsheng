using System;
using System.Text;

namespace Fungus
{
	// Token: 0x020013A7 RID: 5031
	public interface IStringSubstituter
	{
		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x060079E1 RID: 31201
		StringBuilder _StringBuilder { get; }

		// Token: 0x060079E2 RID: 31202
		string SubstituteStrings(string input);

		// Token: 0x060079E3 RID: 31203
		bool SubstituteStrings(StringBuilder input);
	}
}
