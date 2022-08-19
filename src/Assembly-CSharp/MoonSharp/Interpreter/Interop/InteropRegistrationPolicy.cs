using System;
using MoonSharp.Interpreter.Interop.RegistrationPolicies;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x02000D09 RID: 3337
	public static class InteropRegistrationPolicy
	{
		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06005D72 RID: 23922 RVA: 0x00262EC7 File Offset: 0x002610C7
		public static IRegistrationPolicy Default
		{
			get
			{
				return new DefaultRegistrationPolicy();
			}
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06005D73 RID: 23923 RVA: 0x00262EC7 File Offset: 0x002610C7
		[Obsolete("Please use InteropRegistrationPolicy.Default instead.")]
		public static IRegistrationPolicy Explicit
		{
			get
			{
				return new DefaultRegistrationPolicy();
			}
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06005D74 RID: 23924 RVA: 0x00262ECE File Offset: 0x002610CE
		public static IRegistrationPolicy Automatic
		{
			get
			{
				return new AutomaticRegistrationPolicy();
			}
		}
	}
}
