using System;

namespace MoonSharp.Interpreter.Interop.RegistrationPolicies
{
	// Token: 0x02000D30 RID: 3376
	public class AutomaticRegistrationPolicy : DefaultRegistrationPolicy
	{
		// Token: 0x06005ECD RID: 24269 RVA: 0x00024C5F File Offset: 0x00022E5F
		public override bool AllowTypeAutoRegistration(Type type)
		{
			return true;
		}
	}
}
