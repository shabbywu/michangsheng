using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020014AB RID: 5291
	[TaskDescription("The inverter task will invert the return value of the child task after it has finished executing. If the child returns success, the inverter task will return failure. If the child returns failure, the inverter task will return success.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=36")]
	[TaskIcon("{SkinColor}InverterIcon.png")]
	public class Inverter : Decorator
	{
		// Token: 0x06007EFF RID: 32511 RVA: 0x000560D7 File Offset: 0x000542D7
		public override bool CanExecute()
		{
			return this.executionStatus == null || this.executionStatus == 3;
		}

		// Token: 0x06007F00 RID: 32512 RVA: 0x000560EC File Offset: 0x000542EC
		public override void OnChildExecuted(TaskStatus childStatus)
		{
			this.executionStatus = childStatus;
		}

		// Token: 0x06007F01 RID: 32513 RVA: 0x000560F5 File Offset: 0x000542F5
		public override TaskStatus Decorate(TaskStatus status)
		{
			if (status == 2)
			{
				return 1;
			}
			if (status == 1)
			{
				return 2;
			}
			return status;
		}

		// Token: 0x06007F02 RID: 32514 RVA: 0x00056104 File Offset: 0x00054304
		public override void OnEnd()
		{
			this.executionStatus = 0;
		}

		// Token: 0x04006BF4 RID: 27636
		private TaskStatus executionStatus;
	}
}
