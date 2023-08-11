using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector3;

[TaskCategory("Basic/Vector3")]
[TaskDescription("Normalize the Vector3.")]
public class Normalize : Action
{
	[Tooltip("The Vector3 to normalize")]
	public SharedVector3 vector3Variable;

	[Tooltip("The normalized resut")]
	[RequiredField]
	public SharedVector3 storeResult;

	public override TaskStatus OnUpdate()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		((SharedVariable<Vector3>)storeResult).Value = Vector3.Normalize(((SharedVariable<Vector3>)vector3Variable).Value);
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		vector3Variable = (storeResult = Vector3.zero);
	}
}
