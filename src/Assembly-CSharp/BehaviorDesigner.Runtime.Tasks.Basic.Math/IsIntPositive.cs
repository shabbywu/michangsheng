namespace BehaviorDesigner.Runtime.Tasks.Basic.Math;

[TaskCategory("Basic/Math")]
[TaskDescription("Is the int a positive value?")]
public class IsIntPositive : Conditional
{
	[Tooltip("The int to check if positive")]
	public SharedInt intVariable;

	public override TaskStatus OnUpdate()
	{
		if (((SharedVariable<int>)intVariable).Value > 0)
		{
			return (TaskStatus)2;
		}
		return (TaskStatus)1;
	}

	public override void OnReset()
	{
		intVariable = 0;
	}
}
