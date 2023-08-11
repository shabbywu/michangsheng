using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityInput;

[TaskCategory("Basic/Input")]
[TaskDescription("Returns success when the specified key is pressed.")]
public class IsKeyDown : Conditional
{
	[Tooltip("The key to test")]
	public KeyCode key;

	public override TaskStatus OnUpdate()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		if (Input.GetKeyDown(key))
		{
			return (TaskStatus)2;
		}
		return (TaskStatus)1;
	}

	public override void OnReset()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		key = (KeyCode)0;
	}
}
