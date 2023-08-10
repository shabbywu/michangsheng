using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator;

[TaskCategory("Basic/Animator")]
[TaskDescription("Sets the look at weight. Returns success immediately after.")]
public class SetLookAtWeight : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("(0-1) the global weight of the LookAt, multiplier for other parameters.")]
	public SharedFloat weight;

	[Tooltip("(0-1) determines how much the body is involved in the LookAt.")]
	public float bodyWeight;

	[Tooltip("(0-1) determines how much the head is involved in the LookAt.")]
	public float headWeight = 1f;

	[Tooltip("(0-1) determines how much the eyes are involved in the LookAt.")]
	public float eyesWeight;

	[Tooltip("(0-1) 0.0 means the character is completely unrestrained in motion, 1.0 means he's completely clamped (look at becomes impossible), and 0.5 means he'll be able to move on half of the possible range (180 degrees).")]
	public float clampWeight = 0.5f;

	private Animator animator;

	private GameObject prevGameObject;

	private bool weightSet;

	public override void OnStart()
	{
		GameObject defaultGameObject = ((Task)this).GetDefaultGameObject(((SharedVariable<GameObject>)targetGameObject).Value);
		if ((Object)(object)defaultGameObject != (Object)(object)prevGameObject)
		{
			animator = defaultGameObject.GetComponent<Animator>();
			prevGameObject = defaultGameObject;
		}
		weightSet = false;
	}

	public override TaskStatus OnUpdate()
	{
		if ((Object)(object)animator == (Object)null)
		{
			Debug.LogWarning((object)"Animator is null");
			return (TaskStatus)1;
		}
		if (weightSet)
		{
			return (TaskStatus)2;
		}
		return (TaskStatus)3;
	}

	public override void OnAnimatorIK()
	{
		if (!((Object)(object)animator == (Object)null))
		{
			animator.SetLookAtWeight(((SharedVariable<float>)weight).Value, bodyWeight, headWeight, eyesWeight, clampWeight);
			weightSet = true;
		}
	}

	public override void OnReset()
	{
		targetGameObject = null;
		weight = 0f;
		bodyWeight = 0f;
		headWeight = 1f;
		eyesWeight = 0f;
		clampWeight = 0.5f;
	}
}
