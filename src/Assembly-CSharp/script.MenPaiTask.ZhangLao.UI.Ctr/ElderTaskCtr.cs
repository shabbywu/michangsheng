using System.Collections.Generic;
using UnityEngine;
using script.MenPaiTask.ZhangLao.UI.UI;

namespace script.MenPaiTask.ZhangLao.UI.Ctr;

public class ElderTaskCtr
{
	public List<BaseElderTask> TaskList = new List<BaseElderTask>();

	public ElderTaskUI UI => ElderTaskUIMag.Inst.ElderTaskUI;

	public void CreateTaskList()
	{
		ElderTaskMag elderTaskMag = Tools.instance.getPlayer().ElderTaskMag;
		Clear();
		foreach (ElderTask completeTask in elderTaskMag.GetCompleteTaskList())
		{
			GameObject gameObject = UI.已完成.Inst(UI.任务列表列表);
			BaseElderTask item = BaseElderTask.Create<CompleteElderTask>(completeTask, gameObject);
			TaskList.Add(item);
		}
		foreach (ElderTask waitAcceptTask in elderTaskMag.GetWaitAcceptTaskList())
		{
			GameObject gameObject2 = UI.待接取.Inst(UI.任务列表列表);
			BaseElderTask item2 = BaseElderTask.Create<WaitAcceptElderTask>(waitAcceptTask, gameObject2);
			TaskList.Add(item2);
		}
		foreach (ElderTask executingTask in elderTaskMag.GetExecutingTaskList())
		{
			GameObject gameObject3 = UI.执行中.Inst(UI.任务列表列表);
			BaseElderTask item3 = BaseElderTask.Create<ExecutingElderTask>(executingTask, gameObject3);
			TaskList.Add(item3);
		}
	}

	private void Clear()
	{
		for (int num = TaskList.Count - 1; num >= 0; num--)
		{
			TaskList[num].DestroySelf();
		}
	}
}
