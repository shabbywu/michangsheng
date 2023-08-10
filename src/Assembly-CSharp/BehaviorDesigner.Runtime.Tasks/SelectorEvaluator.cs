namespace BehaviorDesigner.Runtime.Tasks;

[TaskDescription("The selector evaluator is a selector task which reevaluates its children every tick. It will run the lowest priority child which returns a task status of running. This is done each tick. If a higher priority child is running and the next frame a lower priority child wants to run it will interrupt the higher priority child. The selector evaluator will return success as soon as the first child returns success otherwise it will keep trying higher priority children. This task mimics the conditional abort functionality except the child tasks don't always have to be conditional tasks.")]
[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=109")]
[TaskIcon("{SkinColor}SelectorEvaluatorIcon.png")]
public class SelectorEvaluator : Composite
{
	private int currentChildIndex;

	private TaskStatus executionStatus;

	private int storedCurrentChildIndex = -1;

	private TaskStatus storedExecutionStatus;

	public override int CurrentChildIndex()
	{
		return currentChildIndex;
	}

	public override void OnChildStarted(int childIndex)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		currentChildIndex++;
		executionStatus = (TaskStatus)3;
	}

	public override bool CanExecute()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Invalid comparison between Unknown and I4
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Invalid comparison between Unknown and I4
		if ((int)executionStatus == 2 || (int)executionStatus == 3)
		{
			return false;
		}
		if (storedCurrentChildIndex != -1)
		{
			return currentChildIndex < storedCurrentChildIndex - 1;
		}
		return currentChildIndex < ((ParentTask)this).children.Count;
	}

	public override void OnChildExecuted(int childIndex, TaskStatus childStatus)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Invalid comparison between Unknown and I4
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		if ((int)childStatus != 0 && (int)childStatus != 3)
		{
			executionStatus = childStatus;
		}
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

	public override TaskStatus OverrideStatus(TaskStatus status)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		return executionStatus;
	}

	public override bool CanRunParallelChildren()
	{
		return true;
	}

	public override bool CanReevaluate()
	{
		return true;
	}

	public override bool OnReevaluationStarted()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		if ((int)executionStatus == 0)
		{
			return false;
		}
		storedCurrentChildIndex = currentChildIndex;
		storedExecutionStatus = executionStatus;
		currentChildIndex = 0;
		executionStatus = (TaskStatus)0;
		return true;
	}

	public override void OnReevaluationEnded(TaskStatus status)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Invalid comparison between Unknown and I4
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		if ((int)executionStatus != 1 && (int)executionStatus != 0)
		{
			BehaviorManager.instance.Interrupt(((Task)this).Owner, ((ParentTask)this).children[storedCurrentChildIndex - 1], (Task)(object)this);
		}
		else
		{
			currentChildIndex = storedCurrentChildIndex;
			executionStatus = storedExecutionStatus;
		}
		storedCurrentChildIndex = -1;
		storedExecutionStatus = (TaskStatus)0;
	}
}
