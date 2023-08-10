using System;
using MoonSharp.Interpreter.Interop.RegistrationPolicies;

namespace MoonSharp.Interpreter.Interop;

public static class InteropRegistrationPolicy
{
	public static IRegistrationPolicy Default => new DefaultRegistrationPolicy();

	[Obsolete("Please use InteropRegistrationPolicy.Default instead.")]
	public static IRegistrationPolicy Explicit => new DefaultRegistrationPolicy();

	public static IRegistrationPolicy Automatic => new AutomaticRegistrationPolicy();
}
