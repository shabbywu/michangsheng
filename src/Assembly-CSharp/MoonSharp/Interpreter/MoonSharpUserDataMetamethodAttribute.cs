using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x02001086 RID: 4230
	[AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
	public sealed class MoonSharpUserDataMetamethodAttribute : Attribute
	{
		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x06006646 RID: 26182 RVA: 0x00046936 File Offset: 0x00044B36
		// (set) Token: 0x06006647 RID: 26183 RVA: 0x0004693E File Offset: 0x00044B3E
		public string Name { get; private set; }

		// Token: 0x06006648 RID: 26184 RVA: 0x00046947 File Offset: 0x00044B47
		public MoonSharpUserDataMetamethodAttribute(string name)
		{
			this.Name = name;
		}
	}
}
