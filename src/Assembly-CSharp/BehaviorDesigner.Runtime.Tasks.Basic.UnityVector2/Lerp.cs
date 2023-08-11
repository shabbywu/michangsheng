using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector2;

[TaskCategory("Basic/Vector2")]
[TaskDescription("Lerp the Vector2 by an amount.")]
public class Lerp : Action
{
	[Tooltip("The from value")]
	public SharedVector2 fromVector2;

	[Tooltip("The to value")]
	public SharedVector2 toVector2;

	[Tooltip("The amount to lerp")]
	public SharedFloat lerpAmount;

	[Tooltip("The lerp resut")]
	[RequiredField]
	public SharedVector2 storeResult;

	public override TaskStatus OnUpdate()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		((SharedVariable<Vector2>)storeResult).Value = Vector2.Lerp(((SharedVariable<Vector2>)fromVector2).Value, ((SharedVariable<Vector2>)toVector2).Value, ((SharedVariable<float>)lerpAmount).Value);
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		fromVector2 = (toVector2 = (storeResult = Vector2.zero));
		lerpAmount = 0f;
	}
}
