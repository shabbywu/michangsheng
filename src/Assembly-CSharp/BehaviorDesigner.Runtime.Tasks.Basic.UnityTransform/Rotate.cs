using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform;

[TaskCategory("Basic/Transform")]
[TaskDescription("Applies a rotation. Returns Success.")]
public class Rotate : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("Amount to rotate")]
	public SharedVector3 eulerAngles;

	[Tooltip("Specifies which axis the rotation is relative to")]
	public Space relativeTo = (Space)1;

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
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)targetTransform == (Object)null)
		{
			Debug.LogWarning((object)"Transform is null");
			return (TaskStatus)1;
		}
		targetTransform.Rotate(((SharedVariable<Vector3>)eulerAngles).Value, relativeTo);
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		targetGameObject = null;
		eulerAngles = Vector3.zero;
		relativeTo = (Space)1;
	}
}
