using System;
using KBEngine;
using UnityEngine;

// Token: 0x02000513 RID: 1299
public class TaskUIManager : MonoBehaviour
{
	// Token: 0x06002178 RID: 8568 RVA: 0x00117698 File Offset: 0x00115898
	public void initTaskList(int type = 1)
	{
		TaskUIManager.inst = this;
		this.CurTaskID = -1;
		this.curTask = null;
		this.taskDesc.SetActive(false);
		this.clear();
		this.curType = type;
		if (type <= 1)
		{
			this.initZhuXianAndChuanWen();
			return;
		}
		if (type != 2)
		{
			return;
		}
		if (!this.isOld)
		{
			this.initWeiTuo();
		}
	}

	// Token: 0x06002179 RID: 8569 RVA: 0x001176F0 File Offset: 0x001158F0
	private void initZhuXianAndChuanWen()
	{
		foreach (JSONObject jsonobject in Tools.instance.getPlayer().taskMag._TaskData["Task"].list)
		{
			int num = (int)jsonData.instance.TaskJsonData[((int)jsonobject["id"].n).ToString()]["Type"].n;
			if (jsonData.instance.TaskJsonData.HasField(((int)jsonobject["id"].n).ToString()))
			{
				if (!TaskUIManager.checkIsGuoShi(jsonobject) && !this.isOld)
				{
					if (num == this.curType)
					{
						TaskRenWuCell component = Tools.InstantiateGameObject(this._TaskCell, this._TaskCell.transform.parent).GetComponent<TaskRenWuCell>();
						int num2 = (int)jsonobject["id"].n;
						component.setTaskName(Tools.Code64(jsonData.instance.TaskJsonData[num2.ToString()]["Name"].str));
						component.setTaskInfo(jsonobject);
					}
				}
				else if (this.isOld && TaskUIManager.checkIsGuoShi(jsonobject) && num == this.curType)
				{
					TaskRenWuCell component2 = Tools.InstantiateGameObject(this._TaskCell, this._TaskCell.transform.parent).GetComponent<TaskRenWuCell>();
					int num3 = (int)jsonobject["id"].n;
					component2.setTaskName(Tools.Code64(jsonData.instance.TaskJsonData[num3.ToString()]["Name"].str));
					component2.setTaskInfo(jsonobject);
				}
			}
		}
		if (this.curType == 0 && !this.isOld)
		{
			foreach (JSONObject jsonobject2 in Tools.instance.getPlayer().nomelTaskMag.GetNowNTask())
			{
				JSONObject jsonobject3 = jsonData.instance.NTaskAllType[jsonobject2["id"].I.ToString()];
				if (jsonobject3.HasField("seid") && jsonobject3["seid"].HasItem(2) && !TaskUIManager.CheckWeiTuoIsOut(jsonobject2))
				{
					TaskRenWuCell component3 = Tools.InstantiateGameObject(this._TaskCell, this._TaskCell.transform.parent).GetComponent<TaskRenWuCell>();
					component3.setTaskName(Tools.Code64(jsonobject3["name"].str));
					component3.setWeTuoInfo(jsonobject2);
					component3.setIsChuanWen(true);
				}
			}
		}
	}

	// Token: 0x0600217A RID: 8570 RVA: 0x001179F4 File Offset: 0x00115BF4
	private void initWeiTuo()
	{
		foreach (JSONObject jsonobject in Tools.instance.getPlayer().nomelTaskMag.GetNowNTask())
		{
			JSONObject jsonobject2 = jsonData.instance.NTaskAllType[jsonobject["id"].I.ToString()];
			if ((!jsonobject2.HasField("seid") || (!jsonobject2["seid"].HasItem(1) && !jsonobject2["seid"].HasItem(2))) && !TaskUIManager.CheckWeiTuoIsOut(jsonobject))
			{
				TaskRenWuCell component = Tools.InstantiateGameObject(this._TaskCell, this._TaskCell.transform.parent).GetComponent<TaskRenWuCell>();
				component.setTaskName(Tools.Code64(jsonobject2["name"].str));
				component.setWeTuoInfo(jsonobject);
			}
		}
	}

	// Token: 0x0600217B RID: 8571 RVA: 0x00117AF8 File Offset: 0x00115CF8
	private void clear()
	{
		foreach (object obj in this._TaskCell.transform.parent)
		{
			Transform transform = (Transform)obj;
			if (transform.gameObject.activeSelf)
			{
				Object.Destroy(transform.gameObject);
			}
		}
	}

	// Token: 0x0600217C RID: 8572 RVA: 0x0001B8BF File Offset: 0x00019ABF
	public void setOld(bool flag)
	{
		this.isOld = flag;
	}

	// Token: 0x0600217D RID: 8573 RVA: 0x0001B8C8 File Offset: 0x00019AC8
	public void setCurTaskID(int id)
	{
		this.CurTaskID = id;
	}

	// Token: 0x0600217E RID: 8574 RVA: 0x0001B8D1 File Offset: 0x00019AD1
	public bool getIsOld()
	{
		return this.isOld;
	}

	// Token: 0x0600217F RID: 8575 RVA: 0x0001B8D9 File Offset: 0x00019AD9
	public int getCurTaskID()
	{
		return this.CurTaskID;
	}

	// Token: 0x06002180 RID: 8576 RVA: 0x0001B8E1 File Offset: 0x00019AE1
	public int getCurType()
	{
		return this.curType;
	}

	// Token: 0x06002181 RID: 8577 RVA: 0x0001B8E9 File Offset: 0x00019AE9
	private void OnDestroy()
	{
		TaskUIManager.inst = null;
	}

	// Token: 0x06002182 RID: 8578 RVA: 0x0001B8F1 File Offset: 0x00019AF1
	public void ClickJiuShi(bool isOn)
	{
		this.taskDesc.SetActive(false);
		this.setOld(isOn);
		this.initTaskList(this.curType);
	}

	// Token: 0x06002183 RID: 8579 RVA: 0x0001B912 File Offset: 0x00019B12
	public void setCurSelectIsChuanWen(bool flag)
	{
		this.CurisChuanWen = flag;
	}

	// Token: 0x06002184 RID: 8580 RVA: 0x0001B91B File Offset: 0x00019B1B
	public bool getCurSelectIsChuanWen()
	{
		return this.CurisChuanWen;
	}

	// Token: 0x06002185 RID: 8581 RVA: 0x00117B6C File Offset: 0x00115D6C
	public void setCurZhuiZhong()
	{
		Avatar player = Tools.instance.getPlayer();
		if (!player.TaskZhuiZhong.HasField("curTask") || this.isOld || this.CurTaskID == -1)
		{
			player.TaskZhuiZhong.SetField("CurTaskID", this.getCurTaskID());
			player.TaskZhuiZhong.SetField("curType", this.curType);
			player.TaskZhuiZhong.SetField("CurisChuanWen", this.CurisChuanWen);
			player.TaskZhuiZhong.SetField("curTask", this.curTask);
			return;
		}
		if (player.TaskZhuiZhong["CurTaskID"].I == this.getCurTaskID() && player.TaskZhuiZhong["curType"].I == this.curType && player.TaskZhuiZhong["CurisChuanWen"].b == this.CurisChuanWen)
		{
			player.TaskZhuiZhong.SetField("CurTaskID", -1);
			return;
		}
		player.TaskZhuiZhong.SetField("CurTaskID", this.getCurTaskID());
		player.TaskZhuiZhong.SetField("curType", this.curType);
		player.TaskZhuiZhong.SetField("CurisChuanWen", this.CurisChuanWen);
		player.TaskZhuiZhong.SetField("curTask", this.curTask);
	}

	// Token: 0x06002186 RID: 8582 RVA: 0x0001B923 File Offset: 0x00019B23
	public void setCurTask(JSONObject task)
	{
		this.curTask = task;
	}

	// Token: 0x06002187 RID: 8583 RVA: 0x00117CCC File Offset: 0x00115ECC
	public static bool checkIsGuoShi(JSONObject task)
	{
		int num = (int)task["id"].n;
		Avatar player = Tools.instance.getPlayer();
		if (jsonData.instance.TaskJsonData[num.ToString()]["Type"].I == 0)
		{
			if (task.HasField("isBlack") && task["isBlack"].b)
			{
				return true;
			}
			if (task.HasField("circulation") && task["circulation"].I > 0)
			{
				return false;
			}
			DateTime starTime = DateTime.Parse(jsonData.instance.TaskJsonData[num.ToString()]["StarTime"].str);
			DateTime endTime = DateTime.Parse(jsonData.instance.TaskJsonData[num.ToString()]["EndTime"].str);
			int circulation = (int)jsonData.instance.TaskJsonData[num.ToString()]["circulation"].n;
			DateTime nowTime = player.worldTimeMag.getNowTime();
			return Tools.Code64(TaskCell.getTaskNextTime(circulation, nowTime, starTime, endTime)).Contains("传闻已过时");
		}
		else
		{
			if (jsonData.instance.TaskJsonData[num.ToString()]["Type"].I != 1)
			{
				return false;
			}
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
				DateTime t = DateTime.Parse(task["curTime"].str);
				return !(t.AddMonths(task["continueTime"].I) >= t);
			}
			string s = "3000-01-01";
			if (jsonData.instance.TaskJsonData[num.ToString()]["EndTime"].str != "")
			{
				s = jsonData.instance.TaskJsonData[num.ToString()]["EndTime"].str;
			}
			DateTime t2 = DateTime.Parse(s);
			DateTime nowTime2 = player.worldTimeMag.getNowTime();
			return !(t2 > nowTime2);
		}
	}

	// Token: 0x06002188 RID: 8584 RVA: 0x00117F48 File Offset: 0x00116148
	public static bool CheckWeiTuoIsOut(JSONObject weiTuo)
	{
		int taskID = (int)weiTuo["id"].n;
		Avatar player = Tools.instance.getPlayer();
		string startTime = "0001-01-01";
		if (player.NomelTaskJson[taskID.ToString()].HasField("StartTime"))
		{
			startTime = player.NomelTaskJson[taskID.ToString()]["StartTime"].str;
		}
		DateTime endTime = Tools.GetEndTime(startTime, 0, player.nomelTaskMag.GetNTaskXiangXiData(taskID).shiXian, 0);
		DateTime nowTime = player.worldTimeMag.getNowTime();
		return !(endTime > nowTime);
	}

	// Token: 0x04001D00 RID: 7424
	[SerializeField]
	private GameObject _TaskCell;

	// Token: 0x04001D01 RID: 7425
	private int CurTaskID = -1;

	// Token: 0x04001D02 RID: 7426
	private int curType = -1;

	// Token: 0x04001D03 RID: 7427
	private bool isOld;

	// Token: 0x04001D04 RID: 7428
	private bool isZhuiZhong;

	// Token: 0x04001D05 RID: 7429
	public static TaskUIManager inst;

	// Token: 0x04001D06 RID: 7430
	[SerializeField]
	private GameObject taskDesc;

	// Token: 0x04001D07 RID: 7431
	private bool CurisChuanWen;

	// Token: 0x04001D08 RID: 7432
	private JSONObject curTask;
}
