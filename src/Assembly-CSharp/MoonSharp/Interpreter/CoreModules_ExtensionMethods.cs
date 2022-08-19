using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x02000CBC RID: 3260
	internal static class CoreModules_ExtensionMethods
	{
		// Token: 0x06005B63 RID: 23395 RVA: 0x00259EFE File Offset: 0x002580FE
		public static bool Has(this CoreModules val, CoreModules flag)
		{
			return (val & flag) == flag;
		}
	}
}
