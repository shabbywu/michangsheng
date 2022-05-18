using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x02001095 RID: 4245
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
	public sealed class MoonSharpModuleMethodAttribute : Attribute
	{
		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x06006675 RID: 26229 RVA: 0x00046AD5 File Offset: 0x00044CD5
		// (set) Token: 0x06006676 RID: 26230 RVA: 0x00046ADD File Offset: 0x00044CDD
		public string Name { get; set; }
	}
}
