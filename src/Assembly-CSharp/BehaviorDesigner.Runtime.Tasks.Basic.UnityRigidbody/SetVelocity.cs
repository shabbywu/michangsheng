using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody;

[TaskCategory("Basic/Rigidbody")]
[TaskDescription("Sets the velocity of the Rigidbody. Returns Success.")]
public class SetVelocity : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("The velocity of the Rigidbody")]
	public SharedVector3 velocity;

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
		rigidbody.velocity = ((SharedVariable<Vector3>)velocity).Value;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		targetGameObject = null;
		velocity = Vector3.zero;
	}
}
