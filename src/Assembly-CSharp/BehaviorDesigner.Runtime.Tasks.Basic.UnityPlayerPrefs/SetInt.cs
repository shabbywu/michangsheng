using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityPlayerPrefs;

[TaskCategory("Basic/PlayerPrefs")]
[TaskDescription("Sets the value with the specified key from the PlayerPrefs.")]
public class SetInt : Action
{
	[Tooltip("The key to store")]
	public SharedString key;

	[Tooltip("The value to set")]
	public SharedInt value;

	public override TaskStatus OnUpdate()
	{
		PlayerPrefs.SetInt(((SharedVariable<string>)key).Value, ((SharedVariable<int>)value).Value);
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		key = "";
		value = 0;
	}
}
