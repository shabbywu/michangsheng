using System;
using System.Collections.Generic;
using JSONClass;
using UnityEngine;

namespace Task
{
	// Token: 0x02000A26 RID: 2598
	[Serializable]
	public class TaskMag
	{
		// Token: 0x0600435A RID: 17242 RVA: 0x00030200 File Offset: 0x0002E400
		public void Init()
		{
			if (this.HasGetTaskId == null)
			{
				this.HasGetTaskId = new List<int>();
			}
			if (this.HasTaskNpcList == null)
			{
				this.HasTaskNpcList = new List<int>();
			}
		}

		// Token: 0x0600435B RID: 17243 RVA: 0x001CC6AC File Offset: 0x001CA8AC
		public void CheckHasOut()
		{
			List<int> list = new List<int>();
			int num = 0;
			foreach (int num2 in this.TaskDict.Keys)
			{
				TaskData taskData = this.TaskDict[num2];
				if (taskData.IsFail(out num))
				{
					if (taskData.FailStaticId > 0)
					{
						GlobalValue.Set(taskData.FailStaticId, 0, "TaskMag.CheckHasOut 把变量置为0告诉策划任务失败");
					}
					if (Tools.instance.getPlayer().taskMag._TaskData["Task"].HasField(taskData.TaskId.ToString()))
					{
						Tools.instance.getPlayer().taskMag._TaskData["Task"][string.Concat(taskData.TaskId)].SetField("isComplete", true);
						Tools.instance.getPlayer().taskMag._TaskData["Task"][string.Concat(taskData.TaskId)].SetField("disableTask", true);
					}
					if (num > 1)
					{
						int randomInt = Tools.instance.GetRandomInt(1, 3);
						JSONObject npcData = NpcJieSuanManager.inst.getNpcData(taskData.SendNpcId);
						int duiBaiId = NpcJieSuanManager.inst.GetDuiBaiId(npcData["XingGe"].I, jsonData.instance.CyRandomTaskFailData[taskData.TaskId.ToString()]["ShiBaiInfo" + num].I);
						Tools.instance.getPlayer().emailDateMag.TaskFailSendToPlayer(this.TaskDict[num2].SendNpcId, duiBaiId, randomInt, NpcJieSuanManager.inst.JieSuanTime);
						if (taskData.TaskType == 1)
						{
							this.HasTaskNpcList.Remove(taskData.SendNpcId);
						}
						list.Add(num2);
						npcData.RemoveField("LockAction");
					}
				}
				else if (Tools.instance.getPlayer().taskMag._TaskData["Task"].HasField(taskData.TaskId.ToString()) && TaskUIManager.checkIsGuoShi(Tools.instance.getPlayer().taskMag._TaskData["Task"][taskData.TaskId.ToString()]))
				{
					JSONObject npcData = NpcJieSuanManager.inst.getNpcData(taskData.SendNpcId);
					npcData.RemoveField("LockAction");
					if (taskData.TaskType == 1)
					{
						this.HasTaskNpcList.Remove(taskData.SendNpcId);
					}
					list.Add(num2);
				}
			}
			foreach (int key in list)
			{
				this.TaskDict.Remove(key);
			}
		}

		// Token: 0x0600435C RID: 17244 RVA: 0x001CC9D4 File Offset: 0x001CABD4
		public void AddTask(int TaskId, int taskType, int CyId, int NpcId, int failStaticId, DateTime SendTime)
		{
			if (!this.HasGetTaskId.Contains(TaskId))
			{
				this.HasGetTaskId.Add(TaskId);
			}
			TaskData taskData = new TaskData(TaskId, taskType, CyId, NpcId, failStaticId, SendTime);
			if (this.TaskDict.ContainsKey(TaskId))
			{
				Debug.LogError("存在相同的任务");
				return;
			}
			if (taskData.FailTypeList.Count > 0)
			{
				this.TaskDict.Add(TaskId, taskData);
			}
			if (taskType == 1)
			{
				this.HasTaskNpcList.Add(NpcId);
			}
			if (!Tools.instance.getPlayer().taskMag.isHasTask(TaskId))
			{
				Tools.instance.getPlayer().taskMag.addTask(SendTime.ToString(), TaskId);
				string name = TaskJsonData.DataDict[TaskId].Name;
				string msg = (TaskJsonData.DataDict[TaskId].Type == 0) ? "获得一条新的传闻" : ("<color=#FF0000>" + name + "</color>任务已开启");
				UIPopTip.Inst.Pop(msg, PopTipIconType.任务进度);
			}
		}

		// Token: 0x04003B5D RID: 15197
		public Dictionary<int, TaskData> TaskDict = new Dictionary<int, TaskData>();

		// Token: 0x04003B5E RID: 15198
		public List<int> HasGetTaskId = new List<int>();

		// Token: 0x04003B5F RID: 15199
		public List<int> HasTaskNpcList = new List<int>();
	}
}
