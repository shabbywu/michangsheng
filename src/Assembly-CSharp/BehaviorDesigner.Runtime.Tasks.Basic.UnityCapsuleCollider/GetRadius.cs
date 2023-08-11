using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCapsuleCollider;

[TaskCategory("Basic/CapsuleCollider")]
[TaskDescription("Stores the radius of the CapsuleCollider. Returns Success.")]
public class GetRadius : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("The radius of the CapsuleCollider")]
	[RequiredField]
	public SharedFloat storeValue;

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
		if ((Object)(object)capsuleCollider == (Object)null)
		{
			Debug.LogWarning((object)"CapsuleCollider is null");
			return (TaskStatus)1;
		}
		((SharedVariable<float>)storeValue).Value = capsuleCollider.radius;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		targetGameObject = null;
		storeValue = 0f;
	}
}
