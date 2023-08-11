using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityInput;

[TaskCategory("Basic/Input")]
[TaskDescription("Stores the state of the specified mouse button.")]
public class GetMouseButton : Action
{
	[Tooltip("The index of the button")]
	public SharedInt buttonIndex;

	[RequiredField]
	[Tooltip("The stored result")]
	public SharedBool storeResult;

	public override TaskStatus OnUpdate()
	{
		((SharedVariable<bool>)storeResult).Value = Input.GetMouseButton(((SharedVariable<int>)buttonIndex).Value);
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		buttonIndex = 0;
		storeResult = false;
	}
}
