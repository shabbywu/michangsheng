using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector2;

[TaskCategory("Basic/Vector2")]
[TaskDescription("Stores the Vector3 value of the Vector2.")]
public class GetVector3 : Action
{
	[Tooltip("The Vector2 to get the Vector3 value of")]
	public SharedVector2 vector3Variable;

	[Tooltip("The Vector3 value")]
	[RequiredField]
	public SharedVector3 storeResult;

	public override TaskStatus OnUpdate()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		((SharedVariable<Vector3>)storeResult).Value = Vector2.op_Implicit(((SharedVariable<Vector2>)vector3Variable).Value);
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		vector3Variable = Vector2.zero;
		storeResult = Vector3.zero;
	}
}
