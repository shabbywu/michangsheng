using System;
using System.Collections.Generic;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200038B RID: 907
public class TaskDescManager : MonoBehaviour
{
	// Token: 0x06001DE1 RID: 7649 RVA: 0x000D2804 File Offset: 0x000D0A04
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

	// Token: 0x06001DE2 RID: 7650 RVA: 0x000D2864 File Offset: 0x000D0A64
	public void MapBtnEvent()
	{
		int id = int.Parse(this.MapBtn.name);
		UIMapPanel.Inst.OpenHighlight(id);
	}

	// Token: 0x06001DE3 RID: 7651 RVA: 0x000D2890 File Offset: 0x000D0A90
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
		List<JSONObject> list = jsonData.instance.TaskInfoJsonData.list.FindAll((JSONObject aa) => aa["TaskID"].I == task["id"].I && indexList.Contains(aa["TaskIndex"].I));
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

	// Token: 0x06001DE4 RID: 7652 RVA: 0x000D2B50 File Offset: 0x000D0D50
	private void setCurTime(JSONObject task)
	{
		int i = task["id"].I;
		Avatar player = Tools.instance.getPlayer();
		if (jsonData.instance.TaskJsonData[i.ToString()]["Type"].I == 1)
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
				DateTime endTime2 = DateTime.Parse(jsonData.instance.TaskJsonData[i.ToString()]["EndTime"].str);
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
			DateTime starTime = DateTime.Parse(jsonData.instance.TaskJsonData[i.ToString()]["StarTime"].str);
			DateTime endTime3 = DateTime.Parse(jsonData.instance.TaskJsonData[i.ToString()]["EndTime"].str);
			int circulation = (int)jsonData.instance.TaskJsonData[i.ToString()]["circulation"].n;
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

	// Token: 0x06001DE5 RID: 7653 RVA: 0x000D2DF6 File Offset: 0x000D0FF6
	private void setDesc(string str)
	{
		this.Desc.text = str.STVarReplace().ToCN();
	}

	// Token: 0x06001DE6 RID: 7654 RVA: 0x000D2E0E File Offset: 0x000D100E
	private void setDescTaskName(string str)
	{
		this.Name.text = Tools.Code64(str);
	}

	// Token: 0x06001DE7 RID: 7655 RVA: 0x000D2E21 File Offset: 0x000D1021
	private void setTaskNameColor(Color color)
	{
		this.Name.color = color;
	}

	// Token: 0x06001DE8 RID: 7656 RVA: 0x000D2E30 File Offset: 0x000D1030
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

	// Token: 0x06001DE9 RID: 7657 RVA: 0x000D2ED0 File Offset: 0x000D10D0
	public void clearCurTaskDesc()
	{
		this.clear();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001DEA RID: 7658 RVA: 0x000D2EE4 File Offset: 0x000D10E4
	public void setChuanWenWenTuo(JSONObject weiTuo)
	{
		this.clear();
		base.gameObject.SetActive(true);
		this.ChuanWenShengYuShiJian.SetActive(true);
		this.NomalTaskRenWuJinDu.SetActive(false);
		this.setTitle("事件状态:", "传闻时间", "传闻描述");
		int i = weiTuo["id"].I;
		Avatar player = Tools.instance.getPlayer();
		NTaskXiangXi ntaskXiangXiData = player.nomelTaskMag.GetNTaskXiangXiData(i);
		string text = ntaskXiangXiData.zongmiaoshu;
		if (text.Contains("{where=99}"))
		{
			int i2 = player.NomelTaskJson[i.ToString()]["TaskWhereChild"][0].I;
			text = text.Replace("{where=99}", NTaskSuiJI.DataDict[i2].name);
			this.MapBtn.gameObject.SetActive(true);
			this.MapBtn.gameObject.name = this.GetMapIndex(i2).ToString();
			text = text.Replace("‘", "");
			text = text.Replace("\"", "");
		}
		this.ChuanWenShengYuTime.text = TaskDescManager.getWeiTuoShengYuShiJian(weiTuo);
		this.HasTime.text = "   已开启";
		this.setDescTaskName(ntaskXiangXiData.name);
		this.setDesc((!text.Contains("{ZongMiaoShu}")) ? text : text.Replace("{ZongMiaoShu}", NTaskAllType.DataDict[i].ZongMiaoShu));
	}

	// Token: 0x06001DEB RID: 7659 RVA: 0x000D3068 File Offset: 0x000D1268
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

	// Token: 0x06001DEC RID: 7660 RVA: 0x000D30F8 File Offset: 0x000D12F8
	public void setWeiTuoDesc(JSONObject weiTuo)
	{
		this.clear();
		base.gameObject.SetActive(true);
		int i = weiTuo["id"].I;
		Avatar player = Tools.instance.getPlayer();
		NTaskXiangXi ntaskXiangXiData = player.nomelTaskMag.GetNTaskXiangXiData(i);
		string zongmiaoshu = ntaskXiangXiData.zongmiaoshu;
		DateTime endTime = Tools.GetEndTime(player.NomelTaskJson[i.ToString()]["StartTime"].str, 0, ntaskXiangXiData.shiXian, 0);
		this.HasTime.text = Tools.TimeToShengYuTime(Tools.getShengYuShiJian(player.worldTimeMag.getNowTime(), endTime), "").ToCN();
		this.setDescTaskName(ntaskXiangXiData.name);
		this.setDesc((!zongmiaoshu.Contains("{ZongMiaoShu}")) ? zongmiaoshu : zongmiaoshu.Replace("{ZongMiaoShu}", NTaskAllType.DataDict[i].ZongMiaoShu));
		int num = 0;
		foreach (JSONObject jsonobject in player.nomelTaskMag.GetNTaskXiangXiList(i))
		{
			int i2 = player.NomelTaskJson[i.ToString()]["TaskChild"][num].I;
			NTaskSuiJI ntaskSuiJI = NTaskSuiJI.DataDict[i2];
			string text = jsonobject["desc"].str.Replace(jsonobject["TaskID"].str, ntaskSuiJI.name);
			if (jsonobject["Place"].str != "0" && text.Contains(jsonobject["Place"].str))
			{
				int whereChilidID = player.nomelTaskMag.getWhereChilidID(i, num);
				text = text.Replace(jsonobject["Place"].str, NTaskSuiJI.DataDict[whereChilidID].name);
				this.MapBtn.gameObject.SetActive(true);
				this.MapBtn.gameObject.name = this.GetMapIndex(whereChilidID).ToString();
			}
			string str = text.ToCN();
			Tools.InstantiateGameObject(this.TaskIndex, this.TaskIndex.transform.parent).GetComponent<TaskIndexCell>().setContent(str, player.nomelTaskMag.XiangXiTaskIsEnd(jsonobject, i, num));
			num++;
		}
	}

	// Token: 0x06001DED RID: 7661 RVA: 0x000D3394 File Offset: 0x000D1594
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

	// Token: 0x06001DEE RID: 7662 RVA: 0x000D3465 File Offset: 0x000D1665
	private void setTitle(string status = "剩余时间:", string jindu = "任务进度", string des = "任务描述")
	{
		this.TaskStatus.text = status;
		this.TaskJinDu.text = jindu;
		this.TaskDes.text = des;
	}

	// Token: 0x06001DEF RID: 7663 RVA: 0x000D348C File Offset: 0x000D168C
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

	// Token: 0x06001DF0 RID: 7664 RVA: 0x000D3550 File Offset: 0x000D1750
	public static string getShengYuShiJi(JSONObject task)
	{
		int i = task["id"].I;
		Avatar player = Tools.instance.getPlayer();
		string text;
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
				text = Tools.TimeToShengYuTime(Tools.getShengYuShiJian(player.worldTimeMag.getNowTime(), endTime2), "剩余时间").ToCN();
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

	// Token: 0x06001DF1 RID: 7665 RVA: 0x000D3774 File Offset: 0x000D1974
	public static string getWeiTuoShengYuShiJian(JSONObject weiTuo)
	{
		int i = weiTuo["id"].I;
		Avatar player = Tools.instance.getPlayer();
		if (!player.NomelTaskJson[i.ToString()].HasField("StartTime"))
		{
			player.NomelTaskJson[i.ToString()].SetField("StartTime", player.worldTimeMag.nowTime);
			player.nomelTaskMag.RefreshGetNowNTaskData();
		}
		DateTime endTime = Tools.GetEndTime(player.NomelTaskJson[i.ToString()]["StartTime"].str, 0, player.nomelTaskMag.GetNTaskXiangXiData(i).shiXian, 0);
		player.worldTimeMag.getNowTime();
		return Tools.Code64(Tools.TimeToShengYuTime(Tools.getShengYuShiJian(player.worldTimeMag.getNowTime(), endTime), ""));
	}

	// Token: 0x06001DF2 RID: 7666 RVA: 0x000D3853 File Offset: 0x000D1A53
	private void setIsShowFinish(JSONObject task)
	{
		if (TaskUIManager.checkIsGuoShi(task) && TaskUIManager.inst.getIsOld())
		{
			this.FinshImage.SetActive(true);
			return;
		}
		this.FinshImage.SetActive(false);
	}

	// Token: 0x04001884 RID: 6276
	[SerializeField]
	private GameObject TaskIndex;

	// Token: 0x04001885 RID: 6277
	[SerializeField]
	private Text HasTime;

	// Token: 0x04001886 RID: 6278
	[SerializeField]
	private Text Desc;

	// Token: 0x04001887 RID: 6279
	[SerializeField]
	private Text Name;

	// Token: 0x04001888 RID: 6280
	[SerializeField]
	private GameObject FinshImage;

	// Token: 0x04001889 RID: 6281
	[SerializeField]
	private GameObject ZhuiZhongImage;

	// Token: 0x0400188A RID: 6282
	[SerializeField]
	private GameObject NoZhuiZhongImage;

	// Token: 0x0400188B RID: 6283
	[SerializeField]
	private Text TaskStatus;

	// Token: 0x0400188C RID: 6284
	[SerializeField]
	private Text TaskJinDu;

	// Token: 0x0400188D RID: 6285
	[SerializeField]
	private Text TaskDes;

	// Token: 0x0400188E RID: 6286
	[SerializeField]
	private Text chunWenStatus;

	// Token: 0x0400188F RID: 6287
	public GameObject HasOpen;

	// Token: 0x04001890 RID: 6288
	public GameObject NoOpen;

	// Token: 0x04001891 RID: 6289
	[SerializeField]
	private GameObject ChuanWenShengYuShiJian;

	// Token: 0x04001892 RID: 6290
	[SerializeField]
	private Text ChuanWenShengYuTime;

	// Token: 0x04001893 RID: 6291
	[SerializeField]
	private GameObject NomalTaskRenWuJinDu;

	// Token: 0x04001894 RID: 6292
	public FpBtn MapBtn;
}
