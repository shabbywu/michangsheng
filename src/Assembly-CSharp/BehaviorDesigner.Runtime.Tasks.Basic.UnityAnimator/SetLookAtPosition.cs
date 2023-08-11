using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator;

[TaskCategory("Basic/Animator")]
[TaskDescription("Sets the look at position. Returns Success.")]
public class SetLookAtPosition : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("The position to lookAt")]
	public SharedVector3 position;

	private Animator animator;

	private GameObject prevGameObject;

	private bool positionSet;

	public override void OnStart()
	{
		GameObject defaultGameObject = ((Task)this).GetDefaultGameObject(((SharedVariable<GameObject>)targetGameObject).Value);
		if ((Object)(object)defaultGameObject != (Object)(object)prevGameObject)
		{
			animator = defaultGameObject.GetComponent<Animator>();
			prevGameObject = defaultGameObject;
		}
		positionSet = false;
	}

	public override TaskStatus OnUpdate()
	{
		if ((Object)(object)animator == (Object)null)
		{
			Debug.LogWarning((object)"Animator is null");
			return (TaskStatus)1;
		}
		if (positionSet)
		{
			return (TaskStatus)2;
		}
		return (TaskStatus)3;
	}

	public override void OnAnimatorIK()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		if (!((Object)(object)animator == (Object)null))
		{
			animator.SetLookAtPosition(((SharedVariable<Vector3>)position).Value);
			positionSet = true;
		}
	}

	public override void OnReset()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		targetGameObject = null;
		position = Vector3.zero;
	}
}
