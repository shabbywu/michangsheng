namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables;

[TaskCategory("Basic/SharedVariable")]
[TaskDescription("Sets the SharedInt variable to the specified object. Returns Success.")]
public class SetSharedInt : Action
{
	[Tooltip("The value to set the SharedInt to")]
	public SharedInt targetValue;

	[RequiredField]
	[Tooltip("The SharedInt to set")]
	public SharedInt targetVariable;

	public override TaskStatus OnUpdate()
	{
		((SharedVariable<int>)targetVariable).Value = ((SharedVariable<int>)targetValue).Value;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		targetValue = 0;
		targetVariable = 0;
	}
}
