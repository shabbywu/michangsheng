using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCharacterController;

[TaskCategory("Basic/CharacterController")]
[TaskDescription("A more complex move function taking absolute movement deltas. Returns Success.")]
public class Move : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("The amount to move")]
	public SharedVector3 motion;

	private CharacterController characterController;

	private GameObject prevGameObject;

	public override void OnStart()
	{
		GameObject defaultGameObject = ((Task)this).GetDefaultGameObject(((SharedVariable<GameObject>)targetGameObject).Value);
		if ((Object)(object)defaultGameObject != (Object)(object)prevGameObject)
		{
			characterController = defaultGameObject.GetComponent<CharacterController>();
			prevGameObject = defaultGameObject;
		}
	}

	public override TaskStatus OnUpdate()
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)characterController == (Object)null)
		{
			Debug.LogWarning((object)"CharacterController is null");
			return (TaskStatus)1;
		}
		characterController.Move(((SharedVariable<Vector3>)motion).Value);
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		targetGameObject = null;
		motion = Vector3.zero;
	}
}
