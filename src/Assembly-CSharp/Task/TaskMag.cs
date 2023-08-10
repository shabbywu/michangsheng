using System;
using System.Collections.Generic;
using JSONClass;
using UnityEngine;

namespace Task;

[Serializable]
public class TaskMag
{
	public Dictionary<int, TaskData> TaskDict = new Dictionary<int, TaskData>();

	public List<int> HasGetTaskId = new List<int>();

	public List<int> HasTaskNpcList = new List<int>();

	public void Init()
	{
		if (HasGetTaskId == null)
		{
			HasGetTaskId = new List<int>();
		}
		if (HasTaskNpcList == null)
		{
			HasTaskNpcList = new List<int>();
		}
	}

	public void CheckHasOut()
	{
		List<int> list = new List<int>();
		int failType = 0;
		JSONObject jSONObject = null;
		TaskData taskData = null;
		foreach (int key in TaskDict.Keys)
		{
			taskData = TaskDict[key];
			if (taskData.IsFail(out failType))
			{
				if (taskData.FailStaticId > 0)
				{
					GlobalValue.Set(taskData.FailStaticId, 0, "TaskMag.CheckHasOut 把变量置为0告诉策划任务失败");
				}
				if (Tools.instance.getPlayer().taskMag._TaskData["Task"].HasField(taskData.TaskId.ToString()))
				{
					Tools.instance.getPlayer().taskMag._TaskData["Task"][string.Concat(taskData.TaskId)].SetField("isComplete", val: true);
					Tools.instance.getPlayer().taskMag._TaskData["Task"][string.Concat(taskData.TaskId)].SetField("disableTask", val: true);
				}
				if (failType > 1)
				{
					int randomInt = Tools.instance.GetRandomInt(1, 3);
					jSONObject = NpcJieSuanManager.inst.GetNpcData(taskData.SendNpcId);
					int duiBaiId = NpcJieSuanManager.inst.GetDuiBaiId(jSONObject["XingGe"].I, jsonData.instance.CyRandomTaskFailData[taskData.TaskId.ToString()]["ShiBaiInfo" + failType].I);
					Tools.instance.getPlayer().emailDateMag.TaskFailSendToPlayer(TaskDict[key].SendNpcId, duiBaiId, randomInt, NpcJieSuanManager.inst.JieSuanTime);
					if (taskData.TaskType == 1)
					{
						HasTaskNpcList.Remove(taskData.SendNpcId);
					}
					list.Add(key);
					jSONObject.RemoveField("LockAction");
				}
			}
			else if (Tools.instance.getPlayer().taskMag._TaskData["Task"].HasField(taskData.TaskId.ToString()) && TaskUIManager.checkIsGuoShi(Tools.instance.getPlayer().taskMag._TaskData["Task"][taskData.TaskId.ToString()]))
			{
				jSONObject = NpcJieSuanManager.inst.GetNpcData(taskData.SendNpcId);
				jSONObject.RemoveField("LockAction");
				if (taskData.TaskType == 1)
				{
					HasTaskNpcList.Remove(taskData.SendNpcId);
				}
				list.Add(key);
			}
		}
		foreach (int item in list)
		{
			TaskDict.Remove(item);
		}
	}

	public void AddTask(int TaskId, int taskType, int CyId, int NpcId, int failStaticId, DateTime SendTime)
	{
		if (!HasGetTaskId.Contains(TaskId))
		{
			HasGetTaskId.Add(TaskId);
		}
		TaskData taskData = new TaskData(TaskId, taskType, CyId, NpcId, failStaticId, SendTime);
		if (TaskDict.ContainsKey(TaskId))
		{
			Debug.LogError((object)"存在相同的任务");
			return;
		}
		if (taskData.FailTypeList.Count > 0)
		{
			TaskDict.Add(TaskId, taskData);
		}
		if (taskType == 1)
		{
			HasTaskNpcList.Add(NpcId);
		}
		if (!Tools.instance.getPlayer().taskMag.isHasTask(TaskId))
		{
			Tools.instance.getPlayer().taskMag.addTask(SendTime.ToString(), TaskId);
			string name = TaskJsonData.DataDict[TaskId].Name;
			string msg = ((TaskJsonData.DataDict[TaskId].Type == 0) ? "获得一条新的传闻" : ("<color=#FF0000>" + name + "</color>任务已开启"));
			UIPopTip.Inst.Pop(msg, PopTipIconType.任务进度);
		}
	}
}
