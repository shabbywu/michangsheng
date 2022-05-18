using System;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02001498 RID: 5272
	[TaskDescription("Similar to the sequence task, the random sequence task will return success as soon as every child task returns success.  The difference is that the random sequence class will run its children in a random order. The sequence task is deterministic in that it will always run the tasks from left to right within the tree. The random sequence task shuffles the child tasks up and then begins execution in a random order. Other than that the random sequence class is the same as the sequence class. It will stop running tasks as soon as a single task ends in failure. On a task failure it will stop executing all of the child tasks and return failure. If no child returns failure then it will return success.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=31")]
	[TaskIcon("{SkinColor}RandomSequenceIcon.png")]
	public class RandomSequence : Composite
	{
		// Token: 0x06007E83 RID: 32387 RVA: 0x002C8EF8 File Offset: 0x002C70F8
		public override void OnAwake()
		{
			if (this.useSeed)
			{
				Random.InitState(this.seed);
			}
			this.childIndexList.Clear();
			for (int i = 0; i < this.children.Count; i++)
			{
				this.childIndexList.Add(i);
			}
		}

		// Token: 0x06007E84 RID: 32388 RVA: 0x000559D0 File Offset: 0x00053BD0
		public override void OnStart()
		{
			this.ShuffleChilden();
		}

		// Token: 0x06007E85 RID: 32389 RVA: 0x000559D8 File Offset: 0x00053BD8
		public override int CurrentChildIndex()
		{
			return this.childrenExecutionOrder.Peek();
		}

		// Token: 0x06007E86 RID: 32390 RVA: 0x000559E5 File Offset: 0x00053BE5
		public override bool CanExecute()
		{
			return this.childrenExecutionOrder.Count > 0 && this.executionStatus != 1;
		}

		// Token: 0x06007E87 RID: 32391 RVA: 0x00055A03 File Offset: 0x00053C03
		public override void OnChildExecuted(TaskStatus childStatus)
		{
			if (this.childrenExecutionOrder.Count > 0)
			{
				this.childrenExecutionOrder.Pop();
			}
			this.executionStatus = childStatus;
		}

		// Token: 0x06007E88 RID: 32392 RVA: 0x00055A26 File Offset: 0x00053C26
		public override void OnConditionalAbort(int childIndex)
		{
			this.childrenExecutionOrder.Clear();
			this.executionStatus = 0;
			this.ShuffleChilden();
		}

		// Token: 0x06007E89 RID: 32393 RVA: 0x00055A40 File Offset: 0x00053C40
		public override void OnEnd()
		{
			this.executionStatus = 0;
			this.childrenExecutionOrder.Clear();
		}

		// Token: 0x06007E8A RID: 32394 RVA: 0x00055A54 File Offset: 0x00053C54
		public override void OnReset()
		{
			this.seed = 0;
			this.useSeed = false;
		}

		// Token: 0x06007E8B RID: 32395 RVA: 0x002C8F48 File Offset: 0x002C7148
		private void ShuffleChilden()
		{
			for (int i = this.childIndexList.Count; i > 0; i--)
			{
				int index = Random.Range(0, i);
				int num = this.childIndexList[index];
				this.childrenExecutionOrder.Push(num);
				this.childIndexList[index] = this.childIndexList[i - 1];
				this.childIndexList[i - 1] = num;
			}
		}

		// Token: 0x04006BB1 RID: 27569
		[Tooltip("Seed the random number generator to make things easier to debug")]
		public int seed;

		// Token: 0x04006BB2 RID: 27570
		[Tooltip("Do we want to use the seed?")]
		public bool useSeed;

		// Token: 0x04006BB3 RID: 27571
		private List<int> childIndexList = new List<int>();

		// Token: 0x04006BB4 RID: 27572
		private Stack<int> childrenExecutionOrder = new Stack<int>();

		// Token: 0x04006BB5 RID: 27573
		private TaskStatus executionStatus;
	}
}
