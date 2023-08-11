using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTime;

[TaskCategory("Basic/Time")]
[TaskDescription("Sets the scale at which time is passing.")]
public class SetTimeScale : Action
{
	[Tooltip("The timescale")]
	public SharedFloat timeScale;

	public override TaskStatus OnUpdate()
	{
		Time.timeScale = ((SharedVariable<float>)timeScale).Value;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		((SharedVariable<float>)timeScale).Value = 0f;
	}
}
