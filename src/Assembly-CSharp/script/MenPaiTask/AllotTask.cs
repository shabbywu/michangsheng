using System;
using System.Collections.Generic;
using KBEngine;

namespace script.MenPaiTask
{
	// Token: 0x02000A06 RID: 2566
	public class AllotTask
	{
		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x0600470D RID: 18189 RVA: 0x0006EC50 File Offset: 0x0006CE50
		private Avatar player
		{
			get
			{
				return Tools.instance.getPlayer();
			}
		}

		// Token: 0x0600470E RID: 18190 RVA: 0x001E2030 File Offset: 0x001E0230
		public void GetCanAccpetNpcList()
		{
			List<int> canAccpetNpcIdList = this.player.ElderTaskMag.GetCanAccpetNpcIdList();
			List<ElderTask> waitAcceptTaskList = this.player.ElderTaskMag.GetWaitAcceptTaskList();
			if (canAccpetNpcIdList.Count > 0 && waitAcceptTaskList.Count > 0)
			{
				int count = canAccpetNpcIdList.Count;
				int count2 = waitAcceptTaskList.Count;
				for (int i = ((count < count2) ? count : count2) - 1; i >= 0; i--)
				{
					if (waitAcceptTaskList[i].BingNpc(canAccpetNpcIdList[i]))
					{
						this.player.ElderTaskMag.AddExecutingTaskNpcId(canAccpetNpcIdList[i]);
						this.player.ElderTaskMag.AddExecutingTask(waitAcceptTaskList[i]);
						this.player.ElderTaskMag.RemoveWaitAcceptTask(waitAcceptTaskList[i]);
					}
				}
			}
			this.player.ElderTaskMag.ClearCanAccpetNpcIdList();
		}
	}
}
