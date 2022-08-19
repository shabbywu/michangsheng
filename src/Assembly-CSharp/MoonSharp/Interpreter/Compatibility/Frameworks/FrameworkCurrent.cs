using System;
using System.Linq;

namespace MoonSharp.Interpreter.Compatibility.Frameworks
{
	// Token: 0x02000D91 RID: 3473
	internal class FrameworkCurrent : FrameworkClrBase
	{
		// Token: 0x060062E8 RID: 25320 RVA: 0x00279CF0 File Offset: 0x00277EF0
		public override bool IsDbNull(object o)
		{
			return o != null && Convert.IsDBNull(o);
		}

		// Token: 0x060062E9 RID: 25321 RVA: 0x00279CFD File Offset: 0x00277EFD
		public override bool StringContainsChar(string str, char chr)
		{
			return str.Contains(chr);
		}

		// Token: 0x060062EA RID: 25322 RVA: 0x00279D06 File Offset: 0x00277F06
		public override Type GetInterface(Type type, string name)
		{
			return type.GetInterface(name);
		}
	}
}
