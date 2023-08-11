using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.Math;

[TaskCategory("Basic/Math")]
[TaskDescription("Sets a random int value")]
public class RandomInt : Action
{
	[Tooltip("The minimum amount")]
	public SharedInt min;

	[Tooltip("The maximum amount")]
	public SharedInt max;

	[Tooltip("Is the maximum value inclusive?")]
	public bool inclusive;

	[Tooltip("The variable to store the result")]
	public SharedInt storeResult;

	public override TaskStatus OnUpdate()
	{
		if (inclusive)
		{
			((SharedVariable<int>)storeResult).Value = Random.Range(((SharedVariable<int>)min).Value, ((SharedVariable<int>)max).Value + 1);
		}
		else
		{
			((SharedVariable<int>)storeResult).Value = Random.Range(((SharedVariable<int>)min).Value, ((SharedVariable<int>)max).Value);
		}
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		((SharedVariable<int>)min).Value = 0;
		((SharedVariable<int>)max).Value = 0;
		inclusive = false;
		((SharedVariable<int>)storeResult).Value = 0;
	}
}
