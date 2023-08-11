using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator;

[TaskCategory("Basic/Animator")]
[TaskDescription("Plays an animator state. Returns Success.")]
public class Play : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("The name of the state")]
	public SharedString stateName;

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
		animator.Play(((SharedVariable<string>)stateName).Value, layer, normalizedTime);
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		targetGameObject = null;
		stateName = "";
		layer = -1;
		normalizedTime = float.NegativeInfinity;
	}
}
