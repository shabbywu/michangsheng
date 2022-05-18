using System;
using System.Collections.Generic;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002A5 RID: 677
public class TaskCell : MonoBehaviour
{
	// Token: 0x0600149B RID: 5275 RVA: 0x00012FC6 File Offset: 0x000111C6
	private void Start()
	{
		this.hongdian = base.transform.Find("hongdian").gameObject;
		this.setHongDian();
		this.disableSelf();
	}

	// Token: 0x0600149C RID: 5276 RVA: 0x000BA050 File Offset: 0x000B8250
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
				DateTime dateTime = new DateTime(1, 1, 1).AddDays((double)timeSpan.Days);
				result = Tools.getStr("chuanwen7").Replace("{X}", string.Concat(new object[]
				{
					dateTime.Year - 1,
					"年",
					dateTime.Month - 1,
					"月",
					dateTime.Day - 1,
					"日"
				}));
			}
		}
		else
		{
			if (now > StarTime)
			{
				now = new DateTime(now.Year - circulation * ((now.Year - StarTime.Year) / circulation), now.Month, (now.Month == 2 && now.Day == 29) ? 28 : now.Day);
			}
			DateTime d = new DateTime(StarTime.Year + circulation, StarTime.Month, StarTime.Day);
			if (StarTime <= now && now <= EndTime)
			{
				result = Tools.getStr("chuanwen5");
			}
			else
			{
				TimeSpan timeSpan2 = d - now;
				if (StarTime > now && StarTime.Year == now.Year)
				{
					timeSpan2 = StarTime - now;
				}
				DateTime dateTime2 = new DateTime(1, 1, 1).AddDays((double)timeSpan2.Days);
				result = Tools.getStr("chuanwen7").Replace("{X}", string.Concat(new object[]
				{
					dateTime2.Year - 1,
					"年",
					dateTime2.Month - 1,
					"月",
					dateTime2.Day - 1,
					"日"
				}));
			}
		}
		return result;
	}

	// Token: 0x0600149D RID: 5277 RVA: 0x000BA290 File Offset: 0x000B8490
	public void openView()
	{
		UILabel component = this.DescBG.transform.Find("title").GetComponent<UILabel>();
		UILabel component2 = this.DescBG.transform.Find("title1").GetComponent<UILabel>();
		UILabel component3 = this.DescBG.transform.Find("title2").GetComponent<UILabel>();
		UILabel component4 = this.DescBG.transform.Find("message").GetComponent<UILabel>();
		UIButton component5 = this.DescBG.transform.Find("biaoji").GetComponent<UIButton>();
		UIToggle component6 = this.DescBG.transform.Find("Toggle").GetComponent<UIToggle>();
		UILabel component7 = this.DescBG.transform.Find("shengyutime").GetComponent<UILabel>();
		Transform transform = this.DescBG.transform.Find("Canvas/Scroll View/Viewport/Content");
		Transform transform2 = this.DescBG.transform.Find("chuanwen");
		Transform child = transform.transform.GetChild(0);
		Avatar player = Tools.instance.getPlayer();
		NTaskAllType ntaskAllType = NTaskAllType.DataDict[this.taskID];
		NTaskXiangXi ntaskXiangXiData = player.nomelTaskMag.GetNTaskXiangXiData(this.taskID);
		if (!jsonData.instance.TaskJsonData.HasField(this.taskID.ToString()) && this.TaskType != 2)
		{
			return;
		}
		component7.gameObject.SetActive(false);
		foreach (object obj in transform)
		{
			Transform transform3 = (Transform)obj;
			if (transform3.gameObject.activeSelf)
			{
				Object.Destroy(transform3.gameObject);
			}
		}
		if (this.TaskType == 1 || this.TaskType == 0)
		{
			component.text = jsonData.instance.TaskJsonData[this.taskID.ToString()]["Name"].Str;
			string str = jsonData.instance.TaskJsonData[this.taskID.ToString()]["Desc"].Str;
			component4.text = str.STVarReplace();
		}
		if (this.TaskType == 0)
		{
			transform.gameObject.SetActive(false);
			transform2.gameObject.SetActive(true);
			component2.text = Tools.getStr("chuanwen1");
			component3.text = Tools.getStr("chuanwen2");
			DateTime starTime = DateTime.Parse(jsonData.instance.TaskJsonData[this.taskID.ToString()]["StarTime"].str);
			DateTime endTime = DateTime.Parse(jsonData.instance.TaskJsonData[this.taskID.ToString()]["EndTime"].str);
			int circulation = (int)jsonData.instance.TaskJsonData[this.taskID.ToString()]["circulation"].n;
			DateTime nowTime = Tools.instance.getPlayer().worldTimeMag.getNowTime();
			string taskNextTime = TaskCell.getTaskNextTime(circulation, nowTime, starTime, endTime);
			transform2.transform.Find("Label").GetComponent<UILabel>().text = taskNextTime;
		}
		else
		{
			if (this.TaskType == 1)
			{
				transform.gameObject.SetActive(true);
				transform2.gameObject.SetActive(false);
				component2.text = Tools.getStr("chuanwen3");
				component3.text = Tools.getStr("chuanwen4");
				using (List<JSONObject>.Enumerator enumerator2 = player.taskMag._TaskData["Task"][this.taskID.ToString()]["AllIndex"].list.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						JSONObject jsonobject = enumerator2.Current;
						Transform transform4 = Object.Instantiate<Transform>(child);
						transform4.gameObject.SetActive(true);
						transform4.SetParent(transform);
						transform4.localScale = Vector3.one;
						transform4.localPosition = Vector3.zero;
						string text = Tools.instance.Code64ToString(this.getTaskInfoDesc(this.taskID, (int)jsonobject.n));
						transform4.GetComponent<Text>().text = text;
						if (player.taskMag._TaskData["Task"][this.taskID.ToString()].HasField("finishIndex"))
						{
							using (List<JSONObject>.Enumerator enumerator3 = player.taskMag._TaskData["Task"][this.taskID.ToString()]["finishIndex"].list.GetEnumerator())
							{
								while (enumerator3.MoveNext())
								{
									if ((int)enumerator3.Current.n == (int)jsonobject.n)
									{
										transform4.Find("Toggle").GetComponent<Toggle>().isOn = true;
										transform4.GetComponent<Text>().color = Color.gray;
									}
								}
							}
						}
					}
					goto IL_8E5;
				}
			}
			if (this.TaskType == 2)
			{
				component7.gameObject.SetActive(true);
				transform.gameObject.SetActive(true);
				transform2.gameObject.SetActive(false);
				component.text = ntaskXiangXiData.name;
				string zongmiaoshu = ntaskXiangXiData.zongmiaoshu;
				component4.text = ((!zongmiaoshu.Contains("{ZongMiaoShu}")) ? zongmiaoshu : zongmiaoshu.Replace("{ZongMiaoShu}", ntaskAllType.ZongMiaoShu));
				if (ntaskAllType.seid.Contains(2))
				{
					component2.text = Tools.getStr("chuanwen1");
					component3.text = Tools.getStr("chuanwen2");
					transform2.gameObject.SetActive(true);
					transform2.transform.Find("Label").GetComponent<UILabel>().text = "事件已开启";
					component7.gameObject.SetActive(false);
				}
				else
				{
					component2.text = Tools.getStr("chuanwen3");
					component3.text = Tools.getStr("chuanwen4");
					int num = 0;
					foreach (JSONObject jsonobject2 in player.nomelTaskMag.GetNTaskXiangXiList(this.taskID))
					{
						Transform transform5 = Object.Instantiate<Transform>(child);
						transform5.gameObject.SetActive(true);
						transform5.SetParent(transform);
						transform5.localScale = Vector3.one;
						transform5.localPosition = Vector3.zero;
						int i = player.NomelTaskJson[this.taskID.ToString()]["TaskChild"][num].I;
						string text2 = jsonobject2["desc"].str.Replace(jsonobject2["TaskID"].str, NTaskSuiJI.DataDict[i].name);
						if (jsonobject2["Place"].str != "0" && text2.Contains(jsonobject2["Place"].str))
						{
							int whereChilidID = player.nomelTaskMag.getWhereChilidID(this.taskID, num);
							text2 = text2.Replace(jsonobject2["Place"].str, NTaskSuiJI.DataDict[whereChilidID].name);
						}
						string text3 = text2.ToCN();
						transform5.GetComponent<Text>().text = text3;
						if (player.nomelTaskMag.XiangXiTaskIsEnd(jsonobject2, this.taskID, num))
						{
							transform5.Find("Toggle").GetComponent<Toggle>().isOn = true;
							transform5.GetComponent<Text>().color = Color.gray;
						}
						num++;
					}
					if (ntaskXiangXiData.JiaoFuType == 1 && player.nomelTaskMag.nowChildNTask(this.taskID) == -1)
					{
						Transform transform6 = Object.Instantiate<Transform>(child);
						transform6.gameObject.SetActive(true);
						transform6.SetParent(transform);
						transform6.localScale = Vector3.one;
						transform6.localPosition = Vector3.zero;
						transform6.GetComponent<Text>().text = string.Concat(new string[]
						{
							"回",
							ntaskAllType.jiaofudidian,
							"，向",
							ntaskAllType.jiaofurenwu,
							"交付任务"
						});
					}
					DateTime endTime2 = Tools.GetEndTime(player.NomelTaskJson[this.taskID.ToString()]["StartTime"].str, 0, ntaskXiangXiData.shiXian, 0);
					component7.text = Tools.TimeToShengYuTime(Tools.getShengYuShiJian(player.worldTimeMag.getNowTime(), endTime2), "剩余时间");
				}
				component5.gameObject.SetActive(false);
				component6.gameObject.SetActive(false);
			}
		}
		IL_8E5:
		if (this.TaskType == 1 || this.TaskType == 0)
		{
			component5.onClick.Clear();
			component5.onClick.Add(new EventDelegate(new EventDelegate.Callback(this.setAvatarGuide)));
			this.showToggle();
			this.showBiaoJi();
			component6.onChange.Clear();
			component6.value = player.taskMag._TaskData["Task"][this.taskID.ToString()]["disableTask"].b;
			component6.onChange.Add(new EventDelegate(new EventDelegate.Callback(this.ChangedisableTask)));
		}
	}

	// Token: 0x0600149E RID: 5278 RVA: 0x000BAC94 File Offset: 0x000B8E94
	public void AddZiXiang(JSONObject task, Transform tempText, Transform textGrid, Avatar avatar, int index1)
	{
		Transform transform = Object.Instantiate<Transform>(tempText);
		transform.gameObject.SetActive(true);
		transform.SetParent(textGrid);
		transform.localScale = Vector3.one;
		transform.localPosition = Vector3.zero;
		int i = avatar.NomelTaskJson[this.taskID.ToString()]["TaskChild"][index1].I;
		string text = task["desc"].str.Replace(task["TaskID"].str, Tools.Code64(jsonData.instance.NTaskSuiJI[i.ToString()]["name"].str));
		if (task["Place"].str != "0" && text.Contains(task["Place"].str))
		{
			int whereChilidID = avatar.nomelTaskMag.getWhereChilidID(this.taskID, index1);
			text = text.Replace(task["Place"].str, Tools.Code64(jsonData.instance.NTaskSuiJI[whereChilidID.ToString()]["name"].str));
		}
		string text2 = Tools.instance.Code64ToString(text);
		transform.GetComponent<Text>().text = text2;
		if (avatar.nomelTaskMag.XiangXiTaskIsEnd(task, this.taskID, index1))
		{
			transform.Find("Toggle").GetComponent<Toggle>().isOn = true;
			transform.GetComponent<Text>().color = Color.gray;
		}
	}

	// Token: 0x0600149F RID: 5279 RVA: 0x000BAE30 File Offset: 0x000B9030
	public void showToggle()
	{
		Avatar player = Tools.instance.getPlayer();
		UIToggle component = this.DescBG.transform.Find("Toggle").GetComponent<UIToggle>();
		if (player.taskMag.isNowTask(this.taskID) || this.TaskType == 1)
		{
			component.transform.localScale = new Vector3(0f, 0f, 0f);
			return;
		}
		component.transform.localScale = new Vector3(1f, 1f, 1f);
	}

	// Token: 0x060014A0 RID: 5280 RVA: 0x000BAEBC File Offset: 0x000B90BC
	public void showBiaoJi()
	{
		Avatar player = Tools.instance.getPlayer();
		UIButton component = this.DescBG.transform.Find("biaoji").GetComponent<UIButton>();
		if (player.taskMag._TaskData["Task"][this.taskID.ToString()]["disableTask"].b)
		{
			component.transform.localScale = new Vector3(0f, 0f, 0f);
			return;
		}
		component.transform.localScale = new Vector3(1f, 1f, 1f);
	}

	// Token: 0x060014A1 RID: 5281 RVA: 0x000BAF64 File Offset: 0x000B9164
	public void ChangedisableTask()
	{
		JSONObject jsonobject = Tools.instance.getPlayer().taskMag._TaskData["Task"][this.taskID.ToString()];
		UIToggle component = this.DescBG.transform.Find("Toggle").GetComponent<UIToggle>();
		jsonobject.SetField("disableTask", component.value);
		this.disableSelf();
		this.showBiaoJi();
	}

	// Token: 0x060014A2 RID: 5282 RVA: 0x000BAFD8 File Offset: 0x000B91D8
	public void SetTextPosition()
	{
		UIToggle component = base.GetComponent<UIToggle>();
		Transform transform = base.transform.Find("name");
		Transform transform2 = base.transform.Find("hongdian");
		Transform transform3 = base.transform.Find("Background");
		if (component.value)
		{
			transform.localPosition = new Vector3(90f, -1f);
			transform2.localPosition = new Vector3(156f, 0f);
			transform3.localPosition = new Vector3(-8f, 0f);
			return;
		}
		transform.localPosition = new Vector3(64f, -1f);
		transform2.localPosition = new Vector3(131f, 0f);
		transform3.localPosition = new Vector3(-33f, 0f);
	}

	// Token: 0x060014A3 RID: 5283 RVA: 0x000BB0A4 File Offset: 0x000B92A4
	public void disableSelf()
	{
		Avatar player = Tools.instance.getPlayer();
		if (!player.taskMag._TaskData["Task"].HasField(this.taskID.ToString()))
		{
			return;
		}
		if (player.taskMag._TaskData["Task"][this.taskID.ToString()]["disableTask"].b)
		{
			this.ToDown();
			base.GetComponent<UIButton>().defaultColor = new Color(0f, 0f, 0f);
			return;
		}
		if (player.taskMag._TaskData.HasField("ShowTask"))
		{
			if ((int)player.taskMag._TaskData["ShowTask"].n == 0)
			{
				base.transform.SetSiblingIndex(0);
			}
			else
			{
				base.transform.SetSiblingIndex(1);
			}
			base.GetComponent<UIButton>().defaultColor = new Color(255f, 255f, 255f);
		}
	}

	// Token: 0x060014A4 RID: 5284 RVA: 0x000BB1B0 File Offset: 0x000B93B0
	public void setAvatarGuide()
	{
		Avatar player = Tools.instance.getPlayer();
		if (player.taskMag.isNowTask(this.taskID))
		{
			player.taskMag.setNowTask(0);
		}
		else
		{
			player.taskMag.setNowTask(this.taskID);
		}
		foreach (object obj in base.transform.parent)
		{
			((Transform)obj).GetComponent<TaskCell>().setHongDian();
		}
		if (AllMapManage.instance != null)
		{
			AllMapManage.instance.TaskFlag.transform.position = new Vector3(0f, 10000f, 0f);
			foreach (KeyValuePair<int, BaseMapCompont> keyValuePair in AllMapManage.instance.mapIndex)
			{
				keyValuePair.Value.setFlag();
			}
		}
		this.showToggle();
	}

	// Token: 0x060014A5 RID: 5285 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x060014A6 RID: 5286 RVA: 0x000BB2D4 File Offset: 0x000B94D4
	public void setHongDian()
	{
		if (Tools.instance.getPlayer().taskMag.isNowTask(this.taskID))
		{
			this.ToTop();
			this.hongdian.transform.localScale = new Vector3(1f, 1f, 1f);
			return;
		}
		this.hongdian.transform.localScale = new Vector3(0f, 0f, 0f);
	}

	// Token: 0x060014A7 RID: 5287 RVA: 0x00012FEF File Offset: 0x000111EF
	public void ToTop()
	{
		base.transform.SetSiblingIndex(0);
	}

	// Token: 0x060014A8 RID: 5288 RVA: 0x000BB34C File Offset: 0x000B954C
	public void ToDown()
	{
		int childCount = base.transform.parent.childCount;
		base.transform.SetSiblingIndex(childCount - 1);
	}

	// Token: 0x060014A9 RID: 5289 RVA: 0x000BB378 File Offset: 0x000B9578
	public string getTaskInfoDesc(int taskID, int index)
	{
		foreach (JSONObject jsonobject in jsonData.instance.TaskInfoJsonData.list)
		{
			if (jsonobject["TaskID"].I == taskID && jsonobject["TaskIndex"].I == index)
			{
				return jsonobject["Desc"].Str.STVarReplace();
			}
		}
		return "";
	}

	// Token: 0x04000FE7 RID: 4071
	public GameObject DescBG;

	// Token: 0x04000FE8 RID: 4072
	public int taskID;

	// Token: 0x04000FE9 RID: 4073
	private GameObject hongdian;

	// Token: 0x04000FEA RID: 4074
	public int TaskType;
}
