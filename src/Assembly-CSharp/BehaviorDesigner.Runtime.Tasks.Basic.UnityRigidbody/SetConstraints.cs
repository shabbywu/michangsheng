using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody;

[TaskCategory("Basic/Rigidbody")]
[TaskDescription("Sets the constraints of the Rigidbody. Returns Success.")]
public class SetConstraints : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("The constraints of the Rigidbody")]
	public RigidbodyConstraints constraints;

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
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)rigidbody == (Object)null)
		{
			Debug.LogWarning((object)"Rigidbody is null");
			return (TaskStatus)1;
		}
		rigidbody.constraints = constraints;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		targetGameObject = null;
		constraints = (RigidbodyConstraints)0;
	}
}
