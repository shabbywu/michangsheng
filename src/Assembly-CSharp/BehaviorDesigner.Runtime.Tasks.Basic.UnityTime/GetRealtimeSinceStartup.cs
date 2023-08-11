using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTime;

[TaskCategory("Basic/Time")]
[TaskDescription("Returns the real time in seconds since the game started.")]
public class GetRealtimeSinceStartup : Action
{
	[Tooltip("The variable to store the result")]
	public SharedFloat storeResult;

	public override TaskStatus OnUpdate()
	{
		((SharedVariable<float>)storeResult).Value = Time.realtimeSinceStartup;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		((SharedVariable<float>)storeResult).Value = 0f;
	}
}
