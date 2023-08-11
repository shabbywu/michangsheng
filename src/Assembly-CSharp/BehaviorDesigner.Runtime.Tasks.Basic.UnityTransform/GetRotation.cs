using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform;

[TaskCategory("Basic/Transform")]
[TaskDescription("Stores the rotation of the Transform. Returns Success.")]
public class GetRotation : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("The rotation of the Transform")]
	[RequiredField]
	public SharedQuaternion storeValue;

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
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)targetTransform == (Object)null)
		{
			Debug.LogWarning((object)"Transform is null");
			return (TaskStatus)1;
		}
		((SharedVariable<Quaternion>)storeValue).Value = targetTransform.rotation;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		targetGameObject = null;
		storeValue = Quaternion.identity;
	}
}
