using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform;

[TaskCategory("Basic/Transform")]
[TaskDescription("Sets the right vector of the Transform. Returns Success.")]
public class SetRightVector : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("The position of the Transform")]
	public SharedVector3 position;

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
		targetTransform.right = ((SharedVariable<Vector3>)position).Value;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		targetGameObject = null;
		position = Vector3.zero;
	}
}
