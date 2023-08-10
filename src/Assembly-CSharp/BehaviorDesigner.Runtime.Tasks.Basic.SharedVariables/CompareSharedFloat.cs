namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables;

[TaskCategory("Basic/SharedVariable")]
[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
public class CompareSharedFloat : Conditional
{
	[Tooltip("The first variable to compare")]
	public SharedFloat variable;

	[Tooltip("The variable to compare to")]
	public SharedFloat compareTo;

	public override TaskStatus OnUpdate()
	{
		if (((SharedVariable<float>)variable).Value.Equals(((SharedVariable<float>)compareTo).Value))
		{
			return (TaskStatus)2;
		}
		return (TaskStatus)1;
	}

	public override void OnReset()
	{
		variable = 0f;
		compareTo = 0f;
	}
}
