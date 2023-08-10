namespace Fungus;

[CommandInfo("Priority Signals", "Priority Down", "Decrease the FungusPriority count, causing the related FungusPrioritySignals to fire. Intended to be used to notify external systems that fungus is doing something important and they should perhaps resume.", 0)]
public class FungusPriorityDecrease : Command
{
	public override void OnEnter()
	{
		FungusPrioritySignals.DoDecreasePriorityDepth();
		Continue();
	}
}
