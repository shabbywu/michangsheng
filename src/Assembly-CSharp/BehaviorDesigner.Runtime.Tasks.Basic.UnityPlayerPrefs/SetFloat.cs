using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityPlayerPrefs;

[TaskCategory("Basic/PlayerPrefs")]
[TaskDescription("Sets the value with the specified key from the PlayerPrefs.")]
public class SetFloat : Action
{
	[Tooltip("The key to store")]
	public SharedString key;

	[Tooltip("The value to set")]
	public SharedFloat value;

	public override TaskStatus OnUpdate()
	{
		PlayerPrefs.SetFloat(((SharedVariable<string>)key).Value, ((SharedVariable<float>)value).Value);
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		key = "";
		value = 0f;
	}
}
