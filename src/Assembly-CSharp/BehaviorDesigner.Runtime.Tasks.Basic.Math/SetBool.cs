namespace BehaviorDesigner.Runtime.Tasks.Basic.Math;

[TaskCategory("Basic/Math")]
[TaskDescription("Sets a bool value")]
public class SetBool : Action
{
	[Tooltip("The bool value to set")]
	public SharedBool boolValue;

	[Tooltip("The variable to store the result")]
	public SharedBool storeResult;

	public override TaskStatus OnUpdate()
	{
		((SharedVariable<bool>)storeResult).Value = ((SharedVariable<bool>)boolValue).Value;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		((SharedVariable<bool>)boolValue).Value = false;
		((SharedVariable<bool>)storeResult).Value = false;
	}
}
