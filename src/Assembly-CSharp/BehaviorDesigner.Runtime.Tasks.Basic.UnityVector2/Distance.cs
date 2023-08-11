using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector2;

[TaskCategory("Basic/Vector2")]
[TaskDescription("Returns the distance between two Vector2s.")]
public class Distance : Action
{
	[Tooltip("The first Vector2")]
	public SharedVector2 firstVector2;

	[Tooltip("The second Vector2")]
	public SharedVector2 secondVector2;

	[Tooltip("The distance")]
	[RequiredField]
	public SharedFloat storeResult;

	public override TaskStatus OnUpdate()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		((SharedVariable<float>)storeResult).Value = Vector2.Distance(((SharedVariable<Vector2>)firstVector2).Value, ((SharedVariable<Vector2>)secondVector2).Value);
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		firstVector2 = (secondVector2 = Vector2.zero);
		storeResult = 0f;
	}
}
