using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCharacterController;

[TaskCategory("Basic/CharacterController")]
[TaskDescription("Moves the character with speed. Returns Success.")]
public class SimpleMove : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("The speed of the movement")]
	public SharedVector3 speed;

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
		if ((Object)(object)characterController == (Object)null)
		{
			Debug.LogWarning((object)"CharacterController is null");
			return (TaskStatus)1;
		}
		characterController.SimpleMove(((SharedVariable<Vector3>)speed).Value);
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		targetGameObject = null;
		speed = Vector3.zero;
	}
}
