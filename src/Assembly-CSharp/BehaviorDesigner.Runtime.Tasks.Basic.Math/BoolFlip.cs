namespace BehaviorDesigner.Runtime.Tasks.Basic.Math;

[TaskCategory("Basic/Math")]
[TaskDescription("Flips the value of the bool.")]
public class BoolFlip : Action
{
	[Tooltip("The bool to flip the value of")]
	public SharedBool boolVariable;

	public override TaskStatus OnUpdate()
	{
		((SharedVariable<bool>)boolVariable).Value = !((SharedVariable<bool>)boolVariable).Value;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		((SharedVariable<bool>)boolVariable).Value = false;
	}
}
