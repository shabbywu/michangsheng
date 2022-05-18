using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x02001094 RID: 4244
	[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
	public sealed class MoonSharpModuleConstantAttribute : Attribute
	{
		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x06006672 RID: 26226 RVA: 0x00046AC4 File Offset: 0x00044CC4
		// (set) Token: 0x06006673 RID: 26227 RVA: 0x00046ACC File Offset: 0x00044CCC
		public string Name { get; set; }
	}
}
