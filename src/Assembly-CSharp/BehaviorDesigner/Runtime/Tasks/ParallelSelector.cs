using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02000FDD RID: 4061
	[TaskDescription("Similar to the selector task, the parallel selector task will return success as soon as a child task returns success. The difference is that the parallel task will run all of its children tasks simultaneously versus running each task one at a time. If one tasks returns success the parallel selector task will end all of the child tasks and return success. If every child task returns failure then the parallel selector task will return failure.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=28")]
	[TaskIcon("{SkinColor}ParallelSelectorIcon.png")]
	public class ParallelSelector : Composite
	{
		// Token: 0x0600706E RID: 28782 RVA: 0x002A9BAC File Offset: 0x002A7DAC
		public override void OnAwake()
		{
			this.executionStatus = new TaskStatus[this.children.Count];
		}

		// Token: 0x0600706F RID: 28783 RVA: 0x002A9BC4 File Offset: 0x002A7DC4
		public override void OnChildStarted(int childIndex)
		{
			this.currentChildIndex++;
			this.executionStatus[childIndex] = 3;
		}

		// Token: 0x06007070 RID: 28784 RVA: 0x00024C5F File Offset: 0x00022E5F
		public override bool CanRunParallelChildren()
		{
			return true;
		}

		// Token: 0x06007071 RID: 28785 RVA: 0x002A9BDD File Offset: 0x002A7DDD
		public override int CurrentChildIndex()
		{
			return this.currentChildIndex;
		}

		// Token: 0x06007072 RID: 28786 RVA: 0x002A9BE5 File Offset: 0x002A7DE5
		public override bool CanExecute()
		{
			return this.currentChildIndex < this.children.Count;
		}

		// Token: 0x06007073 RID: 28787 RVA: 0x002A9BFA File Offset: 0x002A7DFA
		public override void OnChildExecuted(int childIndex, TaskStatus childStatus)
		{
			this.executionStatus[childIndex] = childStatus;
		}

		// Token: 0x06007074 RID: 28788 RVA: 0x002A9C08 File Offset: 0x002A7E08
		public override void OnConditionalAbort(int childIndex)
		{
			this.currentChildIndex = 0;
			for (int i = 0; i < this.executionStatus.Length; i++)
			{
				this.executionStatus[i] = 0;
			}
		}

		// Token: 0x06007075 RID: 28789 RVA: 0x002A9C38 File Offset: 0x002A7E38
		public override TaskStatus OverrideStatus(TaskStatus status)
		{
			bool flag = true;
			for (int i = 0; i < this.executionStatus.Length; i++)
			{
				if (this.executionStatus[i] == 3)
				{
					flag = false;
				}
				else if (this.executionStatus[i] == 2)
				{
					return 2;
				}
			}
			if (!flag)
			{
				return 3;
			}
			return 1;
		}

		// Token: 0x06007076 RID: 28790 RVA: 0x002A9C7C File Offset: 0x002A7E7C
		public override void OnEnd()
		{
			for (int i = 0; i < this.executionStatus.Length; i++)
			{
				this.executionStatus[i] = 0;
			}
			this.currentChildIndex = 0;
		}

		// Token: 0x04005CAF RID: 23727
		private int currentChildIndex;

		// Token: 0x04005CB0 RID: 23728
		private TaskStatus[] executionStatus;
	}
}
