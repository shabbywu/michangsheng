using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator;

[TaskCategory("Basic/Animator")]
[TaskDescription("Returns success if the specified name matches the name of the active state.")]
public class IsName : Conditional
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("The layer's index")]
	public SharedInt index;

	[Tooltip("The state name to compare")]
	public SharedString name;

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
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)animator == (Object)null)
		{
			Debug.LogWarning((object)"Animator is null");
			return (TaskStatus)1;
		}
		AnimatorStateInfo currentAnimatorStateInfo = animator.GetCurrentAnimatorStateInfo(((SharedVariable<int>)index).Value);
		if (((AnimatorStateInfo)(ref currentAnimatorStateInfo)).IsName(((SharedVariable<string>)name).Value))
		{
			return (TaskStatus)2;
		}
		return (TaskStatus)1;
	}

	public override void OnReset()
	{
		targetGameObject = null;
		index = 0;
		name = "";
	}
}
