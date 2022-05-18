using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x0200149A RID: 5274
	[TaskDescription("The selector evaluator is a selector task which reevaluates its children every tick. It will run the lowest priority child which returns a task status of running. This is done each tick. If a higher priority child is running and the next frame a lower priority child wants to run it will interrupt the higher priority child. The selector evaluator will return success as soon as the first child returns success otherwise it will keep trying higher priority children. This task mimics the conditional abort functionality except the child tasks don't always have to be conditional tasks.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=109")]
	[TaskIcon("{SkinColor}SelectorEvaluatorIcon.png")]
	public class SelectorEvaluator : Composite
	{
		// Token: 0x06007E93 RID: 32403 RVA: 0x00055AE4 File Offset: 0x00053CE4
		public override int CurrentChildIndex()
		{
			return this.currentChildIndex;
		}

		// Token: 0x06007E94 RID: 32404 RVA: 0x00055AEC File Offset: 0x00053CEC
		public override void OnChildStarted(int childIndex)
		{
			this.currentChildIndex++;
			this.executionStatus = 3;
		}

		// Token: 0x06007E95 RID: 32405 RVA: 0x002C8FB8 File Offset: 0x002C71B8
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

		// Token: 0x06007E96 RID: 32406 RVA: 0x00055B03 File Offset: 0x00053D03
		public override void OnChildExecuted(int childIndex, TaskStatus childStatus)
		{
			if (childStatus != null && childStatus != 3)
			{
				this.executionStatus = childStatus;
			}
		}

		// Token: 0x06007E97 RID: 32407 RVA: 0x00055B13 File Offset: 0x00053D13
		public override void OnConditionalAbort(int childIndex)
		{
			this.currentChildIndex = childIndex;
			this.executionStatus = 0;
		}

		// Token: 0x06007E98 RID: 32408 RVA: 0x00055B23 File Offset: 0x00053D23
		public override void OnEnd()
		{
			this.executionStatus = 0;
			this.currentChildIndex = 0;
		}

		// Token: 0x06007E99 RID: 32409 RVA: 0x00055B33 File Offset: 0x00053D33
		public override TaskStatus OverrideStatus(TaskStatus status)
		{
			return this.executionStatus;
		}

		// Token: 0x06007E9A RID: 32410 RVA: 0x0000A093 File Offset: 0x00008293
		public override bool CanRunParallelChildren()
		{
			return true;
		}

		// Token: 0x06007E9B RID: 32411 RVA: 0x0000A093 File Offset: 0x00008293
		public override bool CanReevaluate()
		{
			return true;
		}

		// Token: 0x06007E9C RID: 32412 RVA: 0x00055B3B File Offset: 0x00053D3B
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

		// Token: 0x06007E9D RID: 32413 RVA: 0x002C9008 File Offset: 0x002C7208
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

		// Token: 0x04006BB8 RID: 27576
		private int currentChildIndex;

		// Token: 0x04006BB9 RID: 27577
		private TaskStatus executionStatus;

		// Token: 0x04006BBA RID: 27578
		private int storedCurrentChildIndex = -1;

		// Token: 0x04006BBB RID: 27579
		private TaskStatus storedExecutionStatus;
	}
}
