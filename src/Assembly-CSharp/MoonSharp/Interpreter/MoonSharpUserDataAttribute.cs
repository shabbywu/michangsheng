using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x02000CB6 RID: 3254
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
	public sealed class MoonSharpUserDataAttribute : Attribute
	{
		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06005B51 RID: 23377 RVA: 0x00259D45 File Offset: 0x00257F45
		// (set) Token: 0x06005B52 RID: 23378 RVA: 0x00259D4D File Offset: 0x00257F4D
		public InteropAccessMode AccessMode { get; set; }

		// Token: 0x06005B53 RID: 23379 RVA: 0x00259D56 File Offset: 0x00257F56
		public MoonSharpUserDataAttribute()
		{
			this.AccessMode = InteropAccessMode.Default;
		}
	}
}
