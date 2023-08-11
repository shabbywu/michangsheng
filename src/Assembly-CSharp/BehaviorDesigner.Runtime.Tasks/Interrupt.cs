namespace BehaviorDesigner.Runtime.Tasks;

[TaskDescription("The interrupt task will stop all child tasks from running if it is interrupted. The interruption can be triggered by the perform interruption task. The interrupt task will keep running its child until this interruption is called. If no interruption happens and the child task completed its execution the interrupt task will return the value assigned by the child task.")]
[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=35")]
[TaskIcon("{SkinColor}InterruptIcon.png")]
public class Interrupt : Decorator
{
	private TaskStatus interruptStatus = (TaskStatus)1;

	private TaskStatus executionStatus;

	public override bool CanExecute()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Invalid comparison between Unknown and I4
		if ((int)executionStatus != 0)
		{
			return (int)executionStatus == 3;
		}
		return true;
	}

	public override void OnChildExecuted(TaskStatus childStatus)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		executionStatus = childStatus;
	}

	public void DoInterrupt(TaskStatus status)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		interruptStatus = status;
		BehaviorManager.instance.Interrupt(((Task)this).Owner, (Task)(object)this);
	}

	public override TaskStatus OverrideStatus()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		return interruptStatus;
	}

	public override void OnEnd()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		interruptStatus = (TaskStatus)1;
		executionStatus = (TaskStatus)0;
	}
}
