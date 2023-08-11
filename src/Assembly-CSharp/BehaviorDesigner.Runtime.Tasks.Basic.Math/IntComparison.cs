namespace BehaviorDesigner.Runtime.Tasks.Basic.Math;

[TaskCategory("Basic/Math")]
[TaskDescription("Performs comparison between two integers: less than, less than or equal to, equal to, not equal to, greater than or equal to, or greater than.")]
public class IntComparison : Conditional
{
	public enum Operation
	{
		LessThan,
		LessThanOrEqualTo,
		EqualTo,
		NotEqualTo,
		GreaterThanOrEqualTo,
		GreaterThan
	}

	[Tooltip("The operation to perform")]
	public Operation operation;

	[Tooltip("The first integer")]
	public SharedInt integer1;

	[Tooltip("The second integer")]
	public SharedInt integer2;

	public override TaskStatus OnUpdate()
	{
		switch (operation)
		{
		case Operation.LessThan:
			if (((SharedVariable<int>)integer1).Value < ((SharedVariable<int>)integer2).Value)
			{
				return (TaskStatus)2;
			}
			return (TaskStatus)1;
		case Operation.LessThanOrEqualTo:
			if (((SharedVariable<int>)integer1).Value <= ((SharedVariable<int>)integer2).Value)
			{
				return (TaskStatus)2;
			}
			return (TaskStatus)1;
		case Operation.EqualTo:
			if (((SharedVariable<int>)integer1).Value == ((SharedVariable<int>)integer2).Value)
			{
				return (TaskStatus)2;
			}
			return (TaskStatus)1;
		case Operation.NotEqualTo:
			if (((SharedVariable<int>)integer1).Value != ((SharedVariable<int>)integer2).Value)
			{
				return (TaskStatus)2;
			}
			return (TaskStatus)1;
		case Operation.GreaterThanOrEqualTo:
			if (((SharedVariable<int>)integer1).Value >= ((SharedVariable<int>)integer2).Value)
			{
				return (TaskStatus)2;
			}
			return (TaskStatus)1;
		case Operation.GreaterThan:
			if (((SharedVariable<int>)integer1).Value > ((SharedVariable<int>)integer2).Value)
			{
				return (TaskStatus)2;
			}
			return (TaskStatus)1;
		default:
			return (TaskStatus)1;
		}
	}

	public override void OnReset()
	{
		operation = Operation.LessThan;
		((SharedVariable<int>)integer1).Value = 0;
		((SharedVariable<int>)integer2).Value = 0;
	}
}
