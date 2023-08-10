using System;
using System.Collections.Generic;
using JSONClass;

namespace Task;

[Serializable]
public class TaskData
{
	public int TaskId;

	public int TaskType;

	public int CyId;

	public int SendNpcId;

	public int FailStaticId;

	public List<int> FailTypeList;

	public int FailLevel;

	public DateTime EndTime;

	public TaskData(int TaskId, int taskType, int CyId, int NpcId, int failStaticId, DateTime SendTime)
	{
		NewTaskMagData newTaskMagData = NewTaskMagData.DataDict[TaskId];
		this.CyId = CyId;
		SendNpcId = NpcId;
		TaskType = taskType;
		this.TaskId = TaskId;
		FailLevel = newTaskMagData.ShiBaiLevel;
		FailTypeList = new List<int>(newTaskMagData.ShiBaiType);
		FailStaticId = failStaticId;
		if (!FailTypeList.Contains(3) && !FailTypeList.Contains(4))
		{
			return;
		}
		DateTime dateTime = SendTime.AddMonths(newTaskMagData.continueTime);
		if (DateTime.TryParse(newTaskMagData.EndTime, out var result))
		{
			if (dateTime <= result)
			{
				EndTime = dateTime;
			}
			else
			{
				EndTime = result;
			}
		}
		else
		{
			EndTime = dateTime;
		}
	}

	public bool IsFail(out int failType)
	{
		if (FailTypeList.Count < 1)
		{
			failType = 0;
			return false;
		}
		foreach (int failType2 in FailTypeList)
		{
			switch (failType2)
			{
			case 1:
				if (NpcJieSuanManager.inst.IsDeath(SendNpcId))
				{
					failType = failType2;
					return true;
				}
				break;
			case 2:
				if (!NpcJieSuanManager.inst.IsDeath(SendNpcId) && NpcJieSuanManager.inst.GetNpcData(SendNpcId)["Level"].I >= FailLevel)
				{
					failType = failType2;
					return true;
				}
				break;
			case 3:
			case 4:
				if (NpcJieSuanManager.inst.GetNowTime() > EndTime)
				{
					failType = failType2;
					return true;
				}
				break;
			}
		}
		failType = 0;
		return false;
	}
}
