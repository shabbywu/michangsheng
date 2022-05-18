using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020014B2 RID: 5298
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=23")]
	[TaskIcon("{SkinColor}EntryIcon.png")]
	public class EntryTask : ParentTask
	{
		// Token: 0x06007F22 RID: 32546 RVA: 0x0000A093 File Offset: 0x00008293
		public override int MaxChildren()
		{
			return 1;
		}
	}
}
