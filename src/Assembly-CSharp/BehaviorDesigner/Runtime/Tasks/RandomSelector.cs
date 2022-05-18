using System;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02001497 RID: 5271
	[TaskDescription("Similar to the selector task, the random selector task will return success as soon as a child task returns success.  The difference is that the random selector class will run its children in a random order. The selector task is deterministic in that it will always run the tasks from left to right within the tree. The random selector task shuffles the child tasks up and then begins execution in a random order. Other than that the random selector class is the same as the selector class. It will continue running tasks until a task completes successfully. If no child tasks return success then it will return failure.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=30")]
	[TaskIcon("{SkinColor}RandomSelectorIcon.png")]
	public class RandomSelector : Composite
	{
		// Token: 0x06007E79 RID: 32377 RVA: 0x002C8E38 File Offset: 0x002C7038
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

		// Token: 0x06007E7A RID: 32378 RVA: 0x0005591E File Offset: 0x00053B1E
		public override void OnStart()
		{
			this.ShuffleChilden();
		}

		// Token: 0x06007E7B RID: 32379 RVA: 0x00055926 File Offset: 0x00053B26
		public override int CurrentChildIndex()
		{
			return this.childrenExecutionOrder.Peek();
		}

		// Token: 0x06007E7C RID: 32380 RVA: 0x00055933 File Offset: 0x00053B33
		public override bool CanExecute()
		{
			return this.childrenExecutionOrder.Count > 0 && this.executionStatus != 2;
		}

		// Token: 0x06007E7D RID: 32381 RVA: 0x00055951 File Offset: 0x00053B51
		public override void OnChildExecuted(TaskStatus childStatus)
		{
			if (this.childrenExecutionOrder.Count > 0)
			{
				this.childrenExecutionOrder.Pop();
			}
			this.executionStatus = childStatus;
		}

		// Token: 0x06007E7E RID: 32382 RVA: 0x00055974 File Offset: 0x00053B74
		public override void OnConditionalAbort(int childIndex)
		{
			this.childrenExecutionOrder.Clear();
			this.executionStatus = 0;
			this.ShuffleChilden();
		}

		// Token: 0x06007E7F RID: 32383 RVA: 0x0005598E File Offset: 0x00053B8E
		public override void OnEnd()
		{
			this.executionStatus = 0;
			this.childrenExecutionOrder.Clear();
		}

		// Token: 0x06007E80 RID: 32384 RVA: 0x000559A2 File Offset: 0x00053BA2
		public override void OnReset()
		{
			this.seed = 0;
			this.useSeed = false;
		}

		// Token: 0x06007E81 RID: 32385 RVA: 0x002C8E88 File Offset: 0x002C7088
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

		// Token: 0x04006BAC RID: 27564
		[Tooltip("Seed the random number generator to make things easier to debug")]
		public int seed;

		// Token: 0x04006BAD RID: 27565
		[Tooltip("Do we want to use the seed?")]
		public bool useSeed;

		// Token: 0x04006BAE RID: 27566
		private List<int> childIndexList = new List<int>();

		// Token: 0x04006BAF RID: 27567
		private Stack<int> childrenExecutionOrder = new Stack<int>();

		// Token: 0x04006BB0 RID: 27568
		private TaskStatus executionStatus;
	}
}
