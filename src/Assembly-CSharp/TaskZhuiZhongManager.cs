using System;
using JSONClass;
using KBEngine;
using UnityEngine;

public class TaskZhuiZhongManager : MonoBehaviour
{
	private bool isStart;

	private Avatar avatar;

	[SerializeField]
	private UILabel time;

	[SerializeField]
	private UILabel TextName;

	[SerializeField]
	private GameObject ZhuiZhongTask;

	private int taskID;

	private DateTime endTime;

	private DateTime StarTime;

	private int circulation;

	private JSONObject curTask;

	[SerializeField]
	private GameObject TimeTips;

	private void Start()
	{
		isStart = true;
		avatar = Tools.instance.getPlayer();
	}

	private void Update()
	{
		if (!isStart)
		{
			return;
		}
		if (avatar.TaskZhuiZhong.HasField("curTask") && avatar.TaskZhuiZhong["CurTaskID"].I != -1)
		{
			if (avatar.TaskZhuiZhong["curType"].I == 1)
			{
				curTask = avatar.TaskZhuiZhong["curTask"];
				if (TaskUIManager.checkIsGuoShi(curTask))
				{
					avatar.TaskZhuiZhong.SetField("CurTaskID", -1);
					UIPopTip.Inst.Pop("追踪任务已过期", PopTipIconType.任务进度);
					return;
				}
				TextName.text = Tools.Code64(jsonData.instance.TaskJsonData[curTask["id"].I.ToString()]["Name"].str);
				taskID = curTask["id"].I;
				time.text = TaskDescManager.getShengYuShiJi(curTask);
			}
			else if (avatar.TaskZhuiZhong["curType"].I == 0)
			{
				if (avatar.TaskZhuiZhong["CurisChuanWen"].b)
				{
					taskID = avatar.TaskZhuiZhong["CurTaskID"].I;
					curTask = avatar.TaskZhuiZhong["curTask"];
					if (!avatar.nomelTaskMag.HasNTask(taskID) || TaskUIManager.CheckWeiTuoIsOut(curTask))
					{
						avatar.TaskZhuiZhong.SetField("CurTaskID", -1);
						UIPopTip.Inst.Pop("追踪传闻已过期", PopTipIconType.任务进度);
						return;
					}
					NTaskXiangXi nTaskXiangXiData = avatar.nomelTaskMag.GetNTaskXiangXiData(taskID);
					TextName.text = nTaskXiangXiData.name;
					endTime = Tools.GetEndTime(avatar.NomelTaskJson[taskID.ToString()]["StartTime"].str, 0, nTaskXiangXiData.shiXian);
					try
					{
						time.text = Tools.TimeToShengYuTime(Tools.getShengYuShiJian(avatar.worldTimeMag.getNowTime(), endTime), "").ToCN();
					}
					catch (Exception)
					{
						avatar.TaskZhuiZhong.SetField("CurTaskID", -1);
						UIPopTip.Inst.Pop("追踪传闻已过期", PopTipIconType.任务进度);
						return;
					}
				}
				else
				{
					curTask = avatar.TaskZhuiZhong["curTask"];
					if (TaskUIManager.checkIsGuoShi(curTask))
					{
						avatar.TaskZhuiZhong.SetField("CurTaskID", -1);
						UIPopTip.Inst.Pop("追踪传闻已过期", PopTipIconType.任务进度);
						return;
					}
					taskID = curTask["id"].I;
					time.text = TaskDescManager.getShengYuShiJi(curTask);
					TextName.text = Tools.Code64(jsonData.instance.TaskJsonData[curTask["id"].n.ToString()]["Name"].str);
				}
			}
			else
			{
				taskID = avatar.TaskZhuiZhong["CurTaskID"].I;
				curTask = avatar.TaskZhuiZhong["curTask"];
				if (!avatar.nomelTaskMag.HasNTask(taskID) || TaskUIManager.CheckWeiTuoIsOut(curTask))
				{
					avatar.TaskZhuiZhong.SetField("CurTaskID", -1);
					UIPopTip.Inst.Pop("追踪委托已过期", PopTipIconType.任务进度);
					return;
				}
				NTaskXiangXi nTaskXiangXiData2 = avatar.nomelTaskMag.GetNTaskXiangXiData(taskID);
				endTime = Tools.GetEndTime(avatar.NomelTaskJson[taskID.ToString()]["StartTime"].str, 0, nTaskXiangXiData2.shiXian);
				time.text = Tools.TimeToShengYuTime(Tools.getShengYuShiJian(avatar.worldTimeMag.getNowTime(), endTime), "").ToCN();
				TextName.text = nTaskXiangXiData2.name;
			}
			if (!ZhuiZhongTask.activeSelf)
			{
				ZhuiZhongTask.SetActive(true);
			}
		}
		else if (ZhuiZhongTask.activeSelf)
		{
			ZhuiZhongTask.SetActive(false);
		}
	}
}
