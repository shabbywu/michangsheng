using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02001486 RID: 5254
	[TaskDescription("Returns a TaskStatus of running. Will only stop when interrupted or a conditional abort is triggered.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=112")]
	[TaskIcon("{SkinColor}IdleIcon.png")]
	public class Idle : Action
	{
		// Token: 0x06007E26 RID: 32294 RVA: 0x0002D5FA File Offset: 0x0002B7FA
		public override TaskStatus OnUpdate()
		{
			return 3;
		}
	}
}
