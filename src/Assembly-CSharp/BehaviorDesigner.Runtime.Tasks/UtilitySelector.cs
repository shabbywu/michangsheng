using System.Collections.Generic;

namespace BehaviorDesigner.Runtime.Tasks;

[TaskDescription("The utility selector task evaluates the child tasks using Utility Theory AI. The child task can override the GetUtility method and return the utility value at that particular time. The task with the highest utility value will be selected and the existing running task will be aborted. The utility selector task reevaluates its children every tick.")]
[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=134")]
[TaskIcon("{SkinColor}UtilitySelectorIcon.png")]
public class UtilitySelector : Composite
{
	private int currentChildIndex;

	private float highestUtility;

	private TaskStatus executionStatus;

	private bool reevaluating;

	private List<int> availableChildren = new List<int>();

	public override void OnStart()
	{
		highestUtility = float.MinValue;
		availableChildren.Clear();
		for (int i = 0; i < ((ParentTask)this).children.Count; i++)
		{
			float utility = ((ParentTask)this).children[i].GetUtility();
			if (utility > highestUtility)
			{
				highestUtility = utility;
				currentChildIndex = i;
			}
			availableChildren.Add(i);
		}
	}

	public override int CurrentChildIndex()
	{
		return currentChildIndex;
	}

	public override void OnChildStarted(int childIndex)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		executionStatus = (TaskStatus)3;
	}

	public override bool CanExecute()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Invalid comparison between Unknown and I4
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Invalid comparison between Unknown and I4
		if ((int)executionStatus == 2 || (int)executionStatus == 3 || reevaluating)
		{
			return false;
		}
		return availableChildren.Count > 0;
	}

	public override void OnChildExecuted(int childIndex, TaskStatus childStatus)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Invalid comparison between Unknown and I4
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Invalid comparison between Unknown and I4
		if ((int)childStatus == 0 || (int)childStatus == 3)
		{
			return;
		}
		executionStatus = childStatus;
		if ((int)executionStatus != 1)
		{
			return;
		}
		availableChildren.Remove(childIndex);
		highestUtility = float.MinValue;
		for (int i = 0; i < availableChildren.Count; i++)
		{
			float utility = ((ParentTask)this).children[availableChildren[i]].GetUtility();
			if (utility > highestUtility)
			{
				highestUtility = utility;
				currentChildIndex = availableChildren[i];
			}
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
		if ((int)executionStatus == 0)
		{
			return false;
		}
		reevaluating = true;
		return true;
	}

	public override void OnReevaluationEnded(TaskStatus status)
	{
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		reevaluating = false;
		int num = currentChildIndex;
		highestUtility = float.MinValue;
		for (int i = 0; i < availableChildren.Count; i++)
		{
			float utility = ((ParentTask)this).children[availableChildren[i]].GetUtility();
			if (utility > highestUtility)
			{
				highestUtility = utility;
				currentChildIndex = availableChildren[i];
			}
		}
		if (num != currentChildIndex)
		{
			BehaviorManager.instance.Interrupt(((Task)this).Owner, ((ParentTask)this).children[num], (Task)(object)this);
			executionStatus = (TaskStatus)0;
		}
	}
}
