using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x0200149B RID: 5275
	[TaskDescription("The sequence task is similar to an \"and\" operation. It will return failure as soon as one of its child tasks return failure. If a child task returns success then it will sequentially run the next task. If all child tasks return success then it will return success.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=25")]
	[TaskIcon("{SkinColor}SequenceIcon.png")]
	public class Sequence : Composite
	{
		// Token: 0x06007E9F RID: 32415 RVA: 0x00055B7D File Offset: 0x00053D7D
		public override int CurrentChildIndex()
		{
			return this.currentChildIndex;
		}

		// Token: 0x06007EA0 RID: 32416 RVA: 0x00055B85 File Offset: 0x00053D85
		public override bool CanExecute()
		{
			return this.currentChildIndex < this.children.Count && this.executionStatus != 1;
		}

		// Token: 0x06007EA1 RID: 32417 RVA: 0x00055BA8 File Offset: 0x00053DA8
		public override void OnChildExecuted(TaskStatus childStatus)
		{
			this.currentChildIndex++;
			this.executionStatus = childStatus;
		}

		// Token: 0x06007EA2 RID: 32418 RVA: 0x00055BBF File Offset: 0x00053DBF
		public override void OnConditionalAbort(int childIndex)
		{
			this.currentChildIndex = childIndex;
			this.executionStatus = 0;
		}

		// Token: 0x06007EA3 RID: 32419 RVA: 0x00055BCF File Offset: 0x00053DCF
		public override void OnEnd()
		{
			this.executionStatus = 0;
			this.currentChildIndex = 0;
		}

		// Token: 0x04006BBC RID: 27580
		private int currentChildIndex;

		// Token: 0x04006BBD RID: 27581
		private TaskStatus executionStatus;
	}
}
