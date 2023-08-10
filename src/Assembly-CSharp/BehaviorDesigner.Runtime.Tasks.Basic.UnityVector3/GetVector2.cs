using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector3;

[TaskCategory("Basic/Vector3")]
[TaskDescription("Stores the Vector2 value of the Vector3.")]
public class GetVector2 : Action
{
	[Tooltip("The Vector3 to get the Vector2 value of")]
	public SharedVector3 vector3Variable;

	[Tooltip("The Vector2 value")]
	[RequiredField]
	public SharedVector2 storeResult;

	public override TaskStatus OnUpdate()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		((SharedVariable<Vector2>)storeResult).Value = Vector2.op_Implicit(((SharedVariable<Vector3>)vector3Variable).Value);
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		vector3Variable = Vector3.zero;
		storeResult = Vector2.zero;
	}
}
