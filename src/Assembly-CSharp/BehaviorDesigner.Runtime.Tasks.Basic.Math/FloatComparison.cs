namespace BehaviorDesigner.Runtime.Tasks.Basic.Math;

[TaskCategory("Basic/Math")]
[TaskDescription("Performs comparison between two floats: less than, less than or equal to, equal to, not equal to, greater than or equal to, or greater than.")]
public class FloatComparison : Conditional
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

	[Tooltip("The first float")]
	public SharedFloat float1;

	[Tooltip("The second float")]
	public SharedFloat float2;

	public override TaskStatus OnUpdate()
	{
		switch (operation)
		{
		case Operation.LessThan:
			if (((SharedVariable<float>)float1).Value < ((SharedVariable<float>)float2).Value)
			{
				return (TaskStatus)2;
			}
			return (TaskStatus)1;
		case Operation.LessThanOrEqualTo:
			if (((SharedVariable<float>)float1).Value <= ((SharedVariable<float>)float2).Value)
			{
				return (TaskStatus)2;
			}
			return (TaskStatus)1;
		case Operation.EqualTo:
			if (((SharedVariable<float>)float1).Value == ((SharedVariable<float>)float2).Value)
			{
				return (TaskStatus)2;
			}
			return (TaskStatus)1;
		case Operation.NotEqualTo:
			if (((SharedVariable<float>)float1).Value != ((SharedVariable<float>)float2).Value)
			{
				return (TaskStatus)2;
			}
			return (TaskStatus)1;
		case Operation.GreaterThanOrEqualTo:
			if (((SharedVariable<float>)float1).Value >= ((SharedVariable<float>)float2).Value)
			{
				return (TaskStatus)2;
			}
			return (TaskStatus)1;
		case Operation.GreaterThan:
			if (((SharedVariable<float>)float1).Value > ((SharedVariable<float>)float2).Value)
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
		((SharedVariable<float>)float1).Value = 0f;
		((SharedVariable<float>)float2).Value = 0f;
	}
}
