namespace BehaviorDesigner.Runtime.Tasks.Basic.Math;

[TaskCategory("Basic/Math")]
[TaskDescription("Sets a float value")]
public class SetFloat : Action
{
	[Tooltip("The float value to set")]
	public SharedFloat floatValue;

	[Tooltip("The variable to store the result")]
	public SharedFloat storeResult;

	public override TaskStatus OnUpdate()
	{
		((SharedVariable<float>)storeResult).Value = ((SharedVariable<float>)floatValue).Value;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		((SharedVariable<float>)floatValue).Value = 0f;
		((SharedVariable<float>)storeResult).Value = 0f;
	}
}
