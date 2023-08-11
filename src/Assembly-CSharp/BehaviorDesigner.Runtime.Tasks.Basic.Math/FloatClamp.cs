using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.Math;

[TaskCategory("Basic/Math")]
[TaskDescription("Clamps the float between two values.")]
public class FloatClamp : Action
{
	[Tooltip("The float to clamp")]
	public SharedFloat floatVariable;

	[Tooltip("The maximum value of the float")]
	public SharedFloat minValue;

	[Tooltip("The maximum value of the float")]
	public SharedFloat maxValue;

	public override TaskStatus OnUpdate()
	{
		((SharedVariable<float>)floatVariable).Value = Mathf.Clamp(((SharedVariable<float>)floatVariable).Value, ((SharedVariable<float>)minValue).Value, ((SharedVariable<float>)maxValue).Value);
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		floatVariable = (minValue = (maxValue = 0f));
	}
}
