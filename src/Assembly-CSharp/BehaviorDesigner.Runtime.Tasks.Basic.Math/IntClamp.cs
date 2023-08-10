using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.Math;

[TaskCategory("Basic/Math")]
[TaskDescription("Clamps the int between two values.")]
public class IntClamp : Action
{
	[Tooltip("The int to clamp")]
	public SharedInt intVariable;

	[Tooltip("The maximum value of the int")]
	public SharedInt minValue;

	[Tooltip("The maximum value of the int")]
	public SharedInt maxValue;

	public override TaskStatus OnUpdate()
	{
		((SharedVariable<int>)intVariable).Value = Mathf.Clamp(((SharedVariable<int>)intVariable).Value, ((SharedVariable<int>)minValue).Value, ((SharedVariable<int>)maxValue).Value);
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		intVariable = (minValue = (maxValue = 0));
	}
}
