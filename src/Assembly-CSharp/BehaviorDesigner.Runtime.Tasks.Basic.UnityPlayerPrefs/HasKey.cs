using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityPlayerPrefs;

[TaskCategory("Basic/PlayerPrefs")]
[TaskDescription("Retruns success if the specified key exists.")]
public class HasKey : Conditional
{
	[Tooltip("The key to check")]
	public SharedString key;

	public override TaskStatus OnUpdate()
	{
		if (PlayerPrefs.HasKey(((SharedVariable<string>)key).Value))
		{
			return (TaskStatus)2;
		}
		return (TaskStatus)1;
	}

	public override void OnReset()
	{
		key = "";
	}
}
