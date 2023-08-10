namespace BehaviorDesigner.Runtime.Tasks.Basic.Math;

[TaskCategory("Basic/Math")]
[TaskDescription("Performs a comparison between two bools.")]
public class BoolComparison : Conditional
{
	[Tooltip("The first bool")]
	public SharedBool bool1;

	[Tooltip("The second bool")]
	public SharedBool bool2;

	public override TaskStatus OnUpdate()
	{
		if (((SharedVariable<bool>)bool1).Value == ((SharedVariable<bool>)bool2).Value)
		{
			return (TaskStatus)2;
		}
		return (TaskStatus)1;
	}

	public override void OnReset()
	{
		((SharedVariable<bool>)bool1).Value = false;
		((SharedVariable<bool>)bool2).Value = false;
	}
}
