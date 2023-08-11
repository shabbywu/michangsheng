using System.Collections.Generic;
using KBEngine;

namespace script.MenPaiTask.ZhangLao.ElderTaskSys;

public class UpdateTaskProcess
{
	private Avatar player => Tools.instance.getPlayer();

	public void CheckHasExecutingTask()
	{
		List<ElderTask> executingTaskList = player.ElderTaskMag.GetExecutingTaskList();
		if (executingTaskList.Count <= 0)
		{
			return;
		}
		for (int num = executingTaskList.Count - 1; num >= 0; num--)
		{
			if (NPCEx.IsDeath(executingTaskList[num].NpcId))
			{
				player.ElderTaskMag.RemoveExecutingTaskNpcId(executingTaskList[num].NpcId);
				executingTaskList[num].UnBingNpc();
				player.ElderTaskMag.AddWaitAcceptTask(executingTaskList[num]);
				player.ElderTaskMag.RemoveExecutingTask(executingTaskList[num]);
			}
			else
			{
				AddTaskProcess(executingTaskList[num]);
			}
		}
	}

	public void CheckHasExecutingTask(int times)
	{
		List<ElderTask> executingTaskList = player.ElderTaskMag.GetExecutingTaskList();
		if (executingTaskList.Count <= 0)
		{
			return;
		}
		for (int num = executingTaskList.Count - 1; num >= 0; num--)
		{
			if (NPCEx.IsDeath(executingTaskList[num].NpcId))
			{
				player.ElderTaskMag.RemoveExecutingTaskNpcId(executingTaskList[num].NpcId);
				executingTaskList[num].UnBingNpc();
				player.ElderTaskMag.AddWaitAcceptTask(executingTaskList[num]);
				player.ElderTaskMag.RemoveExecutingTask(executingTaskList[num]);
			}
			else
			{
				AddTaskProcess(executingTaskList[num], times);
			}
		}
	}

	private void AddTaskProcess(ElderTask task)
	{
		task.HasCostTime++;
		if (task.HasCostTime == task.NeedCostTime)
		{
			NpcJieSuanManager.inst.npcSetField.AddNpcMoney(task.NpcId, task.Money);
			player.ElderTaskMag.RemoveExecutingTaskNpcId(task.NpcId);
			player.ElderTaskMag.CompleteTask(task);
		}
	}

	private void AddTaskProcess(ElderTask task, int count)
	{
		task.HasCostTime += count;
		if (task.HasCostTime >= task.NeedCostTime)
		{
			NpcJieSuanManager.inst.npcSetField.AddNpcMoney(task.NpcId, task.Money);
			player.ElderTaskMag.RemoveExecutingTaskNpcId(task.NpcId);
			player.ElderTaskMag.CompleteTask(task);
		}
	}
}
