using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityInput;

[TaskCategory("Basic/Input")]
[TaskDescription("Returns success when the specified mouse button is pressed.")]
public class IsMouseDown : Conditional
{
	[Tooltip("The button index")]
	public SharedInt buttonIndex;

	public override TaskStatus OnUpdate()
	{
		if (Input.GetMouseButtonDown(((SharedVariable<int>)buttonIndex).Value))
		{
			return (TaskStatus)2;
		}
		return (TaskStatus)1;
	}

	public override void OnReset()
	{
		buttonIndex = 0;
	}
}
