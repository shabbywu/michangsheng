using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector3;

[TaskCategory("Basic/Vector3")]
[TaskDescription("Returns the angle between two Vector3s.")]
public class Angle : Action
{
	[Tooltip("The first Vector3")]
	public SharedVector3 firstVector3;

	[Tooltip("The second Vector3")]
	public SharedVector3 secondVector3;

	[Tooltip("The angle")]
	[RequiredField]
	public SharedFloat storeResult;

	public override TaskStatus OnUpdate()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		((SharedVariable<float>)storeResult).Value = Vector3.Angle(((SharedVariable<Vector3>)firstVector3).Value, ((SharedVariable<Vector3>)secondVector3).Value);
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		firstVector3 = (secondVector3 = Vector3.zero);
		storeResult = 0f;
	}
}
