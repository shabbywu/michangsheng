namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityString;

[TaskCategory("Basic/String")]
[TaskDescription("Stores the length of the string")]
public class GetLength : Action
{
	[Tooltip("The target string")]
	public SharedString targetString;

	[Tooltip("The stored result")]
	[RequiredField]
	public SharedInt storeResult;

	public override TaskStatus OnUpdate()
	{
		((SharedVariable<int>)storeResult).Value = ((SharedVariable<string>)targetString).Value.Length;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		targetString = "";
		storeResult = 0;
	}
}
