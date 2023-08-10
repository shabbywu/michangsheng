using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityDebug;

[TaskCategory("Basic/Debug")]
[TaskDescription("Draws a debug ray")]
public class DrawRay : Action
{
	[Tooltip("The position")]
	public SharedVector3 start;

	[Tooltip("The direction")]
	public SharedVector3 direction;

	[Tooltip("The color")]
	public SharedColor color = Color.white;

	public override TaskStatus OnUpdate()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		Debug.DrawRay(((SharedVariable<Vector3>)start).Value, ((SharedVariable<Vector3>)direction).Value, ((SharedVariable<Color>)color).Value);
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		start = Vector3.zero;
		direction = Vector3.zero;
		color = Color.white;
	}
}
