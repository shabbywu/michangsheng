using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator;

[TaskCategory("Basic/Animator")]
[TaskDescription("Gets the avatar delta position for the last evaluated frame. Returns Success.")]
public class GetDeltaPosition : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("The avatar delta position")]
	[RequiredField]
	public SharedVector3 storeValue;

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
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)animator == (Object)null)
		{
			Debug.LogWarning((object)"Animator is null");
			return (TaskStatus)1;
		}
		((SharedVariable<Vector3>)storeValue).Value = animator.deltaPosition;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		targetGameObject = null;
		storeValue = Vector3.zero;
	}
}
