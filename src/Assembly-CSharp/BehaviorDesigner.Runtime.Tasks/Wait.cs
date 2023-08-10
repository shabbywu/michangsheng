using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks;

[TaskDescription("Wait a specified amount of time. The task will return running until the task is done waiting. It will return success after the wait time has elapsed.")]
[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=22")]
[TaskIcon("{SkinColor}WaitIcon.png")]
public class Wait : Action
{
	[Tooltip("The amount of time to wait")]
	public SharedFloat waitTime = 1f;

	[Tooltip("Should the wait be randomized?")]
	public SharedBool randomWait = false;

	[Tooltip("The minimum wait time if random wait is enabled")]
	public SharedFloat randomWaitMin = 1f;

	[Tooltip("The maximum wait time if random wait is enabled")]
	public SharedFloat randomWaitMax = 1f;

	private float waitDuration;

	private float startTime;

	private float pauseTime;

	public override void OnStart()
	{
		startTime = Time.time;
		if (((SharedVariable<bool>)randomWait).Value)
		{
			waitDuration = Random.Range(((SharedVariable<float>)randomWaitMin).Value, ((SharedVariable<float>)randomWaitMax).Value);
		}
		else
		{
			waitDuration = ((SharedVariable<float>)waitTime).Value;
		}
	}

	public override TaskStatus OnUpdate()
	{
		if (!(startTime + waitDuration < Time.time))
		{
			return (TaskStatus)3;
		}
		return (TaskStatus)2;
	}

	public override void OnPause(bool paused)
	{
		if (paused)
		{
			pauseTime = Time.time;
		}
		else
		{
			startTime += Time.time - pauseTime;
		}
	}

	public override void OnReset()
	{
		waitTime = 1f;
		randomWait = false;
		randomWaitMin = 1f;
		randomWaitMax = 1f;
	}
}
