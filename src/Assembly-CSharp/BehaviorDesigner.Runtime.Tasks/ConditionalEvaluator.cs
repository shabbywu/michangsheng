namespace BehaviorDesigner.Runtime.Tasks;

[TaskDescription("Evaluates the specified conditional task. If the conditional task returns success then the child task is run and the child status is returned. If the conditional task does not return success then the child task is not run and a failure status is immediately returned.")]
[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=146")]
[TaskIcon("{SkinColor}ConditionalEvaluatorIcon.png")]
public class ConditionalEvaluator : Decorator
{
	[Tooltip("Should the conditional task be reevaluated every tick?")]
	public SharedBool reevaluate;

	[InspectTask]
	[Tooltip("The conditional task to evaluate")]
	public Conditional conditionalTask;

	private TaskStatus executionStatus;

	private bool checkConditionalTask = true;

	private bool conditionalTaskFailed;

	public override void OnAwake()
	{
		if (conditionalTask != null)
		{
			((Task)conditionalTask).Owner = ((Task)this).Owner;
			((Task)conditionalTask).GameObject = ((Task)this).gameObject;
			((Task)conditionalTask).Transform = ((Task)this).transform;
			((Task)conditionalTask).OnAwake();
		}
	}

	public override void OnStart()
	{
		if (conditionalTask != null)
		{
			((Task)conditionalTask).OnStart();
		}
	}

	public override bool CanExecute()
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Invalid comparison between Unknown and I4
		if (checkConditionalTask)
		{
			checkConditionalTask = false;
			((Task)this).OnUpdate();
		}
		if (conditionalTaskFailed)
		{
			return false;
		}
		if ((int)executionStatus != 0)
		{
			return (int)executionStatus == 3;
		}
		return true;
	}

	public override bool CanReevaluate()
	{
		return ((SharedVariable<bool>)reevaluate).Value;
	}

	public override TaskStatus OnUpdate()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Invalid comparison between Unknown and I4
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		TaskStatus val = ((Task)conditionalTask).OnUpdate();
		conditionalTaskFailed = conditionalTask == null || (int)val == 1;
		return val;
	}

	public override void OnChildExecuted(TaskStatus childStatus)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		executionStatus = childStatus;
	}

	public override TaskStatus OverrideStatus()
	{
		return (TaskStatus)1;
	}

	public override TaskStatus OverrideStatus(TaskStatus status)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		if (conditionalTaskFailed)
		{
			return (TaskStatus)1;
		}
		return status;
	}

	public override void OnEnd()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		executionStatus = (TaskStatus)0;
		checkConditionalTask = true;
		conditionalTaskFailed = false;
		if (conditionalTask != null)
		{
			((Task)conditionalTask).OnEnd();
		}
	}

	public override void OnReset()
	{
		conditionalTask = null;
	}
}
