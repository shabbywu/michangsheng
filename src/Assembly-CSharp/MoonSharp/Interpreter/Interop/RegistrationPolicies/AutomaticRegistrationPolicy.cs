using System;

namespace MoonSharp.Interpreter.Interop.RegistrationPolicies
{
	// Token: 0x02001138 RID: 4408
	public class AutomaticRegistrationPolicy : DefaultRegistrationPolicy
	{
		// Token: 0x06006A9F RID: 27295 RVA: 0x0000A093 File Offset: 0x00008293
		public override bool AllowTypeAutoRegistration(Type type)
		{
			return true;
		}
	}
}
