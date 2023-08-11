using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTime;

[TaskCategory("Basic/Time")]
[TaskDescription("Returns the time in second since the start of the game.")]
public class GetTime : Action
{
	[Tooltip("The variable to store the result")]
	public SharedFloat storeResult;

	public override TaskStatus OnUpdate()
	{
		((SharedVariable<float>)storeResult).Value = Time.time;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		((SharedVariable<float>)storeResult).Value = 0f;
	}
}
