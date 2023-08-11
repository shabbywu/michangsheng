using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnitySphereCollider;

[TaskCategory("Basic/SphereCollider")]
[TaskDescription("Stores the center of the SphereCollider. Returns Success.")]
public class GetCenter : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("The center of the SphereCollider")]
	[RequiredField]
	public SharedVector3 storeValue;

	private SphereCollider sphereCollider;

	private GameObject prevGameObject;

	public override void OnStart()
	{
		GameObject defaultGameObject = ((Task)this).GetDefaultGameObject(((SharedVariable<GameObject>)targetGameObject).Value);
		if ((Object)(object)defaultGameObject != (Object)(object)prevGameObject)
		{
			sphereCollider = defaultGameObject.GetComponent<SphereCollider>();
			prevGameObject = defaultGameObject;
		}
	}

	public override TaskStatus OnUpdate()
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)sphereCollider == (Object)null)
		{
			Debug.LogWarning((object)"SphereCollider is null");
			return (TaskStatus)1;
		}
		((SharedVariable<Vector3>)storeValue).Value = sphereCollider.center;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		storeValue = Vector3.zero;
	}
}
