using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator;

[TaskCategory("Basic/Animator")]
[TaskDescription("Stores the playback speed of the animator. 1 is normal playback speed. Returns Success.")]
public class GetSpeed : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("The playback speed of the Animator")]
	[RequiredField]
	public SharedFloat storeValue;

	private Animator animator;

	private GameObject prevGameObject;

	public override void OnStart()
	{
		GameObject defaultGameObject = ((Task)this).GetDefaultGameObject(((SharedVariable<GameObject>)targetGameObject).Value);
		if ((Object)(object)defaultGameObject != (Object)(object)prevGameObject)
		{
			animator = defaultGameObject.GetComponent<Animator>();
			prevGameObject = defaultGameObject;
		}
	}

	public override TaskStatus OnUpdate()
	{
		if ((Object)(object)animator == (Object)null)
		{
			Debug.LogWarning((object)"Animator is null");
			return (TaskStatus)1;
		}
		((SharedVariable<float>)storeValue).Value = animator.speed;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		targetGameObject = null;
		storeValue = 0f;
	}
}
