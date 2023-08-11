using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCharacterController;

[TaskCategory("Basic/CharacterController")]
[TaskDescription("Sets the radius of the CharacterController. Returns Success.")]
public class SetRadius : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("The radius of the CharacterController")]
	public SharedFloat radius;

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
		characterController.radius = ((SharedVariable<float>)radius).Value;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		targetGameObject = null;
		radius = 0f;
	}
}
