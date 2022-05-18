using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x02001090 RID: 4240
	internal static class CoreModules_ExtensionMethods
	{
		// Token: 0x06006662 RID: 26210 RVA: 0x00046A39 File Offset: 0x00044C39
		public static bool Has(this CoreModules val, CoreModules flag)
		{
			return (val & flag) == flag;
		}
	}
}
