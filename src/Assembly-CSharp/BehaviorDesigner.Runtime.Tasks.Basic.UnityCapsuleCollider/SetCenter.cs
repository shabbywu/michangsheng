using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCapsuleCollider;

[TaskCategory("Basic/CapsuleCollider")]
[TaskDescription("Sets the center of the CapsuleCollider. Returns Success.")]
public class SetCenter : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("The center of the CapsuleCollider")]
	public SharedVector3 center;

	private CapsuleCollider capsuleCollider;

	private GameObject prevGameObject;

	public override void OnStart()
	{
		GameObject defaultGameObject = ((Task)this).GetDefaultGameObject(((SharedVariable<GameObject>)targetGameObject).Value);
		if ((Object)(object)defaultGameObject != (Object)(object)prevGameObject)
		{
			capsuleCollider = defaultGameObject.GetComponent<CapsuleCollider>();
			prevGameObject = defaultGameObject;
		}
	}

	public override TaskStatus OnUpdate()
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)capsuleCollider == (Object)null)
		{
			Debug.LogWarning((object)"CapsuleCollider is null");
			return (TaskStatus)1;
		}
		capsuleCollider.center = ((SharedVariable<Vector3>)center).Value;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		targetGameObject = null;
		center = Vector3.zero;
	}
}
