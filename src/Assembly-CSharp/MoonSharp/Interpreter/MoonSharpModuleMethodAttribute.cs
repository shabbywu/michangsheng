using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x02000CC0 RID: 3264
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
	public sealed class MoonSharpModuleMethodAttribute : Attribute
	{
		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06005B71 RID: 23409 RVA: 0x0025A65F File Offset: 0x0025885F
		// (set) Token: 0x06005B72 RID: 23410 RVA: 0x0025A667 File Offset: 0x00258867
		public string Name { get; set; }
	}
}
