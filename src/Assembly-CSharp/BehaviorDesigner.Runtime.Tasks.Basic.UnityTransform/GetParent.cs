using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform;

[TaskCategory("Basic/Transform")]
[TaskDescription("Stores the parent of the Transform. Returns Success.")]
public class GetParent : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("The parent of the Transform")]
	[RequiredField]
	public SharedTransform storeValue;

	private Transform targetTransform;

	private GameObject prevGameObject;

	public override void OnStart()
	{
		GameObject defaultGameObject = ((Task)this).GetDefaultGameObject(((SharedVariable<GameObject>)targetGameObject).Value);
		if ((Object)(object)defaultGameObject != (Object)(object)prevGameObject)
		{
			targetTransform = defaultGameObject.GetComponent<Transform>();
			prevGameObject = defaultGameObject;
		}
	}

	public override TaskStatus OnUpdate()
	{
		if ((Object)(object)targetTransform == (Object)null)
		{
			Debug.LogWarning((object)"Transform is null");
			return (TaskStatus)1;
		}
		((SharedVariable<Transform>)storeValue).Value = targetTransform.parent;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		targetGameObject = null;
		storeValue = null;
	}
}
