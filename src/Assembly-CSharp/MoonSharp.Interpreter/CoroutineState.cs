namespace MoonSharp.Interpreter;

public enum CoroutineState
{
	Main,
	NotStarted,
	Suspended,
	ForceSuspended,
	Running,
	Dead
}
