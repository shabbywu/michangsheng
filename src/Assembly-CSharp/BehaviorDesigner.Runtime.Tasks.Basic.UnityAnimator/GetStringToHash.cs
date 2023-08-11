using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator;

[TaskCategory("Basic/Animator")]
[TaskDescription("Converts the state name to its corresponding hash code. Returns Success.")]
public class GetStringToHash : Action
{
	[Tooltip("The name of the state to convert to a hash code")]
	public SharedString stateName;

	[Tooltip("The hash value")]
	[RequiredField]
	public SharedInt storeValue;

	public override TaskStatus OnUpdate()
	{
		((SharedVariable<int>)storeValue).Value = Animator.StringToHash(((SharedVariable<string>)stateName).Value);
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		stateName = "";
		storeValue = 0;
	}
}
