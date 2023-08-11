using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityGameObject;

[TaskCategory("Basic/GameObject")]
[TaskDescription("Stores the GameObject tag. Returns Success.")]
public class GetTag : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("Active state of the GameObject")]
	[RequiredField]
	public SharedString storeValue;

	public override TaskStatus OnUpdate()
	{
		((SharedVariable<string>)storeValue).Value = ((Task)this).GetDefaultGameObject(((SharedVariable<GameObject>)targetGameObject).Value).tag;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		targetGameObject = null;
		storeValue = "";
	}
}
