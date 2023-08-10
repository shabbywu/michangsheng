using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCharacterController;

[TaskCategory("Basic/CharacterController")]
[TaskDescription("Returns Success if the collider hit another object, otherwise Failure.")]
public class HasColliderHit : Conditional
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("The tag of the GameObject to check for a collision against")]
	public SharedString tag = "";

	[Tooltip("The object that started the collision")]
	public SharedGameObject collidedGameObject;

	private bool enteredCollision;

	public override TaskStatus OnUpdate()
	{
		if (enteredCollision)
		{
			return (TaskStatus)2;
		}
		return (TaskStatus)1;
	}

	public override void OnEnd()
	{
		enteredCollision = false;
	}

	public override void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (string.IsNullOrEmpty(((SharedVariable<string>)tag).Value) || ((SharedVariable<string>)tag).Value.Equals(hit.gameObject.tag))
		{
			((SharedVariable<GameObject>)collidedGameObject).Value = hit.gameObject;
			enteredCollision = true;
		}
	}

	public override void OnReset()
	{
		targetGameObject = null;
		tag = "";
		collidedGameObject = null;
	}
}
