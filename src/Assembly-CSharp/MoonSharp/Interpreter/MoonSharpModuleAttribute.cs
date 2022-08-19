using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x02000CBE RID: 3262
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
	public sealed class MoonSharpModuleAttribute : Attribute
	{
		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06005B6B RID: 23403 RVA: 0x0025A63D File Offset: 0x0025883D
		// (set) Token: 0x06005B6C RID: 23404 RVA: 0x0025A645 File Offset: 0x00258845
		public string Namespace { get; set; }
	}
}
