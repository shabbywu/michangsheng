using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityGameObject;

[TaskCategory("Basic/GameObject")]
[TaskDescription("Instantiates a new GameObject. Returns Success.")]
public class Instantiate : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("The position of the new GameObject")]
	public SharedVector3 position;

	[Tooltip("The rotation of the new GameObject")]
	public SharedQuaternion rotation = Quaternion.identity;

	[SharedRequired]
	[Tooltip("The instantiated GameObject")]
	public SharedGameObject storeResult;

	public override TaskStatus OnUpdate()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		((SharedVariable<GameObject>)storeResult).Value = Object.Instantiate<GameObject>(((SharedVariable<GameObject>)targetGameObject).Value, ((SharedVariable<Vector3>)position).Value, ((SharedVariable<Quaternion>)rotation).Value);
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		targetGameObject = null;
		position = Vector3.zero;
		rotation = Quaternion.identity;
	}
}
