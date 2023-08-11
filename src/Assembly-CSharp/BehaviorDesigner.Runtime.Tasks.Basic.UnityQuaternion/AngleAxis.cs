using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityQuaternion;

[TaskCategory("Basic/Quaternion")]
[TaskDescription("Stores the rotation which rotates the specified degrees around the specified axis.")]
public class AngleAxis : Action
{
	[Tooltip("The number of degrees")]
	public SharedFloat degrees;

	[Tooltip("The axis direction")]
	public SharedVector3 axis;

	[Tooltip("The stored result")]
	[RequiredField]
	public SharedQuaternion storeResult;

	public override TaskStatus OnUpdate()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		((SharedVariable<Quaternion>)storeResult).Value = Quaternion.AngleAxis(((SharedVariable<float>)degrees).Value, ((SharedVariable<Vector3>)axis).Value);
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		degrees = 0f;
		axis = Vector3.zero;
		storeResult = Quaternion.identity;
	}
}
