using System;
using System.Text;

namespace Fungus
{
	// Token: 0x02000F02 RID: 3842
	public interface IStringSubstituter
	{
		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x06006C2D RID: 27693
		StringBuilder _StringBuilder { get; }

		// Token: 0x06006C2E RID: 27694
		string SubstituteStrings(string input);

		// Token: 0x06006C2F RID: 27695
		bool SubstituteStrings(StringBuilder input);
	}
}
