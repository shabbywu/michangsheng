using System;

namespace MoonSharp.Interpreter.Interop.RegistrationPolicies
{
	// Token: 0x0200113A RID: 4410
	public interface IRegistrationPolicy
	{
		// Token: 0x06006AA4 RID: 27300
		IUserDataDescriptor HandleRegistration(IUserDataDescriptor newDescriptor, IUserDataDescriptor oldDescriptor);

		// Token: 0x06006AA5 RID: 27301
		bool AllowTypeAutoRegistration(Type type);
	}
}
