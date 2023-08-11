using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityInput;

[TaskCategory("Basic/Input")]
[TaskDescription("Stores the state of the specified button.")]
public class GetButton : Action
{
	[Tooltip("The name of the button")]
	public SharedString buttonName;

	[RequiredField]
	[Tooltip("The stored result")]
	public SharedBool storeResult;

	public override TaskStatus OnUpdate()
	{
		((SharedVariable<bool>)storeResult).Value = Input.GetButton(((SharedVariable<string>)buttonName).Value);
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		buttonName = "Fire1";
		storeResult = false;
	}
}
