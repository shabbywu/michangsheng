using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x02000CB5 RID: 3253
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
	public sealed class MoonSharpPropertyAttribute : Attribute
	{
		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06005B4D RID: 23373 RVA: 0x00259D25 File Offset: 0x00257F25
		// (set) Token: 0x06005B4E RID: 23374 RVA: 0x00259D2D File Offset: 0x00257F2D
		public string Name { get; private set; }

		// Token: 0x06005B4F RID: 23375 RVA: 0x00052C2A File Offset: 0x00050E2A
		public MoonSharpPropertyAttribute()
		{
		}

		// Token: 0x06005B50 RID: 23376 RVA: 0x00259D36 File Offset: 0x00257F36
		public MoonSharpPropertyAttribute(string name)
		{
			this.Name = name;
		}
	}
}
