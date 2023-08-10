using System;
using KBEngine;
using UnityEngine;

public class TaskUIManager : MonoBehaviour
{
	[SerializeField]
	private GameObject _TaskCell;

	private int CurTaskID = -1;

	private int curType = -1;

	private bool isOld;

	private bool isZhuiZhong;

	public static TaskUIManager inst;

	[SerializeField]
	private GameObject taskDesc;

	private bool CurisChuanWen;

	private JSONObject curTask;

	public void initTaskList(int type = 1)
	{
		inst = this;
		CurTaskID = -1;
		curTask = null;
		taskDesc.SetActive(false);
		clear();
		curType = type;
		switch (type)
		{
		case 0:
		case 1:
			initZhuXianAndChuanWen();
			break;
		case 2:
			if (!isOld)
			{
				initWeiTuo();
			}
			break;
		}
	}

	private void initZhuXianAndChuanWen()
	{
		foreach (JSONObject item in Tools.instance.getPlayer().taskMag._TaskData["Task"].list)
		{
			int i = jsonData.instance.TaskJsonData[item["id"].I.ToString()]["Type"].I;
			if (jsonData.instance.TaskJsonData.HasField(item["id"].I.ToString()))
			{
				bool flag = false;
				if (!checkIsGuoShi(item) && !isOld)
				{
					flag = true;
				}
				else if (isOld && checkIsGuoShi(item))
				{
					flag = true;
				}
				if (flag && i == curType)
				{
					TaskRenWuCell component = Tools.InstantiateGameObject(_TaskCell, _TaskCell.transform.parent).GetComponent<TaskRenWuCell>();
					int i2 = item["id"].I;
					component.setTaskName(Tools.Code64(jsonData.instance.TaskJsonData[i2.ToString()]["Name"].str));
					component.setTaskInfo(item);
				}
			}
		}
		if (curType != 0 || isOld)
		{
			return;
		}
		foreach (JSONObject item2 in Tools.instance.getPlayer().nomelTaskMag.GetNowNTask())
		{
			JSONObject jSONObject = jsonData.instance.NTaskAllType[item2["id"].I.ToString()];
			if (jSONObject.HasField("seid") && jSONObject["seid"].HasItem(2) && !CheckWeiTuoIsOut(item2))
			{
				TaskRenWuCell component2 = Tools.InstantiateGameObject(_TaskCell, _TaskCell.transform.parent).GetComponent<TaskRenWuCell>();
				component2.setTaskName(Tools.Code64(jSONObject["name"].str));
				component2.setWeTuoInfo(item2);
				component2.setIsChuanWen(flag: true);
			}
		}
	}

	private void initWeiTuo()
	{
		foreach (JSONObject item in Tools.instance.getPlayer().nomelTaskMag.GetNowNTask())
		{
			JSONObject jSONObject = jsonData.instance.NTaskAllType[item["id"].I.ToString()];
			if ((!jSONObject.HasField("seid") || (!jSONObject["seid"].HasItem(1) && !jSONObject["seid"].HasItem(2))) && !CheckWeiTuoIsOut(item))
			{
				TaskRenWuCell component = Tools.InstantiateGameObject(_TaskCell, _TaskCell.transform.parent).GetComponent<TaskRenWuCell>();
				component.setTaskName(Tools.Code64(jSONObject["name"].str));
				component.setWeTuoInfo(item);
			}
		}
	}

	private void clear()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Expected O, but got Unknown
		foreach (Transform item in _TaskCell.transform.parent)
		{
			Transform val = item;
			if (((Component)val).gameObject.activeSelf)
			{
				Object.Destroy((Object)(object)((Component)val).gameObject);
			}
		}
	}

	public void setOld(bool flag)
	{
		isOld = flag;
	}

	public void setCurTaskID(int id)
	{
		CurTaskID = id;
	}

	public bool getIsOld()
	{
		return isOld;
	}

	public int getCurTaskID()
	{
		return CurTaskID;
	}

	public int getCurType()
	{
		return curType;
	}

	private void OnDestroy()
	{
		inst = null;
	}

	public void ClickJiuShi(bool isOn)
	{
		taskDesc.SetActive(false);
		setOld(isOn);
		initTaskList(curType);
	}

	public void setCurSelectIsChuanWen(bool flag)
	{
		CurisChuanWen = flag;
	}

	public bool getCurSelectIsChuanWen()
	{
		return CurisChuanWen;
	}

	public void setCurZhuiZhong()
	{
		Avatar player = Tools.instance.getPlayer();
		if (player.TaskZhuiZhong.HasField("curTask") && !isOld && CurTaskID != -1)
		{
			if (player.TaskZhuiZhong["CurTaskID"].I == getCurTaskID() && player.TaskZhuiZhong["curType"].I == curType && player.TaskZhuiZhong["CurisChuanWen"].b == CurisChuanWen)
			{
				player.TaskZhuiZhong.SetField("CurTaskID", -1);
				return;
			}
			player.TaskZhuiZhong.SetField("CurTaskID", getCurTaskID());
			player.TaskZhuiZhong.SetField("curType", curType);
			player.TaskZhuiZhong.SetField("CurisChuanWen", CurisChuanWen);
			player.TaskZhuiZhong.SetField("curTask", curTask);
		}
		else
		{
			player.TaskZhuiZhong.SetField("CurTaskID", getCurTaskID());
			player.TaskZhuiZhong.SetField("curType", curType);
			player.TaskZhuiZhong.SetField("CurisChuanWen", CurisChuanWen);
			player.TaskZhuiZhong.SetField("curTask", curTask);
		}
	}

	public void setCurTask(JSONObject task)
	{
		curTask = task;
	}

	public static bool checkIsGuoShi(JSONObject task)
	{
		int i = task["id"].I;
		Avatar player = Tools.instance.getPlayer();
		if (jsonData.instance.TaskJsonData[i.ToString()]["Type"].I == 0)
		{
			if (task.HasField("isBlack") && task["isBlack"].b)
			{
				return true;
			}
			if (task.HasField("circulation") && task["circulation"].I > 0)
			{
				return false;
			}
			DateTime starTime = DateTime.Parse(jsonData.instance.TaskJsonData[i.ToString()]["StarTime"].str);
			DateTime endTime = DateTime.Parse(jsonData.instance.TaskJsonData[i.ToString()]["EndTime"].str);
			int circulation = (int)jsonData.instance.TaskJsonData[i.ToString()]["circulation"].n;
			DateTime nowTime = player.worldTimeMag.getNowTime();
			if (Tools.Code64(TaskCell.getTaskNextTime(circulation, nowTime, starTime, endTime)).Contains("传闻已过时"))
			{
				return true;
			}
			return false;
		}
		if (jsonData.instance.TaskJsonData[i.ToString()]["Type"].I == 1)
		{
			if (task.HasField("isComplete") && task["isComplete"].b)
			{
				return true;
			}
			if (task["disableTask"].b)
			{
				return true;
			}
			if (task.HasField("continueTime") && task["continueTime"].I > 0)
			{
				DateTime dateTime = DateTime.Parse(task["curTime"].str);
				if (dateTime.AddMonths(task["continueTime"].I) >= dateTime)
				{
					return false;
				}
				return true;
			}
			string s = "3000-01-01";
			if (jsonData.instance.TaskJsonData[i.ToString()]["EndTime"].str != "")
			{
				s = jsonData.instance.TaskJsonData[i.ToString()]["EndTime"].str;
			}
			DateTime dateTime2 = DateTime.Parse(s);
			DateTime nowTime2 = player.worldTimeMag.getNowTime();
			if (dateTime2 > nowTime2)
			{
				return false;
			}
			return true;
		}
		return false;
	}

	public static bool CheckWeiTuoIsOut(JSONObject weiTuo)
	{
		int i = weiTuo["id"].I;
		Avatar player = Tools.instance.getPlayer();
		string startTime = "0001-01-01";
		if (player.NomelTaskJson[i.ToString()].HasField("StartTime"))
		{
			startTime = player.NomelTaskJson[i.ToString()]["StartTime"].str;
		}
		DateTime endTime = Tools.GetEndTime(startTime, 0, player.nomelTaskMag.GetNTaskXiangXiData(i).shiXian);
		DateTime nowTime = player.worldTimeMag.getNowTime();
		if (endTime > nowTime)
		{
			return false;
		}
		return true;
	}
}
