using MoonSharp.Interpreter.Compatibility.Frameworks;

namespace MoonSharp.Interpreter.Compatibility;

public static class Framework
{
	private static FrameworkCurrent s_FrameworkCurrent = new FrameworkCurrent();

	public static FrameworkBase Do => s_FrameworkCurrent;
}
