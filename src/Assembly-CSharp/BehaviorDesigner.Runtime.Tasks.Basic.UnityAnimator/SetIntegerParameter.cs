using System.Collections;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator;

[TaskCategory("Basic/Animator")]
[TaskDescription("Sets the int parameter on an animator. Returns Success.")]
public class SetIntegerParameter : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("The name of the parameter")]
	public SharedString paramaterName;

	[Tooltip("The value of the int parameter")]
	public SharedInt intValue;

	[Tooltip("Should the value be reverted back to its original value after it has been set?")]
	public bool setOnce;

	private int hashID;

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
		hashID = Animator.StringToHash(((SharedVariable<string>)paramaterName).Value);
		int integer = animator.GetInteger(hashID);
		animator.SetInteger(hashID, ((SharedVariable<int>)intValue).Value);
		if (setOnce)
		{
			((Task)this).StartCoroutine(ResetValue(integer));
		}
		return (TaskStatus)2;
	}

	public IEnumerator ResetValue(int origVale)
	{
		yield return null;
		animator.SetInteger(hashID, origVale);
	}

	public override void OnReset()
	{
		targetGameObject = null;
		paramaterName = "";
		intValue = 0;
	}
}
