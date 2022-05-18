using System;
using System.Linq;

namespace MoonSharp.Interpreter.Compatibility.Frameworks
{
	// Token: 0x020011B4 RID: 4532
	internal class FrameworkCurrent : FrameworkClrBase
	{
		// Token: 0x06006F19 RID: 28441 RVA: 0x0004B82A File Offset: 0x00049A2A
		public override bool IsDbNull(object o)
		{
			return o != null && Convert.IsDBNull(o);
		}

		// Token: 0x06006F1A RID: 28442 RVA: 0x0004B837 File Offset: 0x00049A37
		public override bool StringContainsChar(string str, char chr)
		{
			return str.Contains(chr);
		}

		// Token: 0x06006F1B RID: 28443 RVA: 0x0004B840 File Offset: 0x00049A40
		public override Type GetInterface(Type type, string name)
		{
			return type.GetInterface(name);
		}
	}
}
