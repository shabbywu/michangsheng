namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityString;

[TaskCategory("Basic/String")]
[TaskDescription("Creates a string from multiple other strings.")]
public class BuildString : Action
{
	[Tooltip("The array of strings")]
	public SharedString[] source;

	[Tooltip("The stored result")]
	[RequiredField]
	public SharedString storeResult;

	public override TaskStatus OnUpdate()
	{
		for (int i = 0; i < source.Length; i++)
		{
			SharedString sharedString = storeResult;
			((SharedVariable<string>)sharedString).Value = ((SharedVariable<string>)sharedString).Value + source[i];
		}
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		source = null;
		storeResult = null;
	}
}
