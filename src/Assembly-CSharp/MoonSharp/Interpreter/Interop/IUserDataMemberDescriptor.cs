using System;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x020010EF RID: 4335
	public interface IUserDataMemberDescriptor
	{
		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x060068AB RID: 26795
		string Name { get; }

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x060068AC RID: 26796
		Type Type { get; }

		// Token: 0x060068AD RID: 26797
		DynValue GetValue(Script script, object obj);

		// Token: 0x060068AE RID: 26798
		bool SetValue(Script script, object obj, DynValue value);

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x060068AF RID: 26799
		UserDataMemberType MemberType { get; }

		// Token: 0x060068B0 RID: 26800
		void Optimize();

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x060068B1 RID: 26801
		bool IsStatic { get; }
	}
}
