using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.Math;

[TaskCategory("Basic/Math")]
[TaskDescription("Stores the absolute value of the int.")]
public class IntAbs : Action
{
	[Tooltip("The int to return the absolute value of")]
	public SharedInt intVariable;

	public override TaskStatus OnUpdate()
	{
		((SharedVariable<int>)intVariable).Value = Mathf.Abs(((SharedVariable<int>)intVariable).Value);
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		intVariable = 0;
	}
}
