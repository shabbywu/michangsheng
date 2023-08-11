using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityGameObject;

[TaskCategory("Basic/GameObject")]
[TaskDescription("Sets the GameObject tag. Returns Success.")]
public class SetTag : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("The GameObject tag")]
	public SharedString tag;

	public override TaskStatus OnUpdate()
	{
		((Task)this).GetDefaultGameObject(((SharedVariable<GameObject>)targetGameObject).Value).tag = ((SharedVariable<string>)tag).Value;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		targetGameObject = null;
		tag = "";
	}
}
