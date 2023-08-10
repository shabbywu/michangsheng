namespace BehaviorDesigner.Runtime.Tasks.Basic.Math;

[TaskCategory("Basic/Math")]
[TaskDescription("Sets an int value")]
public class SetInt : Action
{
	[Tooltip("The int value to set")]
	public SharedInt intValue;

	[Tooltip("The variable to store the result")]
	public SharedInt storeResult;

	public override TaskStatus OnUpdate()
	{
		((SharedVariable<int>)storeResult).Value = ((SharedVariable<int>)intValue).Value;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		((SharedVariable<int>)intValue).Value = 0;
		((SharedVariable<int>)storeResult).Value = 0;
	}
}
