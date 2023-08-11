namespace MoonSharp.Interpreter;

public enum InteropAccessMode
{
	Reflection,
	LazyOptimized,
	Preoptimized,
	BackgroundOptimized,
	Hardwired,
	HideMembers,
	NoReflectionAllowed,
	Default
}
