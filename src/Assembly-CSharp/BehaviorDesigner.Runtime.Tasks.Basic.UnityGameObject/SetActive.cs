using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityGameObject;

[TaskCategory("Basic/GameObject")]
[TaskDescription("Activates/Deactivates the GameObject. Returns Success.")]
public class SetActive : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("Active state of the GameObject")]
	public SharedBool active;

	public override TaskStatus OnUpdate()
	{
		((Task)this).GetDefaultGameObject(((SharedVariable<GameObject>)targetGameObject).Value).SetActive(((SharedVariable<bool>)active).Value);
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		targetGameObject = null;
		active = false;
	}
}
