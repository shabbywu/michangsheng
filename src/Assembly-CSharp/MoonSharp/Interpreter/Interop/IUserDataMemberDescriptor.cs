using System;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x02000D0B RID: 3339
	public interface IUserDataMemberDescriptor
	{
		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06005D7C RID: 23932
		string Name { get; }

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06005D7D RID: 23933
		Type Type { get; }

		// Token: 0x06005D7E RID: 23934
		DynValue GetValue(Script script, object obj);

		// Token: 0x06005D7F RID: 23935
		bool SetValue(Script script, object obj, DynValue value);

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06005D80 RID: 23936
		UserDataMemberType MemberType { get; }

		// Token: 0x06005D81 RID: 23937
		void Optimize();

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06005D82 RID: 23938
		bool IsStatic { get; }
	}
}
