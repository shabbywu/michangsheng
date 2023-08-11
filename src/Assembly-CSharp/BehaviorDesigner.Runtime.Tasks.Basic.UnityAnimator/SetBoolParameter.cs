using System.Collections;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator;

[TaskCategory("Basic/Animator")]
[TaskDescription("Sets the bool parameter on an animator. Returns Success.")]
public class SetBoolParameter : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("The name of the parameter")]
	public SharedString paramaterName;

	[Tooltip("The value of the bool parameter")]
	public SharedBool boolValue;

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
		bool @bool = animator.GetBool(hashID);
		animator.SetBool(hashID, ((SharedVariable<bool>)boolValue).Value);
		if (setOnce)
		{
			((Task)this).StartCoroutine(ResetValue(@bool));
		}
		return (TaskStatus)2;
	}

	public IEnumerator ResetValue(bool origVale)
	{
		yield return null;
		animator.SetBool(hashID, origVale);
	}

	public override void OnReset()
	{
		targetGameObject = null;
		paramaterName = "";
		boolValue = false;
	}
}
