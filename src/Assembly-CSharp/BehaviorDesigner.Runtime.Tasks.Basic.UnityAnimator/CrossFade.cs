using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator;

[TaskCategory("Basic/Animator")]
[TaskDescription("Creates a dynamic transition between the current state and the destination state. Returns Success.")]
public class CrossFade : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("The name of the state")]
	public SharedString stateName;

	[Tooltip("The duration of the transition. Value is in source state normalized time")]
	public SharedFloat transitionDuration;

	[Tooltip("The layer where the state is")]
	public int layer = -1;

	[Tooltip("The normalized time at which the state will play")]
	public float normalizedTime = float.NegativeInfinity;

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
		animator.CrossFade(((SharedVariable<string>)stateName).Value, ((SharedVariable<float>)transitionDuration).Value, layer, normalizedTime);
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		targetGameObject = null;
		stateName = "";
		transitionDuration = 0f;
		layer = -1;
		normalizedTime = float.NegativeInfinity;
	}
}
