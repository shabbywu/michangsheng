using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCharacterController;

[TaskCategory("Basic/CharacterController")]
[TaskDescription("Returns Success if the character is grounded, otherwise Failure.")]
public class IsGrounded : Conditional
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

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
		if ((Object)(object)characterController == (Object)null)
		{
			Debug.LogWarning((object)"CharacterController is null");
			return (TaskStatus)1;
		}
		if (characterController.isGrounded)
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
