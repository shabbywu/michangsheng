using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x02000CB4 RID: 3252
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = true, AllowMultiple = true)]
	public sealed class MoonSharpHideMemberAttribute : Attribute
	{
		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06005B4A RID: 23370 RVA: 0x00259D05 File Offset: 0x00257F05
		// (set) Token: 0x06005B4B RID: 23371 RVA: 0x00259D0D File Offset: 0x00257F0D
		public string MemberName { get; private set; }

		// Token: 0x06005B4C RID: 23372 RVA: 0x00259D16 File Offset: 0x00257F16
		public MoonSharpHideMemberAttribute(string memberName)
		{
			this.MemberName = memberName;
		}
	}
}
