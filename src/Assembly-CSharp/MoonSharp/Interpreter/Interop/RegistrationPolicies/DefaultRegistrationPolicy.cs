using System;

namespace MoonSharp.Interpreter.Interop.RegistrationPolicies
{
	// Token: 0x02001139 RID: 4409
	public class DefaultRegistrationPolicy : IRegistrationPolicy
	{
		// Token: 0x06006AA1 RID: 27297 RVA: 0x00048B49 File Offset: 0x00046D49
		public IUserDataDescriptor HandleRegistration(IUserDataDescriptor newDescriptor, IUserDataDescriptor oldDescriptor)
		{
			if (newDescriptor == null)
			{
				return null;
			}
			return oldDescriptor ?? newDescriptor;
		}

		// Token: 0x06006AA2 RID: 27298 RVA: 0x00004050 File Offset: 0x00002250
		public virtual bool AllowTypeAutoRegistration(Type type)
		{
			return false;
		}
	}
}
