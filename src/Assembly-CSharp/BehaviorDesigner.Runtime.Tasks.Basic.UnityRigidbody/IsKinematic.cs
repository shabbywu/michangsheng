using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody;

[TaskCategory("Basic/Rigidbody")]
[TaskDescription("Returns Success if the Rigidbody is kinematic, otherwise Failure.")]
public class IsKinematic : Conditional
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

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
		if ((Object)(object)rigidbody == (Object)null)
		{
			Debug.LogWarning((object)"Rigidbody is null");
			return (TaskStatus)1;
		}
		if (rigidbody.isKinematic)
		{
			return (TaskStatus)2;
		}
		return (TaskStatus)1;
	}

	public override void OnReset()
	{
		targetGameObject = null;
	}
}