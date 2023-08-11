using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityQuaternion;

[TaskCategory("Basic/Quaternion")]
[TaskDescription("Stores the quaternion after a rotation.")]
public class RotateTowards : Action
{
	[Tooltip("The from rotation")]
	public SharedQuaternion fromQuaternion;

	[Tooltip("The to rotation")]
	public SharedQuaternion toQuaternion;

	[Tooltip("The maximum degrees delta")]
	public SharedFloat maxDeltaDegrees;

	[Tooltip("The stored result")]
	[RequiredField]
	public SharedQuaternion storeResult;

	public override TaskStatus OnUpdate()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		((SharedVariable<Quaternion>)storeResult).Value = Quaternion.RotateTowards(((SharedVariable<Quaternion>)fromQuaternion).Value, ((SharedVariable<Quaternion>)toQuaternion).Value, ((SharedVariable<float>)maxDeltaDegrees).Value);
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		fromQuaternion = (toQuaternion = (storeResult = Quaternion.identity));
		maxDeltaDegrees = 0f;
	}
}
