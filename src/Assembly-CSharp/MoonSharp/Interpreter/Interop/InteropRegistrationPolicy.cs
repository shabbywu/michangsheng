using System;
using MoonSharp.Interpreter.Interop.RegistrationPolicies;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x020010ED RID: 4333
	public static class InteropRegistrationPolicy
	{
		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x060068A1 RID: 26785 RVA: 0x00047C53 File Offset: 0x00045E53
		public static IRegistrationPolicy Default
		{
			get
			{
				return new DefaultRegistrationPolicy();
			}
		}

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x060068A2 RID: 26786 RVA: 0x00047C53 File Offset: 0x00045E53
		[Obsolete("Please use InteropRegistrationPolicy.Default instead.")]
		public static IRegistrationPolicy Explicit
		{
			get
			{
				return new DefaultRegistrationPolicy();
			}
		}

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x060068A3 RID: 26787 RVA: 0x00047C5A File Offset: 0x00045E5A
		public static IRegistrationPolicy Automatic
		{
			get
			{
				return new AutomaticRegistrationPolicy();
			}
		}
	}
}
