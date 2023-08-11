using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody;

[TaskCategory("Basic/Rigidbody")]
[TaskDescription("Moves the Rigidbody to the specified position. Returns Success.")]
public class MovePosition : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("The new position of the Rigidbody")]
	public SharedVector3 position;

	private Rigidbody rigidbody;

	private GameObject prevGameObject;

	public override void OnStart()
	{
		GameObject defaultGameObject = ((Task)this).GetDefaultGameObject(((SharedVariable<GameObject>)targetGameObject).Value);
		if ((Object)(object)defaultGameObject != (Object)(object)prevGameObject)
		{
			rigidbody = defaultGameObject.GetComponent<Rigidbody>();
			prevGameObject = defaultGameObject;
		}
	}

	public override TaskStatus OnUpdate()
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)rigidbody == (Object)null)
		{
			Debug.LogWarning((object)"Rigidbody is null");
			return (TaskStatus)1;
		}
		rigidbody.MovePosition(((SharedVariable<Vector3>)position).Value);
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		targetGameObject = null;
		position = Vector3.zero;
	}
}
