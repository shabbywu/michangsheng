namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables;

[TaskCategory("Basic/SharedVariable")]
[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
public class CompareSharedInt : Conditional
{
	[Tooltip("The first variable to compare")]
	public SharedInt variable;

	[Tooltip("The variable to compare to")]
	public SharedInt compareTo;

	public override TaskStatus OnUpdate()
	{
		if (((SharedVariable<int>)variable).Value.Equals(((SharedVariable<int>)compareTo).Value))
		{
			return (TaskStatus)2;
		}
		return (TaskStatus)1;
	}

	public override void OnReset()
	{
		variable = 0;
		compareTo = 0;
	}
}
