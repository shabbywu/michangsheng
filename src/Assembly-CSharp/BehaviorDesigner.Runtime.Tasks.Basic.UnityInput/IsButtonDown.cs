using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityInput;

[TaskCategory("Basic/Input")]
[TaskDescription("Returns success when the specified button is pressed.")]
public class IsButtonDown : Conditional
{
	[Tooltip("The name of the button")]
	public SharedString buttonName;

	public override TaskStatus OnUpdate()
	{
		if (Input.GetButtonDown(((SharedVariable<string>)buttonName).Value))
		{
			return (TaskStatus)2;
		}
		return (TaskStatus)1;
	}

	public override void OnReset()
	{
		buttonName = "Fire1";
	}
}
