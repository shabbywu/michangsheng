using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x02001083 RID: 4227
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = true, AllowMultiple = true)]
	public sealed class MoonSharpHideMemberAttribute : Attribute
	{
		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x0600663C RID: 26172 RVA: 0x000468D6 File Offset: 0x00044AD6
		// (set) Token: 0x0600663D RID: 26173 RVA: 0x000468DE File Offset: 0x00044ADE
		public string MemberName { get; private set; }

		// Token: 0x0600663E RID: 26174 RVA: 0x000468E7 File Offset: 0x00044AE7
		public MoonSharpHideMemberAttribute(string memberName)
		{
			this.MemberName = memberName;
		}
	}
}
