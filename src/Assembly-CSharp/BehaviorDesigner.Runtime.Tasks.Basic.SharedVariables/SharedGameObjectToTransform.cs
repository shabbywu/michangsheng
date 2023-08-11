using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables;

[TaskCategory("Basic/SharedVariable")]
[TaskDescription("Gets the Transform from the GameObject. Returns Success.")]
public class SharedGameObjectToTransform : Action
{
	[Tooltip("The GameObject to get the Transform of")]
	public SharedGameObject sharedGameObject;

	[RequiredField]
	[Tooltip("The Transform to set")]
	public SharedTransform sharedTransform;

	public override TaskStatus OnUpdate()
	{
		if (!((Object)(object)((SharedVariable<GameObject>)sharedGameObject).Value == (Object)null))
		{
			((SharedVariable<Transform>)sharedTransform).Value = ((SharedVariable<GameObject>)sharedGameObject).Value.GetComponent<Transform>();
			return (TaskStatus)2;
		}
		return (TaskStatus)1;
	}

	public override void OnReset()
	{
		sharedGameObject = null;
		sharedTransform = null;
	}
}
