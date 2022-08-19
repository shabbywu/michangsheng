using System;

namespace MoonSharp.Interpreter.Interop.RegistrationPolicies
{
	// Token: 0x02000D33 RID: 3379
	public class PermanentRegistrationPolicy : IRegistrationPolicy
	{
		// Token: 0x06005ED4 RID: 24276 RVA: 0x00268B3F File Offset: 0x00266D3F
		public IUserDataDescriptor HandleRegistration(IUserDataDescriptor newDescriptor, IUserDataDescriptor oldDescriptor)
		{
			return oldDescriptor ?? newDescriptor;
		}

		// Token: 0x06005ED5 RID: 24277 RVA: 0x0000280F File Offset: 0x00000A0F
		public bool AllowTypeAutoRegistration(Type type)
		{
			return false;
		}
	}
}
