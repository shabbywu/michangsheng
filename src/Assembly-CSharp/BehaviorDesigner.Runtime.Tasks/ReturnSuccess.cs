namespace BehaviorDesigner.Runtime.Tasks;

[TaskDescription("The return success task will always return success except when the child task is running.")]
[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=39")]
[TaskIcon("{SkinColor}ReturnSuccessIcon.png")]
public class ReturnSuccess : Decorator
{
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

	public override TaskStatus Decorate(TaskStatus status)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Invalid comparison between Unknown and I4
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		if ((int)status == 1)
		{
			return (TaskStatus)2;
		}
		return status;
	}

	public override void OnEnd()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		executionStatus = (TaskStatus)0;
	}
}
