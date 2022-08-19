using System;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02000FE0 RID: 4064
	[TaskDescription("Similar to the sequence task, the random sequence task will return success as soon as every child task returns success.  The difference is that the random sequence class will run its children in a random order. The sequence task is deterministic in that it will always run the tasks from left to right within the tree. The random sequence task shuffles the child tasks up and then begins execution in a random order. Other than that the random sequence class is the same as the sequence class. It will stop running tasks as soon as a single task ends in failure. On a task failure it will stop executing all of the child tasks and return failure. If no child returns failure then it will return success.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=31")]
	[TaskIcon("{SkinColor}RandomSequenceIcon.png")]
	public class RandomSequence : Composite
	{
		// Token: 0x06007089 RID: 28809 RVA: 0x002A9F30 File Offset: 0x002A8130
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

		// Token: 0x0600708A RID: 28810 RVA: 0x002A9F7D File Offset: 0x002A817D
		public override void OnStart()
		{
			this.ShuffleChilden();
		}

		// Token: 0x0600708B RID: 28811 RVA: 0x002A9F85 File Offset: 0x002A8185
		public override int CurrentChildIndex()
		{
			return this.childrenExecutionOrder.Peek();
		}

		// Token: 0x0600708C RID: 28812 RVA: 0x002A9F92 File Offset: 0x002A8192
		public override bool CanExecute()
		{
			return this.childrenExecutionOrder.Count > 0 && this.executionStatus != 1;
		}

		// Token: 0x0600708D RID: 28813 RVA: 0x002A9FB0 File Offset: 0x002A81B0
		public override void OnChildExecuted(TaskStatus childStatus)
		{
			if (this.childrenExecutionOrder.Count > 0)
			{
				this.childrenExecutionOrder.Pop();
			}
			this.executionStatus = childStatus;
		}

		// Token: 0x0600708E RID: 28814 RVA: 0x002A9FD3 File Offset: 0x002A81D3
		public override void OnConditionalAbort(int childIndex)
		{
			this.childrenExecutionOrder.Clear();
			this.executionStatus = 0;
			this.ShuffleChilden();
		}

		// Token: 0x0600708F RID: 28815 RVA: 0x002A9FED File Offset: 0x002A81ED
		public override void OnEnd()
		{
			this.executionStatus = 0;
			this.childrenExecutionOrder.Clear();
		}

		// Token: 0x06007090 RID: 28816 RVA: 0x002AA001 File Offset: 0x002A8201
		public override void OnReset()
		{
			this.seed = 0;
			this.useSeed = false;
		}

		// Token: 0x06007091 RID: 28817 RVA: 0x002AA014 File Offset: 0x002A8214
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

		// Token: 0x04005CB9 RID: 23737
		[Tooltip("Seed the random number generator to make things easier to debug")]
		public int seed;

		// Token: 0x04005CBA RID: 23738
		[Tooltip("Do we want to use the seed?")]
		public bool useSeed;

		// Token: 0x04005CBB RID: 23739
		private List<int> childIndexList = new List<int>();

		// Token: 0x04005CBC RID: 23740
		private Stack<int> childrenExecutionOrder = new Stack<int>();

		// Token: 0x04005CBD RID: 23741
		private TaskStatus executionStatus;
	}
}
