using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x02001093 RID: 4243
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
	public sealed class MoonSharpModuleAttribute : Attribute
	{
		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x0600666F RID: 26223 RVA: 0x00046AB3 File Offset: 0x00044CB3
		// (set) Token: 0x06006670 RID: 26224 RVA: 0x00046ABB File Offset: 0x00044CBB
		public string Namespace { get; set; }
	}
}
