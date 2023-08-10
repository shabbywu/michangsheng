using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityGameObject;

[TaskCategory("Basic/GameObject")]
[TaskDescription("Returns Success if tags match, otherwise Failure.")]
public class CompareTag : Conditional
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("The tag to compare against")]
	public SharedString tag;

	public override TaskStatus OnUpdate()
	{
		if (((Task)this).GetDefaultGameObject(((SharedVariable<GameObject>)targetGameObject).Value).CompareTag(((SharedVariable<string>)tag).Value))
		{
			return (TaskStatus)2;
		}
		return (TaskStatus)1;
	}

	public override void OnReset()
	{
		targetGameObject = null;
		tag = "";
	}
}
