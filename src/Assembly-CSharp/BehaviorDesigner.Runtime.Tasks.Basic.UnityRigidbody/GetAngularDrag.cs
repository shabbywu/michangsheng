using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody;

[TaskCategory("Basic/Rigidbody")]
[TaskDescription("Stores the angular drag of the Rigidbody. Returns Success.")]
public class GetAngularDrag : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("The angular drag of the Rigidbody")]
	[RequiredField]
	public SharedFloat storeValue;

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
		((SharedVariable<float>)storeValue).Value = rigidbody.angularDrag;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		targetGameObject = null;
		storeValue = 0f;
	}
}
