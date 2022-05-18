using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x02001085 RID: 4229
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
	public sealed class MoonSharpUserDataAttribute : Attribute
	{
		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x06006643 RID: 26179 RVA: 0x00046916 File Offset: 0x00044B16
		// (set) Token: 0x06006644 RID: 26180 RVA: 0x0004691E File Offset: 0x00044B1E
		public InteropAccessMode AccessMode { get; set; }

		// Token: 0x06006645 RID: 26181 RVA: 0x00046927 File Offset: 0x00044B27
		public MoonSharpUserDataAttribute()
		{
			this.AccessMode = InteropAccessMode.Default;
		}
	}
}
