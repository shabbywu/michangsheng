using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityGameObject;

[TaskCategory("Basic/GameObject")]
[TaskDescription("Destorys the specified GameObject immediately. Returns Success.")]
public class DestroyImmediate : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	public override TaskStatus OnUpdate()
	{
		Object.DestroyImmediate((Object)(object)((Task)this).GetDefaultGameObject(((SharedVariable<GameObject>)targetGameObject).Value));
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		targetGameObject = null;
	}
}
