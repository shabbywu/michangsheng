namespace Fungus;

[CommandInfo("Priority Signals", "Priority Up", "Increases the FungusPriority count, causing the related FungusPrioritySignals to fire. Intended to be used to notify external systems that fungus is doing something important and they should perhaps pause.", 0)]
public class FungusPriorityIncrease : Command
{
	public override void OnEnter()
	{
		FungusPrioritySignals.DoIncreasePriorityDepth();
		Continue();
	}
}
