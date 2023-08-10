using System.Collections.Generic;
using KBEngine;

namespace script.MenPaiTask;

public class AllotTask
{
	private Avatar player => Tools.instance.getPlayer();

	public void GetCanAccpetNpcList()
	{
		List<int> canAccpetNpcIdList = player.ElderTaskMag.GetCanAccpetNpcIdList();
		List<ElderTask> waitAcceptTaskList = player.ElderTaskMag.GetWaitAcceptTaskList();
		if (canAccpetNpcIdList.Count > 0 && waitAcceptTaskList.Count > 0)
		{
			int count = canAccpetNpcIdList.Count;
			int count2 = waitAcceptTaskList.Count;
			for (int num = ((count < count2) ? count : count2) - 1; num >= 0; num--)
			{
				if (waitAcceptTaskList[num].BingNpc(canAccpetNpcIdList[num]))
				{
					player.ElderTaskMag.AddExecutingTaskNpcId(canAccpetNpcIdList[num]);
					player.ElderTaskMag.AddExecutingTask(waitAcceptTaskList[num]);
					player.ElderTaskMag.RemoveWaitAcceptTask(waitAcceptTaskList[num]);
				}
			}
		}
		player.ElderTaskMag.ClearCanAccpetNpcIdList();
	}
}
