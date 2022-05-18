using System;

namespace MoonSharp.Interpreter.Interop.RegistrationPolicies
{
	// Token: 0x0200113B RID: 4411
	public class PermanentRegistrationPolicy : IRegistrationPolicy
	{
		// Token: 0x06006AA6 RID: 27302 RVA: 0x00048B56 File Offset: 0x00046D56
		public IUserDataDescriptor HandleRegistration(IUserDataDescriptor newDescriptor, IUserDataDescriptor oldDescriptor)
		{
			return oldDescriptor ?? newDescriptor;
		}

		// Token: 0x06006AA7 RID: 27303 RVA: 0x00004050 File Offset: 0x00002250
		public bool AllowTypeAutoRegistration(Type type)
		{
			return false;
		}
	}
}
