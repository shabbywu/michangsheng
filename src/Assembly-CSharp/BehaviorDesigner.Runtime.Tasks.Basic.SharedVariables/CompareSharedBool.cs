namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables;

[TaskCategory("Basic/SharedVariable")]
[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
public class CompareSharedBool : Conditional
{
	[Tooltip("The first variable to compare")]
	public SharedBool variable;

	[Tooltip("The variable to compare to")]
	public SharedBool compareTo;

	public override TaskStatus OnUpdate()
	{
		if (((SharedVariable<bool>)variable).Value.Equals(((SharedVariable<bool>)compareTo).Value))
		{
			return (TaskStatus)2;
		}
		return (TaskStatus)1;
	}

	public override void OnReset()
	{
		variable = false;
		compareTo = false;
	}
}
