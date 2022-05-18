using System;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x020010E4 RID: 4324
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event, Inherited = true, AllowMultiple = false)]
	public sealed class MoonSharpVisibleAttribute : Attribute
	{
		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x06006874 RID: 26740 RVA: 0x00047B40 File Offset: 0x00045D40
		// (set) Token: 0x06006875 RID: 26741 RVA: 0x00047B48 File Offset: 0x00045D48
		public bool Visible { get; private set; }

		// Token: 0x06006876 RID: 26742 RVA: 0x00047B51 File Offset: 0x00045D51
		public MoonSharpVisibleAttribute(bool visible)
		{
			this.Visible = visible;
		}
	}
}
