using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityBoxCollider;

[TaskCategory("Basic/BoxCollider")]
[TaskDescription("Sets the size of the BoxCollider. Returns Success.")]
public class SetSize : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("The size of the BoxCollider")]
	public SharedVector3 size;

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
		boxCollider.size = ((SharedVariable<Vector3>)size).Value;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		targetGameObject = null;
		size = Vector3.zero;
	}
}
