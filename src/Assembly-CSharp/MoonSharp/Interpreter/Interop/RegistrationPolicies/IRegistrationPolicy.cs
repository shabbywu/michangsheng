using System;

namespace MoonSharp.Interpreter.Interop.RegistrationPolicies
{
	// Token: 0x02000D32 RID: 3378
	public interface IRegistrationPolicy
	{
		// Token: 0x06005ED2 RID: 24274
		IUserDataDescriptor HandleRegistration(IUserDataDescriptor newDescriptor, IUserDataDescriptor oldDescriptor);

		// Token: 0x06005ED3 RID: 24275
		bool AllowTypeAutoRegistration(Type type);
	}
}
