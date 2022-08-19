using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x02000CBF RID: 3263
	[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
	public sealed class MoonSharpModuleConstantAttribute : Attribute
	{
		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06005B6E RID: 23406 RVA: 0x0025A64E File Offset: 0x0025884E
		// (set) Token: 0x06005B6F RID: 23407 RVA: 0x0025A656 File Offset: 0x00258856
		public string Name { get; set; }
	}
}
