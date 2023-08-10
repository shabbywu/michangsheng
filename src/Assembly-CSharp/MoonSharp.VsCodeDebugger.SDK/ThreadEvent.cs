namespace MoonSharp.VsCodeDebugger.SDK;

public class ThreadEvent : Event
{
	public ThreadEvent(string reasn, int tid)
		: base("thread", new
		{
			reason = reasn,
			threadId = tid
		})
	{
	}
}
