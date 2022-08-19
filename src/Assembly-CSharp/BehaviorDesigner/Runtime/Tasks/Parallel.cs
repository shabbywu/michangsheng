using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02000FDB RID: 4059
	[TaskDescription("Similar to the sequence task, the parallel task will run each child task until a child task returns failure. The difference is that the parallel task will run all of its children tasks simultaneously versus running each task one at a time. Like the sequence class, the parallel task will return success once all of its children tasks have return success. If one tasks returns failure the parallel task will end all of the child tasks and return failure.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=27")]
	[TaskIcon("{SkinColor}ParallelIcon.png")]
	public class Parallel : Composite
	{
		// Token: 0x0600705A RID: 28762 RVA: 0x002A999F File Offset: 0x002A7B9F
		public override void OnAwake()
		{
			this.executionStatus = new TaskStatus[this.children.Count];
		}

		// Token: 0x0600705B RID: 28763 RVA: 0x002A99B7 File Offset: 0x002A7BB7
		public override void OnChildStarted(int childIndex)
		{
			this.currentChildIndex++;
			this.executionStatus[childIndex] = 3;
		}

		// Token: 0x0600705C RID: 28764 RVA: 0x00024C5F File Offset: 0x00022E5F
		public override bool CanRunParallelChildren()
		{
			return true;
		}

		// Token: 0x0600705D RID: 28765 RVA: 0x002A99D0 File Offset: 0x002A7BD0
		public override int CurrentChildIndex()
		{
			return this.currentChildIndex;
		}

		// Token: 0x0600705E RID: 28766 RVA: 0x002A99D8 File Offset: 0x002A7BD8
		public override bool CanExecute()
		{
			return this.currentChildIndex < this.children.Count;
		}

		// Token: 0x0600705F RID: 28767 RVA: 0x002A99ED File Offset: 0x002A7BED
		public override void OnChildExecuted(int childIndex, TaskStatus childStatus)
		{
			this.executionStatus[childIndex] = childStatus;
		}

		// Token: 0x06007060 RID: 28768 RVA: 0x002A99F8 File Offset: 0x002A7BF8
		public override TaskStatus OverrideStatus(TaskStatus status)
		{
			bool flag = true;
			for (int i = 0; i < this.executionStatus.Length; i++)
			{
				if (this.executionStatus[i] == 3)
				{
					flag = false;
				}
				else if (this.executionStatus[i] == 1)
				{
					return 1;
				}
			}
			if (!flag)
			{
				return 3;
			}
			return 2;
		}

		// Token: 0x06007061 RID: 28769 RVA: 0x002A9A3C File Offset: 0x002A7C3C
		public override void OnConditionalAbort(int childIndex)
		{
			this.currentChildIndex = 0;
			for (int i = 0; i < this.executionStatus.Length; i++)
			{
				this.executionStatus[i] = 0;
			}
		}

		// Token: 0x06007062 RID: 28770 RVA: 0x002A9A6C File Offset: 0x002A7C6C
		public override void OnEnd()
		{
			for (int i = 0; i < this.executionStatus.Length; i++)
			{
				this.executionStatus[i] = 0;
			}
			this.currentChildIndex = 0;
		}

		// Token: 0x04005CAB RID: 23723
		private int currentChildIndex;

		// Token: 0x04005CAC RID: 23724
		private TaskStatus[] executionStatus;
	}
}
