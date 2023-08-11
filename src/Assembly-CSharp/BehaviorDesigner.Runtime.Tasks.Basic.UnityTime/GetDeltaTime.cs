using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTime;

[TaskCategory("Basic/Time")]
[TaskDescription("Returns the time in seconds it took to complete the last frame.")]
public class GetDeltaTime : Action
{
	[Tooltip("The variable to store the result")]
	public SharedFloat storeResult;

	public override TaskStatus OnUpdate()
	{
		((SharedVariable<float>)storeResult).Value = Time.deltaTime;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		((SharedVariable<float>)storeResult).Value = 0f;
	}
}
