using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02000FE2 RID: 4066
	[TaskDescription("The selector evaluator is a selector task which reevaluates its children every tick. It will run the lowest priority child which returns a task status of running. This is done each tick. If a higher priority child is running and the next frame a lower priority child wants to run it will interrupt the higher priority child. The selector evaluator will return success as soon as the first child returns success otherwise it will keep trying higher priority children. This task mimics the conditional abort functionality except the child tasks don't always have to be conditional tasks.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=109")]
	[TaskIcon("{SkinColor}SelectorEvaluatorIcon.png")]
	public class SelectorEvaluator : Composite
	{
		// Token: 0x06007099 RID: 28825 RVA: 0x002AA101 File Offset: 0x002A8301
		public override int CurrentChildIndex()
		{
			return this.currentChildIndex;
		}

		// Token: 0x0600709A RID: 28826 RVA: 0x002AA109 File Offset: 0x002A8309
		public override void OnChildStarted(int childIndex)
		{
			this.currentChildIndex++;
			this.executionStatus = 3;
		}

		// Token: 0x0600709B RID: 28827 RVA: 0x002AA120 File Offset: 0x002A8320
		public override bool CanExecute()
		{
			if (this.executionStatus == 2 || this.executionStatus == 3)
			{
				return false;
			}
			if (this.storedCurrentChildIndex != -1)
			{
				return this.currentChildIndex < this.storedCurrentChildIndex - 1;
			}
			return this.currentChildIndex < this.children.Count;
		}

		// Token: 0x0600709C RID: 28828 RVA: 0x002AA16E File Offset: 0x002A836E
		public override void OnChildExecuted(int childIndex, TaskStatus childStatus)
		{
			if (childStatus != null && childStatus != 3)
			{
				this.executionStatus = childStatus;
			}
		}

		// Token: 0x0600709D RID: 28829 RVA: 0x002AA17E File Offset: 0x002A837E
		public override void OnConditionalAbort(int childIndex)
		{
			this.currentChildIndex = childIndex;
			this.executionStatus = 0;
		}

		// Token: 0x0600709E RID: 28830 RVA: 0x002AA18E File Offset: 0x002A838E
		public override void OnEnd()
		{
			this.executionStatus = 0;
			this.currentChildIndex = 0;
		}

		// Token: 0x0600709F RID: 28831 RVA: 0x002AA19E File Offset: 0x002A839E
		public override TaskStatus OverrideStatus(TaskStatus status)
		{
			return this.executionStatus;
		}

		// Token: 0x060070A0 RID: 28832 RVA: 0x00024C5F File Offset: 0x00022E5F
		public override bool CanRunParallelChildren()
		{
			return true;
		}

		// Token: 0x060070A1 RID: 28833 RVA: 0x00024C5F File Offset: 0x00022E5F
		public override bool CanReevaluate()
		{
			return true;
		}

		// Token: 0x060070A2 RID: 28834 RVA: 0x002AA1A6 File Offset: 0x002A83A6
		public override bool OnReevaluationStarted()
		{
			if (this.executionStatus == null)
			{
				return false;
			}
			this.storedCurrentChildIndex = this.currentChildIndex;
			this.storedExecutionStatus = this.executionStatus;
			this.currentChildIndex = 0;
			this.executionStatus = 0;
			return true;
		}

		// Token: 0x060070A3 RID: 28835 RVA: 0x002AA1DC File Offset: 0x002A83DC
		public override void OnReevaluationEnded(TaskStatus status)
		{
			if (this.executionStatus != 1 && this.executionStatus != null)
			{
				BehaviorManager.instance.Interrupt(base.Owner, this.children[this.storedCurrentChildIndex - 1], this);
			}
			else
			{
				this.currentChildIndex = this.storedCurrentChildIndex;
				this.executionStatus = this.storedExecutionStatus;
			}
			this.storedCurrentChildIndex = -1;
			this.storedExecutionStatus = 0;
		}

		// Token: 0x04005CC0 RID: 23744
		private int currentChildIndex;

		// Token: 0x04005CC1 RID: 23745
		private TaskStatus executionStatus;

		// Token: 0x04005CC2 RID: 23746
		private int storedCurrentChildIndex = -1;

		// Token: 0x04005CC3 RID: 23747
		private TaskStatus storedExecutionStatus;
	}
}
