using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02000FE1 RID: 4065
	[TaskDescription("The selector task is similar to an \"or\" operation. It will return success as soon as one of its child tasks return success. If a child task returns failure then it will sequentially run the next task. If no child task returns success then it will return failure.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=26")]
	[TaskIcon("{SkinColor}SelectorIcon.png")]
	public class Selector : Composite
	{
		// Token: 0x06007093 RID: 28819 RVA: 0x002AA09F File Offset: 0x002A829F
		public override int CurrentChildIndex()
		{
			return this.currentChildIndex;
		}

		// Token: 0x06007094 RID: 28820 RVA: 0x002AA0A7 File Offset: 0x002A82A7
		public override bool CanExecute()
		{
			return this.currentChildIndex < this.children.Count && this.executionStatus != 2;
		}

		// Token: 0x06007095 RID: 28821 RVA: 0x002AA0CA File Offset: 0x002A82CA
		public override void OnChildExecuted(TaskStatus childStatus)
		{
			this.currentChildIndex++;
			this.executionStatus = childStatus;
		}

		// Token: 0x06007096 RID: 28822 RVA: 0x002AA0E1 File Offset: 0x002A82E1
		public override void OnConditionalAbort(int childIndex)
		{
			this.currentChildIndex = childIndex;
			this.executionStatus = 0;
		}

		// Token: 0x06007097 RID: 28823 RVA: 0x002AA0F1 File Offset: 0x002A82F1
		public override void OnEnd()
		{
			this.executionStatus = 0;
			this.currentChildIndex = 0;
		}

		// Token: 0x04005CBE RID: 23742
		private int currentChildIndex;

		// Token: 0x04005CBF RID: 23743
		private TaskStatus executionStatus;
	}
}
