namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityString;

[TaskCategory("Basic/String")]
[TaskDescription("Returns success if the string is null or empty")]
public class IsNullOrEmpty : Conditional
{
	[Tooltip("The target string")]
	public SharedString targetString;

	public override TaskStatus OnUpdate()
	{
		if (string.IsNullOrEmpty(((SharedVariable<string>)targetString).Value))
		{
			return (TaskStatus)2;
		}
		return (TaskStatus)1;
	}

	public override void OnReset()
	{
		targetString = "";
	}
}
