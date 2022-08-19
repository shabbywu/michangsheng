using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02000FCE RID: 4046
	[TaskDescription("Returns a TaskStatus of running. Will only stop when interrupted or a conditional abort is triggered.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=112")]
	[TaskIcon("{SkinColor}IdleIcon.png")]
	public class Idle : Action
	{
		// Token: 0x0600702C RID: 28716 RVA: 0x001709C1 File Offset: 0x0016EBC1
		public override TaskStatus OnUpdate()
		{
			return 3;
		}
	}
}
