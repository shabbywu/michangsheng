using System;
using System.Collections.Generic;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200050F RID: 1295
public class TaskDescManager : MonoBehaviour
{
	// Token: 0x0600215A RID: 8538 RVA: 0x001164A8 File Offset: 0x001146A8
	private void Awake()
	{
		this.MapBtn.GetComponent<Image>().raycastTarget = true;
		Image[] componentsInChildren = this.MapBtn.GetComponentsInChildren<Image>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].raycastTarget = true;
		}
		this.MapBtn.mouseUpEvent.AddListener(new UnityAction(this.MapBtnEvent));
	}

	// Token: 0x0600215B RID: 8539 RVA: 0x00116508 File Offset: 0x00114708
	public void MapBtnEvent()
	{
		int id = int.Parse(this.MapBtn.name);
		UIMapPanel.Inst.OpenHighlight(id);
	}

	// Token: 0x0600215C RID: 8540 RVA: 0x00116534 File Offset: 0x00114734
	public void setCurTaskDesc(JSONObject task)
	{
		this.clear();
		base.gameObject.SetActive(true);
		this.setCurTime(task);
		this.setDesc(jsonData.instance.TaskJsonData[task["id"].n.ToString()]["Desc"].str);
		this.setDescTaskName(jsonData.instance.TaskJsonData[task["id"].n.ToString()]["Name"].str);
		this.setTitle("剩余时间:", "任务进度", "任务描述");
		this.setIsShowFinish(task);
		List<int> indexList = new List<int>();
		for (int i = 0; i < task["AllIndex"].Count; i++)
		{
			indexList.Add(task["AllIndex"][i].I);
		}
		List<JSONObject> list = jsonData.instance.TaskInfoJsonData.list.FindAll((JSONObject aa) => (int)aa["TaskID"].n == (int)task["id"].n && indexList.Contains(aa["TaskIndex"].I));
		if (task["disableTask"].b)
		{
			for (int j = 0; j < task["AllIndex"].Count; j++)
			{
				Tools.InstantiateGameObject(this.TaskIndex, this.TaskIndex.transform.parent).GetComponent<TaskIndexCell>().setContent(list[j]["Desc"].str, true);
			}
			return;
		}
		for (int k = 0; k < task["AllIndex"].Count; k++)
		{
			TaskIndexCell component = Tools.InstantiateGameObject(this.TaskIndex, this.TaskIndex.transform.parent).GetComponent<TaskIndexCell>();
			if (k == task["AllIndex"].Count - 1)
			{
				if (list[k]["mapIndex"].I > 0)
				{
					this.MapBtn.gameObject.SetActive(true);
					this.MapBtn.gameObject.name = list[k]["mapIndex"].I.ToString();
				}
				component.setContent(list[k]["Desc"].str, false);
			}
			else
			{
				component.setContent(list[k]["Desc"].str, true);
			}
		}
	}

	// Token: 0x0600215D RID: 8541 RVA: 0x001167F4 File Offset: 0x001149F4
	private void setCurTime(JSONObject task)
	{
		int num = (int)task["id"].n;
		Avatar player = Tools.instance.getPlayer();
		if (jsonData.instance.TaskJsonData[num.ToString()]["Type"].I == 1)
		{
			if (TaskUIManager.checkIsGuoShi(task))
			{
				this.HasTime.text = "   已过时";
				this.FinshImage.SetActive(true);
				return;
			}
			string text;
			if (task.HasField("continueTime") && task["continueTime"].I > 0)
			{
				DateTime endTime = DateTime.Parse(task["curTime"].str).AddMonths(task["continueTime"].I);
				text = Tools.Code64(Tools.TimeToShengYuTime(Tools.getShengYuShiJian(player.worldTimeMag.getNowTime(), endTime), ""));
			}
			else
			{
				DateTime endTime2 = DateTime.Parse(jsonData.instance.TaskJsonData[num.ToString()]["EndTime"].str);
				text = Tools.Code64(Tools.TimeToShengYuTime(Tools.getShengYuShiJian(player.worldTimeMag.getNowTime(), endTime2), "剩余时间"));
			}
			if (text.Contains("剩余时间"))
			{
				text = text.Replace("剩余时间", "");
			}
			this.HasTime.text = text;
			return;
		}
		else
		{
			DateTime starTime = DateTime.Parse(jsonData.instance.TaskJsonData[num.ToString()]["StarTime"].str);
			DateTime endTime3 = DateTime.Parse(jsonData.instance.TaskJsonData[num.ToString()]["EndTime"].str);
			int circulation = (int)jsonData.instance.TaskJsonData[num.ToString()]["circulation"].n;
			DateTime nowTime = player.worldTimeMag.getNowTime();
			string text = Tools.Code64(TaskCell.getTaskNextTime(circulation, nowTime, starTime, endTime3));
			if (TaskUIManager.checkIsGuoShi(task))
			{
				this.ChuanWenShengYuTime.text = "已过时";
				this.chunWenStatus.text = "事件状态:";
				return;
			}
			if (text.Contains("距离事件开始还有"))
			{
				this.ChuanWenShengYuTime.text = text.Replace("距离事件开始还有", "");
				this.NoOpen.SetActive(true);
				this.HasOpen.SetActive(false);
				return;
			}
			if (text.Contains("事件已开启"))
			{
				this.ChuanWenShengYuTime.text = "已开启";
				this.NoOpen.SetActive(false);
				this.HasOpen.SetActive(true);
			}
			return;
		}
	}

	// Token: 0x0600215E RID: 8542 RVA: 0x0001B7F5 File Offset: 0x000199F5
	private void setDesc(string str)
	{
		this.Desc.text = str.STVarReplace().ToCN();
	}

	// Token: 0x0600215F RID: 8543 RVA: 0x0001B80D File Offset: 0x00019A0D
	private void setDescTaskName(string str)
	{
		this.Name.text = Tools.Code64(str);
	}

	// Token: 0x06002160 RID: 8544 RVA: 0x0001B820 File Offset: 0x00019A20
	private void setTaskNameColor(Color color)
	{
		this.Name.color = color;
	}

	// Token: 0x06002161 RID: 8545 RVA: 0x00116A9C File Offset: 0x00114C9C
	private void clear()
	{
		foreach (object obj in this.TaskIndex.transform.parent)
		{
			Transform transform = (Transform)obj;
			if (transform.gameObject.activeSelf)
			{
				Object.Destroy(transform.gameObject);
			}
		}
		this.MapBtn.gameObject.SetActive(false);
		this.NomalTaskRenWuJinDu.SetActive(true);
		this.ChuanWenShengYuShiJian.SetActive(false);
	}

	// Token: 0x06002162 RID: 8546 RVA: 0x0001B82E File Offset: 0x00019A2E
	public void clearCurTaskDesc()
	{
		this.clear();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06002163 RID: 8547 RVA: 0x00116B3C File Offset: 0x00114D3C
	public void setChuanWenWenTuo(JSONObject weiTuo)
	{
		this.clear();
		base.gameObject.SetActive(true);
		this.ChuanWenShengYuShiJian.SetActive(true);
		this.NomalTaskRenWuJinDu.SetActive(false);
		this.setTitle("事件状态:", "传闻时间", "传闻描述");
		int num = (int)weiTuo["id"].n;
		Avatar player = Tools.instance.getPlayer();
		NTaskXiangXi ntaskXiangXiData = player.nomelTaskMag.GetNTaskXiangXiData(num);
		string text = ntaskXiangXiData.zongmiaoshu;
		if (text.Contains("{where=99}"))
		{
			int i = player.NomelTaskJson[num.ToString()]["TaskWhereChild"][0].I;
			text = text.Replace("{where=99}", NTaskSuiJI.DataDict[i].name);
			this.MapBtn.gameObject.SetActive(true);
			this.MapBtn.gameObject.name = this.GetMapIndex(i).ToString();
			text = text.Replace("‘", "");
			text = text.Replace("\"", "");
		}
		this.ChuanWenShengYuTime.text = TaskDescManager.getWeiTuoShengYuShiJian(weiTuo);
		this.HasTime.text = "   已开启";
		this.setDescTaskName(ntaskXiangXiData.name);
		this.setDesc((!text.Contains("{ZongMiaoShu}")) ? text : text.Replace("{ZongMiaoShu}", NTaskAllType.DataDict[num].ZongMiaoShu));
	}

	// Token: 0x06002164 RID: 8548 RVA: 0x00116CC4 File Offset: 0x00114EC4
	public int GetMapIndex(int id)
	{
		int num = 0;
		string strValue = NTaskSuiJI.DataDict[id].StrValue;
		foreach (MapIndexData mapIndexData in MapIndexData.DataList)
		{
			if (mapIndexData.StrValue == strValue)
			{
				num = mapIndexData.mapIndex;
				break;
			}
		}
		if (num == 0)
		{
			Debug.LogError("GetMapIndex" + strValue + ",不存在地图点");
		}
		return num;
	}

	// Token: 0x06002165 RID: 8549 RVA: 0x00116D54 File Offset: 0x00114F54
	public void setWeiTuoDesc(JSONObject weiTuo)
	{
		this.clear();
		base.gameObject.SetActive(true);
		int num = (int)weiTuo["id"].n;
		Avatar player = Tools.instance.getPlayer();
		NTaskXiangXi ntaskXiangXiData = player.nomelTaskMag.GetNTaskXiangXiData(num);
		string zongmiaoshu = ntaskXiangXiData.zongmiaoshu;
		DateTime endTime = Tools.GetEndTime(player.NomelTaskJson[num.ToString()]["StartTime"].str, 0, ntaskXiangXiData.shiXian, 0);
		this.HasTime.text = Tools.TimeToShengYuTime(Tools.getShengYuShiJian(player.worldTimeMag.getNowTime(), endTime), "").ToCN();
		this.setDescTaskName(ntaskXiangXiData.name);
		this.setDesc((!zongmiaoshu.Contains("{ZongMiaoShu}")) ? zongmiaoshu : zongmiaoshu.Replace("{ZongMiaoShu}", NTaskAllType.DataDict[num].ZongMiaoShu));
		int num2 = 0;
		foreach (JSONObject jsonobject in player.nomelTaskMag.GetNTaskXiangXiList(num))
		{
			int i = player.NomelTaskJson[num.ToString()]["TaskChild"][num2].I;
			NTaskSuiJI ntaskSuiJI = NTaskSuiJI.DataDict[i];
			string text = jsonobject["desc"].str.Replace(jsonobject["TaskID"].str, ntaskSuiJI.name);
			if (jsonobject["Place"].str != "0" && text.Contains(jsonobject["Place"].str))
			{
				int whereChilidID = player.nomelTaskMag.getWhereChilidID(num, num2);
				text = text.Replace(jsonobject["Place"].str, NTaskSuiJI.DataDict[whereChilidID].name);
				this.MapBtn.gameObject.SetActive(true);
				this.MapBtn.gameObject.name = this.GetMapIndex(whereChilidID).ToString();
			}
			string str = text.ToCN();
			Tools.InstantiateGameObject(this.TaskIndex, this.TaskIndex.transform.parent).GetComponent<TaskIndexCell>().setContent(str, player.nomelTaskMag.XiangXiTaskIsEnd(jsonobject, num, num2));
			num2++;
		}
	}

	// Token: 0x06002166 RID: 8550 RVA: 0x00116FF0 File Offset: 0x001151F0
	public void checkIsZhuiZhong()
	{
		Avatar player = Tools.instance.getPlayer();
		if (player.TaskZhuiZhong.HasField("curTask") && player.TaskZhuiZhong["CurTaskID"].I == TaskUIManager.inst.getCurTaskID() && player.TaskZhuiZhong["curType"].I == TaskUIManager.inst.getCurType())
		{
			this.setTaskNameColor(new Color(0.88235295f, 0.9843137f, 0.72156864f));
			this.ZhuiZhongImage.SetActive(true);
			this.NoZhuiZhongImage.SetActive(false);
			return;
		}
		this.setTaskNameColor(new Color(0.48235294f, 0.3137255f, 0.18431373f));
		this.ZhuiZhongImage.SetActive(false);
		this.NoZhuiZhongImage.SetActive(true);
	}

	// Token: 0x06002167 RID: 8551 RVA: 0x0001B842 File Offset: 0x00019A42
	private void setTitle(string status = "剩余时间:", string jindu = "任务进度", string des = "任务描述")
	{
		this.TaskStatus.text = status;
		this.TaskJinDu.text = jindu;
		this.TaskDes.text = des;
	}

	// Token: 0x06002168 RID: 8552 RVA: 0x001170C4 File Offset: 0x001152C4
	public void setChuanMiaoShu(JSONObject task)
	{
		this.clear();
		base.gameObject.SetActive(true);
		this.ChuanWenShengYuShiJian.SetActive(true);
		this.NomalTaskRenWuJinDu.SetActive(false);
		this.setCurTime(task);
		TaskJsonData taskJsonData = TaskJsonData.DataDict[task["id"].I];
		if (taskJsonData.mapIndex > 0)
		{
			this.MapBtn.gameObject.SetActive(true);
			this.MapBtn.gameObject.name = taskJsonData.mapIndex.ToString();
		}
		this.setDesc(taskJsonData.Desc);
		this.setDescTaskName(taskJsonData.Name);
		this.setTitle("事件状态:", "传闻时间", "传闻描述");
		this.setIsShowFinish(task);
	}

	// Token: 0x06002169 RID: 8553 RVA: 0x00117188 File Offset: 0x00115388
	public static string getShengYuShiJi(JSONObject task)
	{
		int num = (int)task["id"].n;
		Avatar player = Tools.instance.getPlayer();
		string text;
		if (jsonData.instance.TaskJsonData[num.ToString()]["Type"].I == 1)
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
				DateTime endTime2 = DateTime.Parse(jsonData.instance.TaskJsonData[num.ToString()]["EndTime"].str);
				text = Tools.TimeToShengYuTime(Tools.getShengYuShiJian(player.worldTimeMag.getNowTime(), endTime2), "剩余时间").ToCN();
			}
			if (text.Contains("剩余时间"))
			{
				text = text.Replace("剩余时间", "");
			}
		}
		else
		{
			DateTime starTime = DateTime.Parse(jsonData.instance.TaskJsonData[num.ToString()]["StarTime"].str);
			DateTime endTime3 = DateTime.Parse(jsonData.instance.TaskJsonData[num.ToString()]["EndTime"].str);
			int circulation = (int)jsonData.instance.TaskJsonData[num.ToString()]["circulation"].n;
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

	// Token: 0x0600216A RID: 8554 RVA: 0x001173AC File Offset: 0x001155AC
	public static string getWeiTuoShengYuShiJian(JSONObject weiTuo)
	{
		int taskID = (int)weiTuo["id"].n;
		Avatar player = Tools.instance.getPlayer();
		if (!player.NomelTaskJson[taskID.ToString()].HasField("StartTime"))
		{
			player.NomelTaskJson[taskID.ToString()].SetField("StartTime", player.worldTimeMag.nowTime);
			player.nomelTaskMag.RefreshGetNowNTaskData();
		}
		DateTime endTime = Tools.GetEndTime(player.NomelTaskJson[taskID.ToString()]["StartTime"].str, 0, player.nomelTaskMag.GetNTaskXiangXiData(taskID).shiXian, 0);
		player.worldTimeMag.getNowTime();
		return Tools.Code64(Tools.TimeToShengYuTime(Tools.getShengYuShiJian(player.worldTimeMag.getNowTime(), endTime), ""));
	}

	// Token: 0x0600216B RID: 8555 RVA: 0x0001B868 File Offset: 0x00019A68
	private void setIsShowFinish(JSONObject task)
	{
		if (TaskUIManager.checkIsGuoShi(task) && TaskUIManager.inst.getIsOld())
		{
			this.FinshImage.SetActive(true);
			return;
		}
		this.FinshImage.SetActive(false);
	}

	// Token: 0x04001CE2 RID: 7394
	[SerializeField]
	private GameObject TaskIndex;

	// Token: 0x04001CE3 RID: 7395
	[SerializeField]
	private Text HasTime;

	// Token: 0x04001CE4 RID: 7396
	[SerializeField]
	private Text Desc;

	// Token: 0x04001CE5 RID: 7397
	[SerializeField]
	private Text Name;

	// Token: 0x04001CE6 RID: 7398
	[SerializeField]
	private GameObject FinshImage;

	// Token: 0x04001CE7 RID: 7399
	[SerializeField]
	private GameObject ZhuiZhongImage;

	// Token: 0x04001CE8 RID: 7400
	[SerializeField]
	private GameObject NoZhuiZhongImage;

	// Token: 0x04001CE9 RID: 7401
	[SerializeField]
	private Text TaskStatus;

	// Token: 0x04001CEA RID: 7402
	[SerializeField]
	private Text TaskJinDu;

	// Token: 0x04001CEB RID: 7403
	[SerializeField]
	private Text TaskDes;

	// Token: 0x04001CEC RID: 7404
	[SerializeField]
	private Text chunWenStatus;

	// Token: 0x04001CED RID: 7405
	public GameObject HasOpen;

	// Token: 0x04001CEE RID: 7406
	public GameObject NoOpen;

	// Token: 0x04001CEF RID: 7407
	[SerializeField]
	private GameObject ChuanWenShengYuShiJian;

	// Token: 0x04001CF0 RID: 7408
	[SerializeField]
	private Text ChuanWenShengYuTime;

	// Token: 0x04001CF1 RID: 7409
	[SerializeField]
	private GameObject NomalTaskRenWuJinDu;

	// Token: 0x04001CF2 RID: 7410
	public FpBtn MapBtn;
}
