using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02001499 RID: 5273
	[TaskDescription("The selector task is similar to an \"or\" operation. It will return success as soon as one of its child tasks return success. If a child task returns failure then it will sequentially run the next task. If no child task returns success then it will return failure.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=26")]
	[TaskIcon("{SkinColor}SelectorIcon.png")]
	public class Selector : Composite
	{
		// Token: 0x06007E8D RID: 32397 RVA: 0x00055A82 File Offset: 0x00053C82
		public override int CurrentChildIndex()
		{
			return this.currentChildIndex;
		}

		// Token: 0x06007E8E RID: 32398 RVA: 0x00055A8A File Offset: 0x00053C8A
		public override bool CanExecute()
		{
			return this.currentChildIndex < this.children.Count && this.executionStatus != 2;
		}

		// Token: 0x06007E8F RID: 32399 RVA: 0x00055AAD File Offset: 0x00053CAD
		public override void OnChildExecuted(TaskStatus childStatus)
		{
			this.currentChildIndex++;
			this.executionStatus = childStatus;
		}

		// Token: 0x06007E90 RID: 32400 RVA: 0x00055AC4 File Offset: 0x00053CC4
		public override void OnConditionalAbort(int childIndex)
		{
			this.currentChildIndex = childIndex;
			this.executionStatus = 0;
		}

		// Token: 0x06007E91 RID: 32401 RVA: 0x00055AD4 File Offset: 0x00053CD4
		public override void OnEnd()
		{
			this.executionStatus = 0;
			this.currentChildIndex = 0;
		}

		// Token: 0x04006BB6 RID: 27574
		private int currentChildIndex;

		// Token: 0x04006BB7 RID: 27575
		private TaskStatus executionStatus;
	}
}
