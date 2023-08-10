namespace Fungus;

[CommandInfo("Priority Signals", "Priority Reset", "Resets the FungusPriority count to zero. Useful if you are among logic that is hard to have matching increase and decreases.", 0)]
public class FungusPriorityReset : Command
{
	public override void OnEnter()
	{
		FungusPrioritySignals.DoResetPriority();
		Continue();
	}
}
