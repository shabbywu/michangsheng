using System;
using System.Collections.Generic;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

public class TaskCell : MonoBehaviour
{
	public GameObject DescBG;

	public int taskID;

	private GameObject hongdian;

	public int TaskType;

	private void Start()
	{
		hongdian = ((Component)((Component)this).transform.Find("hongdian")).gameObject;
		setHongDian();
		disableSelf();
	}

	public static string getTaskNextTime(int circulation, DateTime now, DateTime StarTime, DateTime EndTime)
	{
		string result = "";
		if (circulation == 0 || now < StarTime)
		{
			if (now >= StarTime && now < EndTime)
			{
				result = Tools.getStr("chuanwen5");
			}
			else if (now >= EndTime)
			{
				result = Tools.getStr("chuanwen6");
			}
			else if (now < StarTime)
			{
				TimeSpan timeSpan = StarTime - now;
				DateTime dateTime = new DateTime(1, 1, 1).AddDays(timeSpan.Days);
				result = Tools.getStr("chuanwen7").Replace("{X}", dateTime.Year - 1 + "年" + (dateTime.Month - 1) + "月" + (dateTime.Day - 1) + "日");
			}
		}
		else
		{
			if (now > StarTime)
			{
				now = new DateTime(now.Year - circulation * ((now.Year - StarTime.Year) / circulation), now.Month, (now.Month == 2 && now.Day == 29) ? 28 : now.Day);
			}
			DateTime dateTime2 = new DateTime(StarTime.Year + circulation, StarTime.Month, StarTime.Day);
			if (StarTime <= now && now <= EndTime)
			{
				result = Tools.getStr("chuanwen5");
			}
			else
			{
				TimeSpan timeSpan2 = dateTime2 - now;
				if (StarTime > now && StarTime.Year == now.Year)
				{
					timeSpan2 = StarTime - now;
				}
				DateTime dateTime3 = new DateTime(1, 1, 1).AddDays(timeSpan2.Days);
				result = Tools.getStr("chuanwen7").Replace("{X}", dateTime3.Year - 1 + "年" + (dateTime3.Month - 1) + "月" + (dateTime3.Day - 1) + "日");
			}
		}
		return result;
	}

	public void openView()
	{
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Expected O, but got Unknown
		//IL_03f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_066d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0679: Unknown result type (might be due to invalid IL or missing references)
		//IL_04de: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_081b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0826: Unknown result type (might be due to invalid IL or missing references)
		UILabel component = ((Component)DescBG.transform.Find("title")).GetComponent<UILabel>();
		UILabel component2 = ((Component)DescBG.transform.Find("title1")).GetComponent<UILabel>();
		UILabel component3 = ((Component)DescBG.transform.Find("title2")).GetComponent<UILabel>();
		UILabel component4 = ((Component)DescBG.transform.Find("message")).GetComponent<UILabel>();
		UIButton component5 = ((Component)DescBG.transform.Find("biaoji")).GetComponent<UIButton>();
		UIToggle component6 = ((Component)DescBG.transform.Find("Toggle")).GetComponent<UIToggle>();
		UILabel component7 = ((Component)DescBG.transform.Find("shengyutime")).GetComponent<UILabel>();
		Transform val = DescBG.transform.Find("Canvas/Scroll View/Viewport/Content");
		Transform val2 = DescBG.transform.Find("chuanwen");
		Transform child = ((Component)val).transform.GetChild(0);
		Avatar player = Tools.instance.getPlayer();
		NTaskAllType nTaskAllType = NTaskAllType.DataDict[taskID];
		NTaskXiangXi nTaskXiangXiData = player.nomelTaskMag.GetNTaskXiangXiData(taskID);
		if (!jsonData.instance.TaskJsonData.HasField(taskID.ToString()) && TaskType != 2)
		{
			return;
		}
		((Component)component7).gameObject.SetActive(false);
		foreach (Transform item in val)
		{
			Transform val3 = item;
			if (((Component)val3).gameObject.activeSelf)
			{
				Object.Destroy((Object)(object)((Component)val3).gameObject);
			}
		}
		if (TaskType == 1 || TaskType == 0)
		{
			component.text = jsonData.instance.TaskJsonData[taskID.ToString()]["Name"].Str;
			string str = jsonData.instance.TaskJsonData[taskID.ToString()]["Desc"].Str;
			component4.text = str.STVarReplace();
		}
		if (TaskType == 0)
		{
			((Component)val).gameObject.SetActive(false);
			((Component)val2).gameObject.SetActive(true);
			component2.text = Tools.getStr("chuanwen1");
			component3.text = Tools.getStr("chuanwen2");
			DateTime starTime = DateTime.Parse(jsonData.instance.TaskJsonData[taskID.ToString()]["StarTime"].str);
			DateTime endTime = DateTime.Parse(jsonData.instance.TaskJsonData[taskID.ToString()]["EndTime"].str);
			int circulation = (int)jsonData.instance.TaskJsonData[taskID.ToString()]["circulation"].n;
			DateTime nowTime = Tools.instance.getPlayer().worldTimeMag.getNowTime();
			string taskNextTime = getTaskNextTime(circulation, nowTime, starTime, endTime);
			((Component)((Component)val2).transform.Find("Label")).GetComponent<UILabel>().text = taskNextTime;
		}
		else if (TaskType == 1)
		{
			((Component)val).gameObject.SetActive(true);
			((Component)val2).gameObject.SetActive(false);
			component2.text = Tools.getStr("chuanwen3");
			component3.text = Tools.getStr("chuanwen4");
			foreach (JSONObject item2 in player.taskMag._TaskData["Task"][taskID.ToString()]["AllIndex"].list)
			{
				Transform val4 = Object.Instantiate<Transform>(child);
				((Component)val4).gameObject.SetActive(true);
				val4.SetParent(val);
				val4.localScale = Vector3.one;
				val4.localPosition = Vector3.zero;
				string text = Tools.instance.Code64ToString(getTaskInfoDesc(taskID, (int)item2.n));
				((Component)val4).GetComponent<Text>().text = text;
				if (!player.taskMag._TaskData["Task"][taskID.ToString()].HasField("finishIndex"))
				{
					continue;
				}
				foreach (JSONObject item3 in player.taskMag._TaskData["Task"][taskID.ToString()]["finishIndex"].list)
				{
					if ((int)item3.n == (int)item2.n)
					{
						((Component)val4.Find("Toggle")).GetComponent<Toggle>().isOn = true;
						((Graphic)((Component)val4).GetComponent<Text>()).color = Color.gray;
					}
				}
			}
		}
		else if (TaskType == 2)
		{
			((Component)component7).gameObject.SetActive(true);
			((Component)val).gameObject.SetActive(true);
			((Component)val2).gameObject.SetActive(false);
			component.text = nTaskXiangXiData.name;
			string zongmiaoshu = nTaskXiangXiData.zongmiaoshu;
			component4.text = ((!zongmiaoshu.Contains("{ZongMiaoShu}")) ? zongmiaoshu : zongmiaoshu.Replace("{ZongMiaoShu}", nTaskAllType.ZongMiaoShu));
			if (nTaskAllType.seid.Contains(2))
			{
				component2.text = Tools.getStr("chuanwen1");
				component3.text = Tools.getStr("chuanwen2");
				((Component)val2).gameObject.SetActive(true);
				((Component)((Component)val2).transform.Find("Label")).GetComponent<UILabel>().text = "事件已开启";
				((Component)component7).gameObject.SetActive(false);
			}
			else
			{
				component2.text = Tools.getStr("chuanwen3");
				component3.text = Tools.getStr("chuanwen4");
				int num = 0;
				foreach (JSONObject nTaskXiangXi in player.nomelTaskMag.GetNTaskXiangXiList(taskID))
				{
					Transform val5 = Object.Instantiate<Transform>(child);
					((Component)val5).gameObject.SetActive(true);
					val5.SetParent(val);
					val5.localScale = Vector3.one;
					val5.localPosition = Vector3.zero;
					int i = player.NomelTaskJson[taskID.ToString()]["TaskChild"][num].I;
					string text2 = nTaskXiangXi["desc"].str.Replace(nTaskXiangXi["TaskID"].str, NTaskSuiJI.DataDict[i].name);
					if (nTaskXiangXi["Place"].str != "0" && text2.Contains(nTaskXiangXi["Place"].str))
					{
						int whereChilidID = player.nomelTaskMag.getWhereChilidID(taskID, num);
						text2 = text2.Replace(nTaskXiangXi["Place"].str, NTaskSuiJI.DataDict[whereChilidID].name);
					}
					string text3 = text2.ToCN();
					((Component)val5).GetComponent<Text>().text = text3;
					if (player.nomelTaskMag.XiangXiTaskIsEnd(nTaskXiangXi, taskID, num))
					{
						((Component)val5.Find("Toggle")).GetComponent<Toggle>().isOn = true;
						((Graphic)((Component)val5).GetComponent<Text>()).color = Color.gray;
					}
					num++;
				}
				if (nTaskXiangXiData.JiaoFuType == 1 && player.nomelTaskMag.nowChildNTask(taskID) == -1)
				{
					Transform obj = Object.Instantiate<Transform>(child);
					((Component)obj).gameObject.SetActive(true);
					obj.SetParent(val);
					obj.localScale = Vector3.one;
					obj.localPosition = Vector3.zero;
					((Component)obj).GetComponent<Text>().text = "回" + nTaskAllType.jiaofudidian + "，向" + nTaskAllType.jiaofurenwu + "交付任务";
				}
				DateTime endTime2 = Tools.GetEndTime(player.NomelTaskJson[taskID.ToString()]["StartTime"].str, 0, nTaskXiangXiData.shiXian);
				component7.text = Tools.TimeToShengYuTime(Tools.getShengYuShiJian(player.worldTimeMag.getNowTime(), endTime2));
			}
			((Component)component5).gameObject.SetActive(false);
			((Component)component6).gameObject.SetActive(false);
		}
		if (TaskType == 1 || TaskType == 0)
		{
			component5.onClick.Clear();
			component5.onClick.Add(new EventDelegate(setAvatarGuide));
			showToggle();
			showBiaoJi();
			component6.onChange.Clear();
			component6.value = player.taskMag._TaskData["Task"][taskID.ToString()]["disableTask"].b;
			component6.onChange.Add(new EventDelegate(ChangedisableTask));
		}
	}

	public void AddZiXiang(JSONObject task, Transform tempText, Transform textGrid, Avatar avatar, int index1)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		Transform val = Object.Instantiate<Transform>(tempText);
		((Component)val).gameObject.SetActive(true);
		val.SetParent(textGrid);
		val.localScale = Vector3.one;
		val.localPosition = Vector3.zero;
		int i = avatar.NomelTaskJson[taskID.ToString()]["TaskChild"][index1].I;
		string text = task["desc"].str.Replace(task["TaskID"].str, Tools.Code64(jsonData.instance.NTaskSuiJI[i.ToString()]["name"].str));
		if (task["Place"].str != "0" && text.Contains(task["Place"].str))
		{
			int whereChilidID = avatar.nomelTaskMag.getWhereChilidID(taskID, index1);
			text = text.Replace(task["Place"].str, Tools.Code64(jsonData.instance.NTaskSuiJI[whereChilidID.ToString()]["name"].str));
		}
		string text2 = Tools.instance.Code64ToString(text);
		((Component)val).GetComponent<Text>().text = text2;
		if (avatar.nomelTaskMag.XiangXiTaskIsEnd(task, taskID, index1))
		{
			((Component)val.Find("Toggle")).GetComponent<Toggle>().isOn = true;
			((Graphic)((Component)val).GetComponent<Text>()).color = Color.gray;
		}
	}

	public void showToggle()
	{
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		Avatar player = Tools.instance.getPlayer();
		UIToggle component = ((Component)DescBG.transform.Find("Toggle")).GetComponent<UIToggle>();
		if (player.taskMag.isNowTask(taskID) || TaskType == 1)
		{
			((Component)component).transform.localScale = new Vector3(0f, 0f, 0f);
		}
		else
		{
			((Component)component).transform.localScale = new Vector3(1f, 1f, 1f);
		}
	}

	public void showBiaoJi()
	{
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		Avatar player = Tools.instance.getPlayer();
		UIButton component = ((Component)DescBG.transform.Find("biaoji")).GetComponent<UIButton>();
		if (player.taskMag._TaskData["Task"][taskID.ToString()]["disableTask"].b)
		{
			((Component)component).transform.localScale = new Vector3(0f, 0f, 0f);
		}
		else
		{
			((Component)component).transform.localScale = new Vector3(1f, 1f, 1f);
		}
	}

	public void ChangedisableTask()
	{
		JSONObject jSONObject = Tools.instance.getPlayer().taskMag._TaskData["Task"][taskID.ToString()];
		UIToggle component = ((Component)DescBG.transform.Find("Toggle")).GetComponent<UIToggle>();
		jSONObject.SetField("disableTask", component.value);
		disableSelf();
		showBiaoJi();
	}

	public void SetTextPosition()
	{
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		UIToggle component = ((Component)this).GetComponent<UIToggle>();
		Transform val = ((Component)this).transform.Find("name");
		Transform val2 = ((Component)this).transform.Find("hongdian");
		Transform val3 = ((Component)this).transform.Find("Background");
		if (component.value)
		{
			val.localPosition = new Vector3(90f, -1f);
			val2.localPosition = new Vector3(156f, 0f);
			val3.localPosition = new Vector3(-8f, 0f);
		}
		else
		{
			val.localPosition = new Vector3(64f, -1f);
			val2.localPosition = new Vector3(131f, 0f);
			val3.localPosition = new Vector3(-33f, 0f);
		}
	}

	public void disableSelf()
	{
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		Avatar player = Tools.instance.getPlayer();
		if (!player.taskMag._TaskData["Task"].HasField(taskID.ToString()))
		{
			return;
		}
		if (player.taskMag._TaskData["Task"][taskID.ToString()]["disableTask"].b)
		{
			ToDown();
			((Component)this).GetComponent<UIButton>().defaultColor = new Color(0f, 0f, 0f);
		}
		else if (player.taskMag._TaskData.HasField("ShowTask"))
		{
			if ((int)player.taskMag._TaskData["ShowTask"].n == 0)
			{
				((Component)this).transform.SetSiblingIndex(0);
			}
			else
			{
				((Component)this).transform.SetSiblingIndex(1);
			}
			((Component)this).GetComponent<UIButton>().defaultColor = new Color(255f, 255f, 255f);
		}
	}

	public void setAvatarGuide()
	{
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		Avatar player = Tools.instance.getPlayer();
		if (player.taskMag.isNowTask(taskID))
		{
			player.taskMag.setNowTask(0);
		}
		else
		{
			player.taskMag.setNowTask(taskID);
		}
		foreach (Transform item in ((Component)this).transform.parent)
		{
			((Component)item).GetComponent<TaskCell>().setHongDian();
		}
		if ((Object)(object)AllMapManage.instance != (Object)null)
		{
			AllMapManage.instance.TaskFlag.transform.position = new Vector3(0f, 10000f, 0f);
			foreach (KeyValuePair<int, BaseMapCompont> item2 in AllMapManage.instance.mapIndex)
			{
				item2.Value.setFlag();
			}
		}
		showToggle();
	}

	private void Update()
	{
	}

	public void setHongDian()
	{
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		if (Tools.instance.getPlayer().taskMag.isNowTask(taskID))
		{
			ToTop();
			hongdian.transform.localScale = new Vector3(1f, 1f, 1f);
		}
		else
		{
			hongdian.transform.localScale = new Vector3(0f, 0f, 0f);
		}
	}

	public void ToTop()
	{
		((Component)this).transform.SetSiblingIndex(0);
	}

	public void ToDown()
	{
		int childCount = ((Component)this).transform.parent.childCount;
		((Component)this).transform.SetSiblingIndex(childCount - 1);
	}

	public string getTaskInfoDesc(int taskID, int index)
	{
		foreach (JSONObject item in jsonData.instance.TaskInfoJsonData.list)
		{
			if (item["TaskID"].I == taskID && item["TaskIndex"].I == index)
			{
				return item["Desc"].Str.STVarReplace();
			}
		}
		return "";
	}
}
