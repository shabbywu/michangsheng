namespace MoonSharp.VsCodeDebugger.SDK;

public class ExitedEvent : Event
{
	public ExitedEvent(int exCode)
		: base("exited", new
		{
			exitCode = exCode
		})
	{
	}
}
