using System;

namespace MoonSharp.Interpreter.Interop.BasicDescriptors
{
	// Token: 0x02000D40 RID: 3392
	public interface IMemberDescriptor
	{
		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06005FA7 RID: 24487
		bool IsStatic { get; }

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x06005FA8 RID: 24488
		string Name { get; }

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06005FA9 RID: 24489
		MemberDescriptorAccess MemberAccess { get; }

		// Token: 0x06005FAA RID: 24490
		DynValue GetValue(Script script, object obj);

		// Token: 0x06005FAB RID: 24491
		void SetValue(Script script, object obj, DynValue value);
	}
}
