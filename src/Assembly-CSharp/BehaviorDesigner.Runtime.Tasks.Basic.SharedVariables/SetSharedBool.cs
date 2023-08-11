namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables;

[TaskCategory("Basic/SharedVariable")]
[TaskDescription("Sets the SharedBool variable to the specified object. Returns Success.")]
public class SetSharedBool : Action
{
	[Tooltip("The value to set the SharedBool to")]
	public SharedBool targetValue;

	[RequiredField]
	[Tooltip("The SharedBool to set")]
	public SharedBool targetVariable;

	public override TaskStatus OnUpdate()
	{
		((SharedVariable<bool>)targetVariable).Value = ((SharedVariable<bool>)targetValue).Value;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		targetValue = false;
		targetVariable = false;
	}
}
