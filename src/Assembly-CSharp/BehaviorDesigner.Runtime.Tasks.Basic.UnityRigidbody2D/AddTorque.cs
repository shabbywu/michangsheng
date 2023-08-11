using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody2D;

[TaskCategory("Basic/Rigidbody2D")]
[TaskDescription("Applies a torque to the Rigidbody2D. Returns Success.")]
public class AddTorque : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("The amount of torque to apply")]
	public SharedFloat torque;

	private Rigidbody2D rigidbody2D;

	private GameObject prevGameObject;

	public override void OnStart()
	{
		GameObject defaultGameObject = ((Task)this).GetDefaultGameObject(((SharedVariable<GameObject>)targetGameObject).Value);
		if ((Object)(object)defaultGameObject != (Object)(object)prevGameObject)
		{
			rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
			prevGameObject = defaultGameObject;
		}
	}

	public override TaskStatus OnUpdate()
	{
		if ((Object)(object)rigidbody2D == (Object)null)
		{
			Debug.LogWarning((object)"Rigidbody2D is null");
			return (TaskStatus)1;
		}
		rigidbody2D.AddTorque(((SharedVariable<float>)torque).Value);
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		targetGameObject = null;
		torque = 0f;
	}
}
