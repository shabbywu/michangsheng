using System;
using System.Collections.Generic;
using KBEngine;

namespace script.MenPaiTask.ZhangLao.ElderTaskSys
{
	// Token: 0x02000A15 RID: 2581
	public class UpdateTaskProcess
	{
		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x0600476F RID: 18287 RVA: 0x0006EC50 File Offset: 0x0006CE50
		private Avatar player
		{
			get
			{
				return Tools.instance.getPlayer();
			}
		}

		// Token: 0x06004770 RID: 18288 RVA: 0x001E3A88 File Offset: 0x001E1C88
		public void CheckHasExecutingTask()
		{
			List<ElderTask> executingTaskList = this.player.ElderTaskMag.GetExecutingTaskList();
			if (executingTaskList.Count > 0)
			{
				for (int i = executingTaskList.Count - 1; i >= 0; i--)
				{
					if (NPCEx.IsDeath(executingTaskList[i].NpcId))
					{
						this.player.ElderTaskMag.RemoveExecutingTaskNpcId(executingTaskList[i].NpcId);
						executingTaskList[i].UnBingNpc();
						this.player.ElderTaskMag.AddWaitAcceptTask(executingTaskList[i]);
						this.player.ElderTaskMag.RemoveExecutingTask(executingTaskList[i]);
					}
					else
					{
						this.AddTaskProcess(executingTaskList[i]);
					}
				}
			}
		}

		// Token: 0x06004771 RID: 18289 RVA: 0x001E3B40 File Offset: 0x001E1D40
		public void CheckHasExecutingTask(int times)
		{
			List<ElderTask> executingTaskList = this.player.ElderTaskMag.GetExecutingTaskList();
			if (executingTaskList.Count > 0)
			{
				for (int i = executingTaskList.Count - 1; i >= 0; i--)
				{
					if (NPCEx.IsDeath(executingTaskList[i].NpcId))
					{
						this.player.ElderTaskMag.RemoveExecutingTaskNpcId(executingTaskList[i].NpcId);
						executingTaskList[i].UnBingNpc();
						this.player.ElderTaskMag.AddWaitAcceptTask(executingTaskList[i]);
						this.player.ElderTaskMag.RemoveExecutingTask(executingTaskList[i]);
					}
					else
					{
						this.AddTaskProcess(executingTaskList[i], times);
					}
				}
			}
		}

		// Token: 0x06004772 RID: 18290 RVA: 0x001E3BFC File Offset: 0x001E1DFC
		private void AddTaskProcess(ElderTask task)
		{
			task.HasCostTime++;
			if (task.HasCostTime == task.NeedCostTime)
			{
				NpcJieSuanManager.inst.npcSetField.AddNpcMoney(task.NpcId, task.Money);
				this.player.ElderTaskMag.RemoveExecutingTaskNpcId(task.NpcId);
				this.player.ElderTaskMag.CompleteTask(task);
			}
		}

		// Token: 0x06004773 RID: 18291 RVA: 0x001E3C68 File Offset: 0x001E1E68
		private void AddTaskProcess(ElderTask task, int count)
		{
			task.HasCostTime += count;
			if (task.HasCostTime >= task.NeedCostTime)
			{
				NpcJieSuanManager.inst.npcSetField.AddNpcMoney(task.NpcId, task.Money);
				this.player.ElderTaskMag.RemoveExecutingTaskNpcId(task.NpcId);
				this.player.ElderTaskMag.CompleteTask(task);
			}
		}
	}
}
