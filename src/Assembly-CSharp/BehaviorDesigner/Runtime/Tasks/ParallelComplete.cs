using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02000FDC RID: 4060
	[TaskDescription("Similar to the parallel selector task, except the parallel complete task will return the child status as soon as the child returns success or failure.The child tasks are executed simultaneously.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=136")]
	[TaskIcon("{SkinColor}ParallelCompleteIcon.png")]
	public class ParallelComplete : Composite
	{
		// Token: 0x06007064 RID: 28772 RVA: 0x002A9AA4 File Offset: 0x002A7CA4
		public override void OnAwake()
		{
			this.executionStatus = new TaskStatus[this.children.Count];
		}

		// Token: 0x06007065 RID: 28773 RVA: 0x002A9ABC File Offset: 0x002A7CBC
		public override void OnChildStarted(int childIndex)
		{
			this.currentChildIndex++;
			this.executionStatus[childIndex] = 3;
		}

		// Token: 0x06007066 RID: 28774 RVA: 0x00024C5F File Offset: 0x00022E5F
		public override bool CanRunParallelChildren()
		{
			return true;
		}

		// Token: 0x06007067 RID: 28775 RVA: 0x002A9AD5 File Offset: 0x002A7CD5
		public override int CurrentChildIndex()
		{
			return this.currentChildIndex;
		}

		// Token: 0x06007068 RID: 28776 RVA: 0x002A9ADD File Offset: 0x002A7CDD
		public override bool CanExecute()
		{
			return this.currentChildIndex < this.children.Count;
		}

		// Token: 0x06007069 RID: 28777 RVA: 0x002A9AF2 File Offset: 0x002A7CF2
		public override void OnChildExecuted(int childIndex, TaskStatus childStatus)
		{
			this.executionStatus[childIndex] = childStatus;
		}

		// Token: 0x0600706A RID: 28778 RVA: 0x002A9B00 File Offset: 0x002A7D00
		public override void OnConditionalAbort(int childIndex)
		{
			this.currentChildIndex = 0;
			for (int i = 0; i < this.executionStatus.Length; i++)
			{
				this.executionStatus[i] = 0;
			}
		}

		// Token: 0x0600706B RID: 28779 RVA: 0x002A9B30 File Offset: 0x002A7D30
		public override TaskStatus OverrideStatus(TaskStatus status)
		{
			for (int i = 0; i < this.executionStatus.Length; i++)
			{
				if (this.executionStatus[i] == 2 || this.executionStatus[i] == 1)
				{
					return this.executionStatus[i];
				}
				if (this.executionStatus[i] == null)
				{
					return 2;
				}
			}
			return 3;
		}

		// Token: 0x0600706C RID: 28780 RVA: 0x002A9B7C File Offset: 0x002A7D7C
		public override void OnEnd()
		{
			for (int i = 0; i < this.executionStatus.Length; i++)
			{
				this.executionStatus[i] = 0;
			}
			this.currentChildIndex = 0;
		}

		// Token: 0x04005CAD RID: 23725
		private int currentChildIndex;

		// Token: 0x04005CAE RID: 23726
		private TaskStatus[] executionStatus;
	}
}
