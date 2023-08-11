using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector2;

[TaskCategory("Basic/Vector2")]
[TaskDescription("Move from the current position to the target position.")]
public class MoveTowards : Action
{
	[Tooltip("The current position")]
	public SharedVector2 currentPosition;

	[Tooltip("The target position")]
	public SharedVector2 targetPosition;

	[Tooltip("The movement speed")]
	public SharedFloat speed;

	[Tooltip("The move resut")]
	[RequiredField]
	public SharedVector2 storeResult;

	public override TaskStatus OnUpdate()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		((SharedVariable<Vector2>)storeResult).Value = Vector2.MoveTowards(((SharedVariable<Vector2>)currentPosition).Value, ((SharedVariable<Vector2>)targetPosition).Value, ((SharedVariable<float>)speed).Value * Time.deltaTime);
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		currentPosition = (targetPosition = (storeResult = Vector2.zero));
		speed = 0f;
	}
}
