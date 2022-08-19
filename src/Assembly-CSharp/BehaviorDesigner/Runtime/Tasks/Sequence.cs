using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02000FE3 RID: 4067
	[TaskDescription("The sequence task is similar to an \"and\" operation. It will return failure as soon as one of its child tasks return failure. If a child task returns success then it will sequentially run the next task. If all child tasks return success then it will return success.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=25")]
	[TaskIcon("{SkinColor}SequenceIcon.png")]
	public class Sequence : Composite
	{
		// Token: 0x060070A5 RID: 28837 RVA: 0x002AA255 File Offset: 0x002A8455
		public override int CurrentChildIndex()
		{
			return this.currentChildIndex;
		}

		// Token: 0x060070A6 RID: 28838 RVA: 0x002AA25D File Offset: 0x002A845D
		public override bool CanExecute()
		{
			return this.currentChildIndex < this.children.Count && this.executionStatus != 1;
		}

		// Token: 0x060070A7 RID: 28839 RVA: 0x002AA280 File Offset: 0x002A8480
		public override void OnChildExecuted(TaskStatus childStatus)
		{
			this.currentChildIndex++;
			this.executionStatus = childStatus;
		}

		// Token: 0x060070A8 RID: 28840 RVA: 0x002AA297 File Offset: 0x002A8497
		public override void OnConditionalAbort(int childIndex)
		{
			this.currentChildIndex = childIndex;
			this.executionStatus = 0;
		}

		// Token: 0x060070A9 RID: 28841 RVA: 0x002AA2A7 File Offset: 0x002A84A7
		public override void OnEnd()
		{
			this.executionStatus = 0;
			this.currentChildIndex = 0;
		}

		// Token: 0x04005CC4 RID: 23748
		private int currentChildIndex;

		// Token: 0x04005CC5 RID: 23749
		private TaskStatus executionStatus;
	}
}
