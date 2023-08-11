using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityQuaternion;

[TaskCategory("Basic/Quaternion")]
[TaskDescription("Stores the angle in degrees between two rotations.")]
public class Angle : Action
{
	[Tooltip("The first rotation")]
	public SharedQuaternion firstRotation;

	[Tooltip("The second rotation")]
	public SharedQuaternion secondRotation;

	[Tooltip("The stored result")]
	[RequiredField]
	public SharedFloat storeResult;

	public override TaskStatus OnUpdate()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		((SharedVariable<float>)storeResult).Value = Quaternion.Angle(((SharedVariable<Quaternion>)firstRotation).Value, ((SharedVariable<Quaternion>)secondRotation).Value);
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		firstRotation = (secondRotation = Quaternion.identity);
		storeResult = 0f;
	}
}
