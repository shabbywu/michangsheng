using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector3;

[TaskCategory("Basic/Vector3")]
[TaskDescription("Move from the current position to the target position.")]
public class MoveTowards : Action
{
	[Tooltip("The current position")]
	public SharedVector3 currentPosition;

	[Tooltip("The target position")]
	public SharedVector3 targetPosition;

	[Tooltip("The movement speed")]
	public SharedFloat speed;

	[Tooltip("The move resut")]
	[RequiredField]
	public SharedVector3 storeResult;

	public override TaskStatus OnUpdate()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		((SharedVariable<Vector3>)storeResult).Value = Vector3.MoveTowards(((SharedVariable<Vector3>)currentPosition).Value, ((SharedVariable<Vector3>)targetPosition).Value, ((SharedVariable<float>)speed).Value * Time.deltaTime);
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		currentPosition = (targetPosition = (storeResult = Vector3.zero));
		speed = 0f;
	}
}
