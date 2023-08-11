namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityString;

[TaskCategory("Basic/String")]
[TaskDescription("Stores a substring of the target string")]
public class GetSubstring : Action
{
	[Tooltip("The target string")]
	public SharedString targetString;

	[Tooltip("The start substring index")]
	public SharedInt startIndex = 0;

	[Tooltip("The length of the substring. Don't use if -1")]
	public SharedInt length = -1;

	[Tooltip("The stored result")]
	[RequiredField]
	public SharedString storeResult;

	public override TaskStatus OnUpdate()
	{
		if (((SharedVariable<int>)length).Value != -1)
		{
			((SharedVariable<string>)storeResult).Value = ((SharedVariable<string>)targetString).Value.Substring(((SharedVariable<int>)startIndex).Value, ((SharedVariable<int>)length).Value);
		}
		else
		{
			((SharedVariable<string>)storeResult).Value = ((SharedVariable<string>)targetString).Value.Substring(((SharedVariable<int>)startIndex).Value);
		}
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		targetString = "";
		startIndex = 0;
		length = -1;
		storeResult = "";
	}
}
