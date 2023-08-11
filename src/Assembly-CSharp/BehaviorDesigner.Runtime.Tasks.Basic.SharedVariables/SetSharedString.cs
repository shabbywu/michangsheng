namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables;

[TaskCategory("Basic/SharedVariable")]
[TaskDescription("Sets the SharedString variable to the specified object. Returns Success.")]
public class SetSharedString : Action
{
	[Tooltip("The value to set the SharedString to")]
	public SharedString targetValue;

	[RequiredField]
	[Tooltip("The SharedString to set")]
	public SharedString targetVariable;

	public override TaskStatus OnUpdate()
	{
		((SharedVariable<string>)targetVariable).Value = ((SharedVariable<string>)targetValue).Value;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		targetValue = "";
		targetVariable = "";
	}
}
