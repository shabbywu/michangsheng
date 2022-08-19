using System;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02000FDF RID: 4063
	[TaskDescription("Similar to the selector task, the random selector task will return success as soon as a child task returns success.  The difference is that the random selector class will run its children in a random order. The selector task is deterministic in that it will always run the tasks from left to right within the tree. The random selector task shuffles the child tasks up and then begins execution in a random order. Other than that the random selector class is the same as the selector class. It will continue running tasks until a task completes successfully. If no child tasks return success then it will return failure.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=30")]
	[TaskIcon("{SkinColor}RandomSelectorIcon.png")]
	public class RandomSelector : Composite
	{
		// Token: 0x0600707F RID: 28799 RVA: 0x002A9DC0 File Offset: 0x002A7FC0
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

		// Token: 0x06007080 RID: 28800 RVA: 0x002A9E0D File Offset: 0x002A800D
		public override void OnStart()
		{
			this.ShuffleChilden();
		}

		// Token: 0x06007081 RID: 28801 RVA: 0x002A9E15 File Offset: 0x002A8015
		public override int CurrentChildIndex()
		{
			return this.childrenExecutionOrder.Peek();
		}

		// Token: 0x06007082 RID: 28802 RVA: 0x002A9E22 File Offset: 0x002A8022
		public override bool CanExecute()
		{
			return this.childrenExecutionOrder.Count > 0 && this.executionStatus != 2;
		}

		// Token: 0x06007083 RID: 28803 RVA: 0x002A9E40 File Offset: 0x002A8040
		public override void OnChildExecuted(TaskStatus childStatus)
		{
			if (this.childrenExecutionOrder.Count > 0)
			{
				this.childrenExecutionOrder.Pop();
			}
			this.executionStatus = childStatus;
		}

		// Token: 0x06007084 RID: 28804 RVA: 0x002A9E63 File Offset: 0x002A8063
		public override void OnConditionalAbort(int childIndex)
		{
			this.childrenExecutionOrder.Clear();
			this.executionStatus = 0;
			this.ShuffleChilden();
		}

		// Token: 0x06007085 RID: 28805 RVA: 0x002A9E7D File Offset: 0x002A807D
		public override void OnEnd()
		{
			this.executionStatus = 0;
			this.childrenExecutionOrder.Clear();
		}

		// Token: 0x06007086 RID: 28806 RVA: 0x002A9E91 File Offset: 0x002A8091
		public override void OnReset()
		{
			this.seed = 0;
			this.useSeed = false;
		}

		// Token: 0x06007087 RID: 28807 RVA: 0x002A9EA4 File Offset: 0x002A80A4
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

		// Token: 0x04005CB4 RID: 23732
		[Tooltip("Seed the random number generator to make things easier to debug")]
		public int seed;

		// Token: 0x04005CB5 RID: 23733
		[Tooltip("Do we want to use the seed?")]
		public bool useSeed;

		// Token: 0x04005CB6 RID: 23734
		private List<int> childIndexList = new List<int>();

		// Token: 0x04005CB7 RID: 23735
		private Stack<int> childrenExecutionOrder = new Stack<int>();

		// Token: 0x04005CB8 RID: 23736
		private TaskStatus executionStatus;
	}
}
