using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02000FFA RID: 4090
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=23")]
	[TaskIcon("{SkinColor}EntryIcon.png")]
	public class EntryTask : ParentTask
	{
		// Token: 0x06007128 RID: 28968 RVA: 0x00024C5F File Offset: 0x00022E5F
		public override int MaxChildren()
		{
			return 1;
		}
	}
}
