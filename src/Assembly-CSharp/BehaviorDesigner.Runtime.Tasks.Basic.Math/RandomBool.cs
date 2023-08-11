using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.Math;

[TaskCategory("Basic/Math")]
[TaskDescription("Sets a random bool value")]
public class RandomBool : Action
{
	[Tooltip("The variable to store the result")]
	public SharedBool storeResult;

	public override TaskStatus OnUpdate()
	{
		((SharedVariable<bool>)storeResult).Value = Random.value < 0.5f;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		((SharedVariable<bool>)storeResult).Value = false;
	}
}
