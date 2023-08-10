using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityBoxCollider;

[TaskCategory("Basic/BoxCollider")]
[TaskDescription("Stores the center of the BoxCollider. Returns Success.")]
public class GetCenter : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("The center of the BoxCollider")]
	[RequiredField]
	public SharedVector3 storeValue;

	private BoxCollider boxCollider;

	private GameObject prevGameObject;

	public override void OnStart()
	{
		GameObject defaultGameObject = ((Task)this).GetDefaultGameObject(((SharedVariable<GameObject>)targetGameObject).Value);
		if ((Object)(object)defaultGameObject != (Object)(object)prevGameObject)
		{
			boxCollider = defaultGameObject.GetComponent<BoxCollider>();
			prevGameObject = defaultGameObject;
		}
	}

	public override TaskStatus OnUpdate()
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)boxCollider == (Object)null)
		{
			Debug.LogWarning((object)"BoxCollider is null");
			return (TaskStatus)1;
		}
		((SharedVariable<Vector3>)storeValue).Value = boxCollider.center;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		targetGameObject = null;
		storeValue = Vector3.zero;
	}
}
