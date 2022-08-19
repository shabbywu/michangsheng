using System;
using KBEngine;
using UnityEngine;

// Token: 0x0200038E RID: 910
public class TaskUIManager : MonoBehaviour
{
	// Token: 0x06001DFD RID: 7677 RVA: 0x000D3A64 File Offset: 0x000D1C64
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

	// Token: 0x06001DFE RID: 7678 RVA: 0x000D3ABC File Offset: 0x000D1CBC
	private void initZhuXianAndChuanWen()
	{
		foreach (JSONObject jsonobject in Tools.instance.getPlayer().taskMag._TaskData["Task"].list)
		{
			int i = jsonData.instance.TaskJsonData[jsonobject["id"].I.ToString()]["Type"].I;
			if (jsonData.instance.TaskJsonData.HasField(jsonobject["id"].I.ToString()))
			{
				bool flag = false;
				if (!TaskUIManager.checkIsGuoShi(jsonobject) && !this.isOld)
				{
					flag = true;
				}
				else if (this.isOld && TaskUIManager.checkIsGuoShi(jsonobject))
				{
					flag = true;
				}
				if (flag && i == this.curType)
				{
					TaskRenWuCell component = Tools.InstantiateGameObject(this._TaskCell, this._TaskCell.transform.parent).GetComponent<TaskRenWuCell>();
					int i2 = jsonobject["id"].I;
					component.setTaskName(Tools.Code64(jsonData.instance.TaskJsonData[i2.ToString()]["Name"].str));
					component.setTaskInfo(jsonobject);
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
					TaskRenWuCell component2 = Tools.InstantiateGameObject(this._TaskCell, this._TaskCell.transform.parent).GetComponent<TaskRenWuCell>();
					component2.setTaskName(Tools.Code64(jsonobject3["name"].str));
					component2.setWeTuoInfo(jsonobject2);
					component2.setIsChuanWen(true);
				}
			}
		}
	}

	// Token: 0x06001DFF RID: 7679 RVA: 0x000D3D4C File Offset: 0x000D1F4C
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

	// Token: 0x06001E00 RID: 7680 RVA: 0x000D3E50 File Offset: 0x000D2050
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

	// Token: 0x06001E01 RID: 7681 RVA: 0x000D3EC4 File Offset: 0x000D20C4
	public void setOld(bool flag)
	{
		this.isOld = flag;
	}

	// Token: 0x06001E02 RID: 7682 RVA: 0x000D3ECD File Offset: 0x000D20CD
	public void setCurTaskID(int id)
	{
		this.CurTaskID = id;
	}

	// Token: 0x06001E03 RID: 7683 RVA: 0x000D3ED6 File Offset: 0x000D20D6
	public bool getIsOld()
	{
		return this.isOld;
	}

	// Token: 0x06001E04 RID: 7684 RVA: 0x000D3EDE File Offset: 0x000D20DE
	public int getCurTaskID()
	{
		return this.CurTaskID;
	}

	// Token: 0x06001E05 RID: 7685 RVA: 0x000D3EE6 File Offset: 0x000D20E6
	public int getCurType()
	{
		return this.curType;
	}

	// Token: 0x06001E06 RID: 7686 RVA: 0x000D3EEE File Offset: 0x000D20EE
	private void OnDestroy()
	{
		TaskUIManager.inst = null;
	}

	// Token: 0x06001E07 RID: 7687 RVA: 0x000D3EF6 File Offset: 0x000D20F6
	public void ClickJiuShi(bool isOn)
	{
		this.taskDesc.SetActive(false);
		this.setOld(isOn);
		this.initTaskList(this.curType);
	}

	// Token: 0x06001E08 RID: 7688 RVA: 0x000D3F17 File Offset: 0x000D2117
	public void setCurSelectIsChuanWen(bool flag)
	{
		this.CurisChuanWen = flag;
	}

	// Token: 0x06001E09 RID: 7689 RVA: 0x000D3F20 File Offset: 0x000D2120
	public bool getCurSelectIsChuanWen()
	{
		return this.CurisChuanWen;
	}

	// Token: 0x06001E0A RID: 7690 RVA: 0x000D3F28 File Offset: 0x000D2128
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

	// Token: 0x06001E0B RID: 7691 RVA: 0x000D4086 File Offset: 0x000D2286
	public void setCurTask(JSONObject task)
	{
		this.curTask = task;
	}

	// Token: 0x06001E0C RID: 7692 RVA: 0x000D4090 File Offset: 0x000D2290
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
			return Tools.Code64(TaskCell.getTaskNextTime(circulation, nowTime, starTime, endTime)).Contains("传闻已过时");
		}
		else
		{
			if (jsonData.instance.TaskJsonData[i.ToString()]["Type"].I != 1)
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
			if (jsonData.instance.TaskJsonData[i.ToString()]["EndTime"].str != "")
			{
				s = jsonData.instance.TaskJsonData[i.ToString()]["EndTime"].str;
			}
			DateTime t2 = DateTime.Parse(s);
			DateTime nowTime2 = player.worldTimeMag.getNowTime();
			return !(t2 > nowTime2);
		}
	}

	// Token: 0x06001E0D RID: 7693 RVA: 0x000D430C File Offset: 0x000D250C
	public static bool CheckWeiTuoIsOut(JSONObject weiTuo)
	{
		int i = weiTuo["id"].I;
		Avatar player = Tools.instance.getPlayer();
		string startTime = "0001-01-01";
		if (player.NomelTaskJson[i.ToString()].HasField("StartTime"))
		{
			startTime = player.NomelTaskJson[i.ToString()]["StartTime"].str;
		}
		DateTime endTime = Tools.GetEndTime(startTime, 0, player.nomelTaskMag.GetNTaskXiangXiData(i).shiXian, 0);
		DateTime nowTime = player.worldTimeMag.getNowTime();
		return !(endTime > nowTime);
	}

	// Token: 0x040018A0 RID: 6304
	[SerializeField]
	private GameObject _TaskCell;

	// Token: 0x040018A1 RID: 6305
	private int CurTaskID = -1;

	// Token: 0x040018A2 RID: 6306
	private int curType = -1;

	// Token: 0x040018A3 RID: 6307
	private bool isOld;

	// Token: 0x040018A4 RID: 6308
	private bool isZhuiZhong;

	// Token: 0x040018A5 RID: 6309
	public static TaskUIManager inst;

	// Token: 0x040018A6 RID: 6310
	[SerializeField]
	private GameObject taskDesc;

	// Token: 0x040018A7 RID: 6311
	private bool CurisChuanWen;

	// Token: 0x040018A8 RID: 6312
	private JSONObject curTask;
}
