using System;

namespace MoonSharp.Interpreter.Interop.BasicDescriptors
{
	// Token: 0x0200114D RID: 4429
	public interface IMemberDescriptor
	{
		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x06006B89 RID: 27529
		bool IsStatic { get; }

		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x06006B8A RID: 27530
		string Name { get; }

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x06006B8B RID: 27531
		MemberDescriptorAccess MemberAccess { get; }

		// Token: 0x06006B8C RID: 27532
		DynValue GetValue(Script script, object obj);

		// Token: 0x06006B8D RID: 27533
		void SetValue(Script script, object obj, DynValue value);
	}
}
