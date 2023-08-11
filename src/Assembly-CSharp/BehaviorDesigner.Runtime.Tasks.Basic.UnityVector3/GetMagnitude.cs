using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector3;

[TaskCategory("Basic/Vector3")]
[TaskDescription("Stores the magnitude of the Vector3.")]
public class GetMagnitude : Action
{
	[Tooltip("The Vector3 to get the magnitude of")]
	public SharedVector3 vector3Variable;

	[Tooltip("The magnitude of the vector")]
	[RequiredField]
	public SharedFloat storeResult;

	public override TaskStatus OnUpdate()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		SharedFloat sharedFloat = storeResult;
		Vector3 value = ((SharedVariable<Vector3>)vector3Variable).Value;
		((SharedVariable<float>)sharedFloat).Value = ((Vector3)(ref value)).magnitude;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		vector3Variable = Vector3.zero;
		storeResult = 0f;
	}
}
