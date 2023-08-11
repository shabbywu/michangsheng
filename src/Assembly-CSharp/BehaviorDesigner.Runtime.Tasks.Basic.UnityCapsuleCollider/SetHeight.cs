using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCapsuleCollider;

[TaskCategory("Basic/CapsuleCollider")]
[TaskDescription("Sets the height of the CapsuleCollider. Returns Success.")]
public class SetHeight : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("The height of the CapsuleCollider")]
	public SharedFloat direction;

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
		capsuleCollider.height = ((SharedVariable<float>)direction).Value;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		targetGameObject = null;
		direction = 0f;
	}
}
