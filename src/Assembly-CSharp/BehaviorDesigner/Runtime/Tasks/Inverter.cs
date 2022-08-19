using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02000FF3 RID: 4083
	[TaskDescription("The inverter task will invert the return value of the child task after it has finished executing. If the child returns success, the inverter task will return failure. If the child returns failure, the inverter task will return success.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=36")]
	[TaskIcon("{SkinColor}InverterIcon.png")]
	public class Inverter : Decorator
	{
		// Token: 0x06007105 RID: 28933 RVA: 0x002AB0E6 File Offset: 0x002A92E6
		public override bool CanExecute()
		{
			return this.executionStatus == null || this.executionStatus == 3;
		}

		// Token: 0x06007106 RID: 28934 RVA: 0x002AB0FB File Offset: 0x002A92FB
		public override void OnChildExecuted(TaskStatus childStatus)
		{
			this.executionStatus = childStatus;
		}

		// Token: 0x06007107 RID: 28935 RVA: 0x002AB104 File Offset: 0x002A9304
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

		// Token: 0x06007108 RID: 28936 RVA: 0x002AB113 File Offset: 0x002A9313
		public override void OnEnd()
		{
			this.executionStatus = 0;
		}

		// Token: 0x04005CFC RID: 23804
		private TaskStatus executionStatus;
	}
}
