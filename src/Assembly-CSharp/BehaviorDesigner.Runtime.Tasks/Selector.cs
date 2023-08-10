namespace BehaviorDesigner.Runtime.Tasks;

[TaskDescription("The selector task is similar to an \"or\" operation. It will return success as soon as one of its child tasks return success. If a child task returns failure then it will sequentially run the next task. If no child task returns success then it will return failure.")]
[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=26")]
[TaskIcon("{SkinColor}SelectorIcon.png")]
public class Selector : Composite
{
	private int currentChildIndex;

	private TaskStatus executionStatus;

	public override int CurrentChildIndex()
	{
		return currentChildIndex;
	}

	public override bool CanExecute()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Invalid comparison between Unknown and I4
		if (currentChildIndex < ((ParentTask)this).children.Count)
		{
			return (int)executionStatus != 2;
		}
		return false;
	}

	public override void OnChildExecuted(TaskStatus childStatus)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		currentChildIndex++;
		executionStatus = childStatus;
	}

	public override void OnConditionalAbort(int childIndex)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		currentChildIndex = childIndex;
		executionStatus = (TaskStatus)0;
	}

	public override void OnEnd()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		executionStatus = (TaskStatus)0;
		currentChildIndex = 0;
	}
}
