using System;
using System.Collections.Generic;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TaskDescManager : MonoBehaviour
{
	[SerializeField]
	private GameObject TaskIndex;

	[SerializeField]
	private Text HasTime;

	[SerializeField]
	private Text Desc;

	[SerializeField]
	private Text Name;

	[SerializeField]
	private GameObject FinshImage;

	[SerializeField]
	private GameObject ZhuiZhongImage;

	[SerializeField]
	private GameObject NoZhuiZhongImage;

	[SerializeField]
	private Text TaskStatus;

	[SerializeField]
	private Text TaskJinDu;

	[SerializeField]
	private Text TaskDes;

	[SerializeField]
	private Text chunWenStatus;

	public GameObject HasOpen;

	public GameObject NoOpen;

	[SerializeField]
	private GameObject ChuanWenShengYuShiJian;

	[SerializeField]
	private Text ChuanWenShengYuTime;

	[SerializeField]
	private GameObject NomalTaskRenWuJinDu;

	public FpBtn MapBtn;

	private void Awake()
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Expected O, but got Unknown
		((Graphic)((Component)MapBtn).GetComponent<Image>()).raycastTarget = true;
		Image[] componentsInChildren = ((Component)MapBtn).GetComponentsInChildren<Image>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			((Graphic)componentsInChildren[i]).raycastTarget = true;
		}
		MapBtn.mouseUpEvent.AddListener(new UnityAction(MapBtnEvent));
	}

	public void MapBtnEvent()
	{
		int id = int.Parse(((Object)MapBtn).name);
		UIMapPanel.Inst.OpenHighlight(id);
	}

	public void setCurTaskDesc(JSONObject task)
	{
		clear();
		((Component)this).gameObject.SetActive(true);
		setCurTime(task);
		setDesc(jsonData.instance.TaskJsonData[task["id"].n.ToString()]["Desc"].str);
		setDescTaskName(jsonData.instance.TaskJsonData[task["id"].n.ToString()]["Name"].str);
		setTitle();
		setIsShowFinish(task);
		List<int> indexList = new List<int>();
		for (int i = 0; i < task["AllIndex"].Count; i++)
		{
			indexList.Add(task["AllIndex"][i].I);
		}
		List<JSONObject> list = jsonData.instance.TaskInfoJsonData.list.FindAll((JSONObject aa) => aa["TaskID"].I == task["id"].I && indexList.Contains(aa["TaskIndex"].I));
		if (task["disableTask"].b)
		{
			for (int j = 0; j < task["AllIndex"].Count; j++)
			{
				Tools.InstantiateGameObject(TaskIndex, TaskIndex.transform.parent).GetComponent<TaskIndexCell>().setContent(list[j]["Desc"].str, isFinsh: true);
			}
			return;
		}
		for (int k = 0; k < task["AllIndex"].Count; k++)
		{
			TaskIndexCell component = Tools.InstantiateGameObject(TaskIndex, TaskIndex.transform.parent).GetComponent<TaskIndexCell>();
			if (k == task["AllIndex"].Count - 1)
			{
				if (list[k]["mapIndex"].I > 0)
				{
					((Component)MapBtn).gameObject.SetActive(true);
					((Object)((Component)MapBtn).gameObject).name = list[k]["mapIndex"].I.ToString();
				}
				component.setContent(list[k]["Desc"].str);
			}
			else
			{
				component.setContent(list[k]["Desc"].str, isFinsh: true);
			}
		}
	}

	private void setCurTime(JSONObject task)
	{
		int i = task["id"].I;
		Avatar player = Tools.instance.getPlayer();
		string text = "";
		if (jsonData.instance.TaskJsonData[i.ToString()]["Type"].I == 1)
		{
			if (TaskUIManager.checkIsGuoShi(task))
			{
				HasTime.text = "   已过时";
				FinshImage.SetActive(true);
				return;
			}
			if (task.HasField("continueTime") && task["continueTime"].I > 0)
			{
				DateTime endTime = DateTime.Parse(task["curTime"].str).AddMonths(task["continueTime"].I);
				text = Tools.Code64(Tools.TimeToShengYuTime(Tools.getShengYuShiJian(player.worldTimeMag.getNowTime(), endTime), ""));
			}
			else
			{
				DateTime endTime2 = DateTime.Parse(jsonData.instance.TaskJsonData[i.ToString()]["EndTime"].str);
				text = Tools.Code64(Tools.TimeToShengYuTime(Tools.getShengYuShiJian(player.worldTimeMag.getNowTime(), endTime2)));
			}
			if (text.Contains("剩余时间"))
			{
				text = text.Replace("剩余时间", "");
			}
			HasTime.text = text;
			return;
		}
		DateTime starTime = DateTime.Parse(jsonData.instance.TaskJsonData[i.ToString()]["StarTime"].str);
		DateTime endTime3 = DateTime.Parse(jsonData.instance.TaskJsonData[i.ToString()]["EndTime"].str);
		int circulation = (int)jsonData.instance.TaskJsonData[i.ToString()]["circulation"].n;
		DateTime nowTime = player.worldTimeMag.getNowTime();
		text = Tools.Code64(TaskCell.getTaskNextTime(circulation, nowTime, starTime, endTime3));
		if (TaskUIManager.checkIsGuoShi(task))
		{
			ChuanWenShengYuTime.text = "已过时";
			chunWenStatus.text = "事件状态:";
		}
		else if (text.Contains("距离事件开始还有"))
		{
			ChuanWenShengYuTime.text = text.Replace("距离事件开始还有", "");
			NoOpen.SetActive(true);
			HasOpen.SetActive(false);
		}
		else if (text.Contains("事件已开启"))
		{
			ChuanWenShengYuTime.text = "已开启";
			NoOpen.SetActive(false);
			HasOpen.SetActive(true);
		}
	}

	private void setDesc(string str)
	{
		Desc.text = str.STVarReplace().ToCN();
	}

	private void setDescTaskName(string str)
	{
		Name.text = Tools.Code64(str);
	}

	private void setTaskNameColor(Color color)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		((Graphic)Name).color = color;
	}

	private void clear()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Expected O, but got Unknown
		foreach (Transform item in TaskIndex.transform.parent)
		{
			Transform val = item;
			if (((Component)val).gameObject.activeSelf)
			{
				Object.Destroy((Object)(object)((Component)val).gameObject);
			}
		}
		((Component)MapBtn).gameObject.SetActive(false);
		NomalTaskRenWuJinDu.SetActive(true);
		ChuanWenShengYuShiJian.SetActive(false);
	}

	public void clearCurTaskDesc()
	{
		clear();
		((Component)this).gameObject.SetActive(false);
	}

	public void setChuanWenWenTuo(JSONObject weiTuo)
	{
		clear();
		((Component)this).gameObject.SetActive(true);
		ChuanWenShengYuShiJian.SetActive(true);
		NomalTaskRenWuJinDu.SetActive(false);
		setTitle("事件状态:", "传闻时间", "传闻描述");
		int i = weiTuo["id"].I;
		Avatar player = Tools.instance.getPlayer();
		NTaskXiangXi nTaskXiangXiData = player.nomelTaskMag.GetNTaskXiangXiData(i);
		string text = nTaskXiangXiData.zongmiaoshu;
		if (text.Contains("{where=99}"))
		{
			int i2 = player.NomelTaskJson[i.ToString()]["TaskWhereChild"][0].I;
			text = text.Replace("{where=99}", NTaskSuiJI.DataDict[i2].name);
			((Component)MapBtn).gameObject.SetActive(true);
			((Object)((Component)MapBtn).gameObject).name = GetMapIndex(i2).ToString();
			text = text.Replace("‘", "");
			text = text.Replace("\"", "");
		}
		ChuanWenShengYuTime.text = getWeiTuoShengYuShiJian(weiTuo);
		HasTime.text = "   已开启";
		setDescTaskName(nTaskXiangXiData.name);
		setDesc((!text.Contains("{ZongMiaoShu}")) ? text : text.Replace("{ZongMiaoShu}", NTaskAllType.DataDict[i].ZongMiaoShu));
	}

	public int GetMapIndex(int id)
	{
		int num = 0;
		string strValue = NTaskSuiJI.DataDict[id].StrValue;
		foreach (MapIndexData data in MapIndexData.DataList)
		{
			if (data.StrValue == strValue)
			{
				num = data.mapIndex;
				break;
			}
		}
		if (num == 0)
		{
			Debug.LogError((object)("GetMapIndex" + strValue + ",不存在地图点"));
		}
		return num;
	}

	public void setWeiTuoDesc(JSONObject weiTuo)
	{
		clear();
		((Component)this).gameObject.SetActive(true);
		int i = weiTuo["id"].I;
		Avatar player = Tools.instance.getPlayer();
		NTaskXiangXi nTaskXiangXiData = player.nomelTaskMag.GetNTaskXiangXiData(i);
		string zongmiaoshu = nTaskXiangXiData.zongmiaoshu;
		DateTime endTime = Tools.GetEndTime(player.NomelTaskJson[i.ToString()]["StartTime"].str, 0, nTaskXiangXiData.shiXian);
		HasTime.text = Tools.TimeToShengYuTime(Tools.getShengYuShiJian(player.worldTimeMag.getNowTime(), endTime), "").ToCN();
		setDescTaskName(nTaskXiangXiData.name);
		setDesc((!zongmiaoshu.Contains("{ZongMiaoShu}")) ? zongmiaoshu : zongmiaoshu.Replace("{ZongMiaoShu}", NTaskAllType.DataDict[i].ZongMiaoShu));
		int num = 0;
		foreach (JSONObject nTaskXiangXi in player.nomelTaskMag.GetNTaskXiangXiList(i))
		{
			int i2 = player.NomelTaskJson[i.ToString()]["TaskChild"][num].I;
			NTaskSuiJI nTaskSuiJI = NTaskSuiJI.DataDict[i2];
			string text = nTaskXiangXi["desc"].str.Replace(nTaskXiangXi["TaskID"].str, nTaskSuiJI.name);
			if (nTaskXiangXi["Place"].str != "0" && text.Contains(nTaskXiangXi["Place"].str))
			{
				int whereChilidID = player.nomelTaskMag.getWhereChilidID(i, num);
				text = text.Replace(nTaskXiangXi["Place"].str, NTaskSuiJI.DataDict[whereChilidID].name);
				((Component)MapBtn).gameObject.SetActive(true);
				((Object)((Component)MapBtn).gameObject).name = GetMapIndex(whereChilidID).ToString();
			}
			string str = text.ToCN();
			Tools.InstantiateGameObject(TaskIndex, TaskIndex.transform.parent).GetComponent<TaskIndexCell>().setContent(str, player.nomelTaskMag.XiangXiTaskIsEnd(nTaskXiangXi, i, num));
			num++;
		}
	}

	public void checkIsZhuiZhong()
	{
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		Avatar player = Tools.instance.getPlayer();
		if (player.TaskZhuiZhong.HasField("curTask") && player.TaskZhuiZhong["CurTaskID"].I == TaskUIManager.inst.getCurTaskID() && player.TaskZhuiZhong["curType"].I == TaskUIManager.inst.getCurType())
		{
			setTaskNameColor(new Color(0.88235295f, 0.9843137f, 0.72156864f));
			ZhuiZhongImage.SetActive(true);
			NoZhuiZhongImage.SetActive(false);
		}
		else
		{
			setTaskNameColor(new Color(41f / 85f, 16f / 51f, 0.18431373f));
			ZhuiZhongImage.SetActive(false);
			NoZhuiZhongImage.SetActive(true);
		}
	}

	private void setTitle(string status = "剩余时间:", string jindu = "任务进度", string des = "任务描述")
	{
		TaskStatus.text = status;
		TaskJinDu.text = jindu;
		TaskDes.text = des;
	}

	public void setChuanMiaoShu(JSONObject task)
	{
		clear();
		((Component)this).gameObject.SetActive(true);
		ChuanWenShengYuShiJian.SetActive(true);
		NomalTaskRenWuJinDu.SetActive(false);
		setCurTime(task);
		TaskJsonData taskJsonData = TaskJsonData.DataDict[task["id"].I];
		if (taskJsonData.mapIndex > 0)
		{
			((Component)MapBtn).gameObject.SetActive(true);
			((Object)((Component)MapBtn).gameObject).name = taskJsonData.mapIndex.ToString();
		}
		setDesc(taskJsonData.Desc);
		setDescTaskName(taskJsonData.Name);
		setTitle("事件状态:", "传闻时间", "传闻描述");
		setIsShowFinish(task);
	}

	public static string getShengYuShiJi(JSONObject task)
	{
		string text = "";
		int i = task["id"].I;
		Avatar player = Tools.instance.getPlayer();
		if (jsonData.instance.TaskJsonData[i.ToString()]["Type"].I == 1)
		{
			if (TaskUIManager.checkIsGuoShi(task))
			{
				return "   已过时";
			}
			if (task.HasField("continueTime") && task["continueTime"].I > 0)
			{
				DateTime endTime = DateTime.Parse(task["curTime"].str).AddMonths(task["continueTime"].I);
				text = Tools.Code64(Tools.TimeToShengYuTime(Tools.getShengYuShiJian(player.worldTimeMag.getNowTime(), endTime), ""));
			}
			else
			{
				DateTime endTime2 = DateTime.Parse(jsonData.instance.TaskJsonData[i.ToString()]["EndTime"].str);
				text = Tools.TimeToShengYuTime(Tools.getShengYuShiJian(player.worldTimeMag.getNowTime(), endTime2)).ToCN();
			}
			if (text.Contains("剩余时间"))
			{
				text = text.Replace("剩余时间", "");
			}
		}
		else
		{
			DateTime starTime = DateTime.Parse(jsonData.instance.TaskJsonData[i.ToString()]["StarTime"].str);
			DateTime endTime3 = DateTime.Parse(jsonData.instance.TaskJsonData[i.ToString()]["EndTime"].str);
			int circulation = (int)jsonData.instance.TaskJsonData[i.ToString()]["circulation"].n;
			DateTime nowTime = player.worldTimeMag.getNowTime();
			text = TaskCell.getTaskNextTime(circulation, nowTime, starTime, endTime3).ToCN();
			if (text.Contains("距离事件开始还有"))
			{
				text = text.Replace("距离事件开始还有", "");
			}
			else if (text.Contains("事件已开启"))
			{
				text = "   已开启";
			}
		}
		return text;
	}

	public static string getWeiTuoShengYuShiJian(JSONObject weiTuo)
	{
		int i = weiTuo["id"].I;
		Avatar player = Tools.instance.getPlayer();
		if (!player.NomelTaskJson[i.ToString()].HasField("StartTime"))
		{
			player.NomelTaskJson[i.ToString()].SetField("StartTime", player.worldTimeMag.nowTime);
			player.nomelTaskMag.RefreshGetNowNTaskData();
		}
		DateTime endTime = Tools.GetEndTime(player.NomelTaskJson[i.ToString()]["StartTime"].str, 0, player.nomelTaskMag.GetNTaskXiangXiData(i).shiXian);
		player.worldTimeMag.getNowTime();
		return Tools.Code64(Tools.TimeToShengYuTime(Tools.getShengYuShiJian(player.worldTimeMag.getNowTime(), endTime), ""));
	}

	private void setIsShowFinish(JSONObject task)
	{
		if (TaskUIManager.checkIsGuoShi(task) && TaskUIManager.inst.getIsOld())
		{
			FinshImage.SetActive(true);
		}
		else
		{
			FinshImage.SetActive(false);
		}
	}
}
