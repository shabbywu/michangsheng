using System;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x02000D05 RID: 3333
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event, Inherited = true, AllowMultiple = false)]
	public sealed class MoonSharpVisibleAttribute : Attribute
	{
		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06005D56 RID: 23894 RVA: 0x002627B5 File Offset: 0x002609B5
		// (set) Token: 0x06005D57 RID: 23895 RVA: 0x002627BD File Offset: 0x002609BD
		public bool Visible { get; private set; }

		// Token: 0x06005D58 RID: 23896 RVA: 0x002627C6 File Offset: 0x002609C6
		public MoonSharpVisibleAttribute(bool visible)
		{
			this.Visible = visible;
		}
	}
}
