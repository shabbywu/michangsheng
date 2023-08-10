using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.Math;

[TaskCategory("Basic/Math")]
[TaskDescription("Stores the absolute value of the float.")]
public class FloatAbs : Action
{
	[Tooltip("The float to return the absolute value of")]
	public SharedFloat floatVariable;

	public override TaskStatus OnUpdate()
	{
		((SharedVariable<float>)floatVariable).Value = Mathf.Abs(((SharedVariable<float>)floatVariable).Value);
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		floatVariable = 0f;
	}
}
