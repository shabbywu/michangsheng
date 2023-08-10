using System.Collections.Generic;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks;

[TaskDescription("Similar to the selector task, the random selector task will return success as soon as a child task returns success.  The difference is that the random selector class will run its children in a random order. The selector task is deterministic in that it will always run the tasks from left to right within the tree. The random selector task shuffles the child tasks up and then begins execution in a random order. Other than that the random selector class is the same as the selector class. It will continue running tasks until a task completes successfully. If no child tasks return success then it will return failure.")]
[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=30")]
[TaskIcon("{SkinColor}RandomSelectorIcon.png")]
public class RandomSelector : Composite
{
	[Tooltip("Seed the random number generator to make things easier to debug")]
	public int seed;

	[Tooltip("Do we want to use the seed?")]
	public bool useSeed;

	private List<int> childIndexList = new List<int>();

	private Stack<int> childrenExecutionOrder = new Stack<int>();

	private TaskStatus executionStatus;

	public override void OnAwake()
	{
		if (useSeed)
		{
			Random.InitState(seed);
		}
		childIndexList.Clear();
		for (int i = 0; i < ((ParentTask)this).children.Count; i++)
		{
			childIndexList.Add(i);
		}
	}

	public override void OnStart()
	{
		ShuffleChilden();
	}

	public override int CurrentChildIndex()
	{
		return childrenExecutionOrder.Peek();
	}

	public override bool CanExecute()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Invalid comparison between Unknown and I4
		if (childrenExecutionOrder.Count > 0)
		{
			return (int)executionStatus != 2;
		}
		return false;
	}

	public override void OnChildExecuted(TaskStatus childStatus)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		if (childrenExecutionOrder.Count > 0)
		{
			childrenExecutionOrder.Pop();
		}
		executionStatus = childStatus;
	}

	public override void OnConditionalAbort(int childIndex)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		childrenExecutionOrder.Clear();
		executionStatus = (TaskStatus)0;
		ShuffleChilden();
	}

	public override void OnEnd()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		executionStatus = (TaskStatus)0;
		childrenExecutionOrder.Clear();
	}

	public override void OnReset()
	{
		seed = 0;
		useSeed = false;
	}

	private void ShuffleChilden()
	{
		for (int num = childIndexList.Count; num > 0; num--)
		{
			int index = Random.Range(0, num);
			int num2 = childIndexList[index];
			childrenExecutionOrder.Push(num2);
			childIndexList[index] = childIndexList[num - 1];
			childIndexList[num - 1] = num2;
		}
	}
}
