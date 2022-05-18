using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x02001084 RID: 4228
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
	public sealed class MoonSharpPropertyAttribute : Attribute
	{
		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x0600663F RID: 26175 RVA: 0x000468F6 File Offset: 0x00044AF6
		// (set) Token: 0x06006640 RID: 26176 RVA: 0x000468FE File Offset: 0x00044AFE
		public string Name { get; private set; }

		// Token: 0x06006641 RID: 26177 RVA: 0x00010224 File Offset: 0x0000E424
		public MoonSharpPropertyAttribute()
		{
		}

		// Token: 0x06006642 RID: 26178 RVA: 0x00046907 File Offset: 0x00044B07
		public MoonSharpPropertyAttribute(string name)
		{
			this.Name = name;
		}
	}
}
