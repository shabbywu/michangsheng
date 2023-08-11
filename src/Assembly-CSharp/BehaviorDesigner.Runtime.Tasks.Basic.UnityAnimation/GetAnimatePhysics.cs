using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation;

[TaskCategory("Basic/Animation")]
[TaskDescription("Stores the animate physics value. Returns Success.")]
public class GetAnimatePhysics : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("Are the if animations are executed in the physics loop?")]
	[RequiredField]
	public SharedBool storeValue;

	private Animation animation;

	private GameObject prevGameObject;

	public override void OnStart()
	{
		GameObject defaultGameObject = ((Task)this).GetDefaultGameObject(((SharedVariable<GameObject>)targetGameObject).Value);
		if ((Object)(object)defaultGameObject != (Object)(object)prevGameObject)
		{
			animation = defaultGameObject.GetComponent<Animation>();
			prevGameObject = defaultGameObject;
		}
	}

	public override TaskStatus OnUpdate()
	{
		if ((Object)(object)animation == (Object)null)
		{
			Debug.LogWarning((object)"Animation is null");
			return (TaskStatus)1;
		}
		((SharedVariable<bool>)storeValue).Value = animation.animatePhysics;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		targetGameObject = null;
		((SharedVariable<bool>)storeValue).Value = false;
	}
}
