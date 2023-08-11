using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityGameObject;

[TaskCategory("Basic/GameObject")]
[TaskDescription("Finds a GameObject by tag. Returns Success.")]
public class FindWithTag : Action
{
	[Tooltip("The tag of the GameObject to find")]
	public SharedString tag;

	[Tooltip("The object found by name")]
	[RequiredField]
	public SharedGameObject storeValue;

	public override TaskStatus OnUpdate()
	{
		((SharedVariable<GameObject>)storeValue).Value = GameObject.FindWithTag(((SharedVariable<string>)tag).Value);
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		((SharedVariable<string>)tag).Value = null;
		((SharedVariable<GameObject>)storeValue).Value = null;
	}
}
