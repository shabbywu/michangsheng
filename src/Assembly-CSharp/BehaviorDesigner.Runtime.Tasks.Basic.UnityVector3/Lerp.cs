using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector3;

[TaskCategory("Basic/Vector3")]
[TaskDescription("Lerp the Vector3 by an amount.")]
public class Lerp : Action
{
	[Tooltip("The from value")]
	public SharedVector3 fromVector3;

	[Tooltip("The to value")]
	public SharedVector3 toVector3;

	[Tooltip("The amount to lerp")]
	public SharedFloat lerpAmount;

	[Tooltip("The lerp resut")]
	[RequiredField]
	public SharedVector3 storeResult;

	public override TaskStatus OnUpdate()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		((SharedVariable<Vector3>)storeResult).Value = Vector3.Lerp(((SharedVariable<Vector3>)fromVector3).Value, ((SharedVariable<Vector3>)toVector3).Value, ((SharedVariable<float>)lerpAmount).Value);
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		fromVector3 = (toVector3 = (storeResult = Vector3.zero));
		lerpAmount = 0f;
	}
}
