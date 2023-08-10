using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables;

[TaskCategory("Basic/SharedVariable")]
[TaskDescription("Gets the GameObject from the Transform component. Returns Success.")]
public class SharedTransformToGameObject : Action
{
	[Tooltip("The Transform component")]
	public SharedTransform sharedTransform;

	[RequiredField]
	[Tooltip("The GameObject to set")]
	public SharedGameObject sharedGameObject;

	public override TaskStatus OnUpdate()
	{
		if (!((Object)(object)((SharedVariable<Transform>)sharedTransform).Value == (Object)null))
		{
			((SharedVariable<GameObject>)sharedGameObject).Value = ((Component)((SharedVariable<Transform>)sharedTransform).Value).gameObject;
			return (TaskStatus)2;
		}
		return (TaskStatus)1;
	}

	public override void OnReset()
	{
		sharedTransform = null;
		sharedGameObject = null;
	}
}
