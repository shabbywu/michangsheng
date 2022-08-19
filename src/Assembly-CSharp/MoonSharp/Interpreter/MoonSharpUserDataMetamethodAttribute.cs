using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x02000CB7 RID: 3255
	[AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
	public sealed class MoonSharpUserDataMetamethodAttribute : Attribute
	{
		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06005B54 RID: 23380 RVA: 0x00259D65 File Offset: 0x00257F65
		// (set) Token: 0x06005B55 RID: 23381 RVA: 0x00259D6D File Offset: 0x00257F6D
		public string Name { get; private set; }

		// Token: 0x06005B56 RID: 23382 RVA: 0x00259D76 File Offset: 0x00257F76
		public MoonSharpUserDataMetamethodAttribute(string name)
		{
			this.Name = name;
		}
	}
}
