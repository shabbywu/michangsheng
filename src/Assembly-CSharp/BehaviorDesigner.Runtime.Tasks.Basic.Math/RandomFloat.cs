using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.Math;

[TaskCategory("Basic/Math")]
[TaskDescription("Sets a random float value")]
public class RandomFloat : Action
{
	[Tooltip("The minimum amount")]
	public SharedFloat min;

	[Tooltip("The maximum amount")]
	public SharedFloat max;

	[Tooltip("Is the maximum value inclusive?")]
	public bool inclusive;

	[Tooltip("The variable to store the result")]
	public SharedFloat storeResult;

	public override TaskStatus OnUpdate()
	{
		if (inclusive)
		{
			((SharedVariable<float>)storeResult).Value = Random.Range(((SharedVariable<float>)min).Value, ((SharedVariable<float>)max).Value);
		}
		else
		{
			((SharedVariable<float>)storeResult).Value = Random.Range(((SharedVariable<float>)min).Value, ((SharedVariable<float>)max).Value - 1E-05f);
		}
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		((SharedVariable<float>)min).Value = 0f;
		((SharedVariable<float>)max).Value = 0f;
		inclusive = false;
		((SharedVariable<float>)storeResult).Value = 0f;
	}
}
