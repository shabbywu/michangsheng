using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody;

[RequiredComponent(typeof(Rigidbody))]
[TaskCategory("Basic/Rigidbody")]
[TaskDescription("Applies a force to the rigidbody. Returns Success.")]
public class AddForce : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("The amount of force to apply")]
	public SharedVector3 force;

	[Tooltip("The type of force")]
	public ForceMode forceMode;

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
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)rigidbody == (Object)null)
		{
			Debug.LogWarning((object)"Rigidbody is null");
			return (TaskStatus)1;
		}
		rigidbody.AddForce(((SharedVariable<Vector3>)force).Value, forceMode);
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		targetGameObject = null;
		if (force != null)
		{
			((SharedVariable<Vector3>)force).Value = Vector3.zero;
		}
		forceMode = (ForceMode)0;
	}
}
