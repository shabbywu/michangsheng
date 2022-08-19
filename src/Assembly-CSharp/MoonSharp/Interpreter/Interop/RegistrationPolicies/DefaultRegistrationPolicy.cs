using System;

namespace MoonSharp.Interpreter.Interop.RegistrationPolicies
{
	// Token: 0x02000D31 RID: 3377
	public class DefaultRegistrationPolicy : IRegistrationPolicy
	{
		// Token: 0x06005ECF RID: 24271 RVA: 0x00268B32 File Offset: 0x00266D32
		public IUserDataDescriptor HandleRegistration(IUserDataDescriptor newDescriptor, IUserDataDescriptor oldDescriptor)
		{
			if (newDescriptor == null)
			{
				return null;
			}
			return oldDescriptor ?? newDescriptor;
		}

		// Token: 0x06005ED0 RID: 24272 RVA: 0x0000280F File Offset: 0x00000A0F
		public virtual bool AllowTypeAutoRegistration(Type type)
		{
			return false;
		}
	}
}
