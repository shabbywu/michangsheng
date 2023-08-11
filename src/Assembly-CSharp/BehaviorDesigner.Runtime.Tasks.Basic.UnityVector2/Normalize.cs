using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector2;

[TaskCategory("Basic/Vector2")]
[TaskDescription("Normalize the Vector2.")]
public class Normalize : Action
{
	[Tooltip("The Vector2 to normalize")]
	public SharedVector2 vector2Variable;

	[Tooltip("The normalized resut")]
	[RequiredField]
	public SharedVector2 storeResult;

	public override TaskStatus OnUpdate()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		SharedVector2 sharedVector = storeResult;
		Vector2 value = ((SharedVariable<Vector2>)vector2Variable).Value;
		((SharedVariable<Vector2>)sharedVector).Value = ((Vector2)(ref value)).normalized;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		vector2Variable = (storeResult = Vector2.zero);
	}
}
