using System;
using System.Collections.Generic;
using Fungus;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002C3 RID: 707
public class UIMiniTaskPanel : MonoBehaviour
{
	// Token: 0x060018CB RID: 6347 RVA: 0x000B1C5C File Offset: 0x000AFE5C
	private void Awake()
	{
		UIMiniTaskPanel.Inst = this;
	}

	// Token: 0x060018CC RID: 6348 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x060018CD RID: 6349 RVA: 0x000B1C64 File Offset: 0x000AFE64
	private void Update()
	{
		if ((!(PanelMamager.inst != null) || !(PanelMamager.inst.UISceneGameObject != null)) && this.ScaleObj.activeInHierarchy)
		{
			this.ScaleObj.SetActive(false);
		}
	}

	// Token: 0x060018CE RID: 6350 RVA: 0x000B1CA0 File Offset: 0x000AFEA0
	public void RefreshUI()
	{
		this.refreshCD -= Time.deltaTime;
		if (this.refreshCD >= 0f)
		{
			return;
		}
		this.refreshCD = 1f;
		Avatar player = Tools.instance.getPlayer();
		if (player == null)
		{
			return;
		}
		DateTime nowTime = player.worldTimeMag.getNowTime();
		this.WorldTimeText.text = string.Format("{0}年{1}月{2}日", nowTime.Year, nowTime.Month, nowTime.Day);
		string screenName = Tools.getScreenName();
		if (UIFuBenShengYuTimePanel.Inst != null)
		{
			if (jsonData.instance.FuBenInfoJsonData.HasField(screenName))
			{
				UIFuBenShengYuTimePanel.Inst.ScaleObj.SetActive(true);
				int residueTimeDay = player.fubenContorl[screenName].ResidueTimeDay;
				if (residueTimeDay > 0)
				{
					int num = residueTimeDay / 360;
					if (num > 2000)
					{
						UIFuBenShengYuTimePanel.Inst.ScaleObj.SetActive(false);
					}
					int num2 = (residueTimeDay - num * 360) / 30;
					int num3 = residueTimeDay - num * 360 - num2 * 30;
					string text;
					if (num > 0)
					{
						text = string.Format("{0}年", num);
					}
					else if (num2 > 0)
					{
						text = string.Format("{0}月", num2);
					}
					else
					{
						text = string.Format("{0}日", num3);
					}
					UIFuBenShengYuTimePanel.Inst.TimeText.text = text;
				}
				else
				{
					if (this.oldHead.isOut)
					{
						return;
					}
					GameObject gameObject = GameObject.Find("OutFuBenTalk");
					if (gameObject != null)
					{
						Flowchart component = gameObject.GetComponent<Flowchart>();
						Block block = component.FindBlock("OutFuBen");
						component.ExecuteBlock(block, 0, delegate()
						{
							AllMapManage.instance.backToLastInFuBenScene.Try();
						});
					}
					else
					{
						AllMapManage.instance.backToLastInFuBenScene.Try();
					}
					this.oldHead.isOut = true;
				}
			}
			else
			{
				UIFuBenShengYuTimePanel.Inst.ScaleObj.SetActive(false);
			}
		}
		if (RandomFuBen.IsInRandomFuBen)
		{
			this.PlaceText.text = (string)player.RandomFuBenList[RandomFuBen.NowRanDomFuBenID.ToString()]["Name"];
		}
		else if (screenName == "S101")
		{
			this.PlaceText.text = DongFuManager.GetDongFuName(DongFuManager.NowDongFuID);
		}
		else
		{
			if (!jsonData.instance.SceneNameJsonData.HasField(screenName))
			{
				return;
			}
			this.PlaceText.text = jsonData.instance.SceneNameJsonData[screenName]["EventName"].Str;
		}
		this.PlaceText.rectTransform.sizeDelta = new Vector2(this.PlaceText.preferredWidth, this.PlaceText.rectTransform.sizeDelta.y);
		this.PlaceImage.rectTransform.anchoredPosition = new Vector2(this.PlaceText.rectTransform.anchoredPosition.x - this.PlaceText.rectTransform.sizeDelta.x - 10f, this.PlaceImage.rectTransform.anchoredPosition.y);
		this.RefreshTaskZhuiZong();
	}

	// Token: 0x060018CF RID: 6351 RVA: 0x000B1FE4 File Offset: 0x000B01E4
	private void RefreshTaskZhuiZong()
	{
		Avatar player = PlayerEx.Player;
		if (player.TaskZhuiZhong.HasField("curTask") && player.TaskZhuiZhong["CurTaskID"].I != -1)
		{
			if (player.TaskZhuiZhong["curType"].I == 1)
			{
				this.RefreshTask();
			}
			else if (player.TaskZhuiZhong["curType"].I == 0)
			{
				this.RefreshChuanWen();
			}
			else
			{
				this.RefreshWeiTuo();
			}
			if (!this.ZhuiZongObj.activeSelf)
			{
				this.ZhuiZongObj.SetActive(true);
			}
			this.TaskTextBG.anchoredPosition = new Vector2(this.TaskTextBG.anchoredPosition.x, -this.TaskText.preferredHeight - 49f);
			return;
		}
		if (this.ZhuiZongObj.activeSelf)
		{
			this.ZhuiZongObj.SetActive(false);
		}
	}

	// Token: 0x060018D0 RID: 6352 RVA: 0x000B20D4 File Offset: 0x000B02D4
	private void RefreshTask()
	{
		Avatar player = PlayerEx.Player;
		this.curTask = player.TaskZhuiZhong["curTask"];
		if (TaskUIManager.checkIsGuoShi(this.curTask))
		{
			player.TaskZhuiZhong.SetField("CurTaskID", -1);
			UIPopTip.Inst.Pop("追踪任务已过期", PopTipIconType.任务进度);
			return;
		}
		this.taskID = this.curTask["id"].I;
		JSONObject jsonobject = jsonData.instance.TaskJsonData[this.taskID.ToString()];
		this.TitleText.text = jsonobject["Name"].Str;
		JSONObject jsonobject2 = player.taskMag._TaskData["Task"].list.Find((JSONObject j) => j["id"].I == this.taskID);
		int nowZiXiangIndex = jsonobject2["NowIndex"].I;
		JSONObject jsonobject3 = jsonData.instance.TaskInfoJsonData.list.Find((JSONObject j) => j["TaskID"].I == this.taskID && j["TaskIndex"].I == nowZiXiangIndex);
		this.TaskText.text = jsonobject3["Desc"].Str.STVarReplace();
		this.TaskIcon.sprite = this.TaskIconSprites[1];
	}

	// Token: 0x060018D1 RID: 6353 RVA: 0x000B2228 File Offset: 0x000B0428
	private void RefreshChuanWen()
	{
		Avatar player = PlayerEx.Player;
		if (player.TaskZhuiZhong["CurisChuanWen"].b)
		{
			this.taskID = player.TaskZhuiZhong["CurTaskID"].I;
			this.curTask = player.TaskZhuiZhong["curTask"];
			if (!player.nomelTaskMag.HasNTask(this.taskID) || TaskUIManager.CheckWeiTuoIsOut(this.curTask))
			{
				player.TaskZhuiZhong.SetField("CurTaskID", -1);
				UIPopTip.Inst.Pop("追踪传闻已过期", PopTipIconType.任务进度);
				return;
			}
			NTaskXiangXi ntaskXiangXiData = player.nomelTaskMag.GetNTaskXiangXiData(this.taskID);
			this.TitleText.text = ntaskXiangXiData.name;
			this.endTime = Tools.GetEndTime(player.NomelTaskJson[this.taskID.ToString()]["StartTime"].str, 0, ntaskXiangXiData.shiXian, 0);
			try
			{
				this.TaskText.text = Tools.TimeToShengYuTime(Tools.getShengYuShiJian(player.worldTimeMag.getNowTime(), this.endTime), "剩余时间：").ToCN();
				goto IL_225;
			}
			catch (Exception)
			{
				player.TaskZhuiZhong.SetField("CurTaskID", -1);
				UIPopTip.Inst.Pop("追踪传闻已过期", PopTipIconType.任务进度);
				return;
			}
		}
		this.curTask = player.TaskZhuiZhong["curTask"];
		if (TaskUIManager.checkIsGuoShi(this.curTask))
		{
			player.TaskZhuiZhong.SetField("CurTaskID", -1);
			UIPopTip.Inst.Pop("追踪传闻已过期", PopTipIconType.任务进度);
			return;
		}
		this.taskID = this.curTask["id"].I;
		string shengYuShiJi = TaskDescManager.getShengYuShiJi(this.curTask);
		if (shengYuShiJi.Contains("年"))
		{
			this.TaskText.text = "剩余时间：" + shengYuShiJi;
		}
		else
		{
			this.TaskText.text = shengYuShiJi.Replace(" ", "");
		}
		this.TitleText.text = TaskJsonData.DataDict[this.curTask["id"].I].Name;
		IL_225:
		this.TaskIcon.sprite = this.TaskIconSprites[0];
	}

	// Token: 0x060018D2 RID: 6354 RVA: 0x000B2484 File Offset: 0x000B0684
	private void RefreshWeiTuo()
	{
		Avatar player = PlayerEx.Player;
		this.taskID = player.TaskZhuiZhong["CurTaskID"].I;
		this.curTask = player.TaskZhuiZhong["curTask"];
		if (!player.nomelTaskMag.HasNTask(this.taskID) || TaskUIManager.CheckWeiTuoIsOut(this.curTask))
		{
			player.TaskZhuiZhong.SetField("CurTaskID", -1);
			UIPopTip.Inst.Pop("追踪委托已过期", PopTipIconType.任务进度);
			return;
		}
		NTaskXiangXi ntaskXiangXiData = player.nomelTaskMag.GetNTaskXiangXiData(this.taskID);
		this.TitleText.text = ntaskXiangXiData.name;
		this.TaskIcon.sprite = this.TaskIconSprites[2];
		int num = 0;
		foreach (JSONObject jsonobject in player.nomelTaskMag.GetNTaskXiangXiList(this.taskID))
		{
			if (!player.nomelTaskMag.XiangXiTaskIsEnd(jsonobject, this.taskID, num))
			{
				int i = player.NomelTaskJson[this.taskID.ToString()]["TaskChild"][num].I;
				NTaskSuiJI ntaskSuiJI = NTaskSuiJI.DataDict[i];
				string text = jsonobject["desc"].str.Replace(jsonobject["TaskID"].str, ntaskSuiJI.name);
				if (jsonobject["Place"].str != "0" && text.Contains(jsonobject["Place"].str))
				{
					int whereChilidID = player.nomelTaskMag.getWhereChilidID(this.taskID, num);
					text = text.Replace(jsonobject["Place"].str, NTaskSuiJI.DataDict[whereChilidID].name);
				}
				string text2 = text.STVarReplace().ToCN();
				this.TaskText.text = text2;
				break;
			}
			num++;
		}
	}

	// Token: 0x060018D3 RID: 6355 RVA: 0x000B26C0 File Offset: 0x000B08C0
	public void OnTaskBtnClick()
	{
		if (Tools.instance.canClick(false, true))
		{
			PanelMamager.inst.OpenPanel(PanelMamager.PanelType.任务, 1);
		}
	}

	// Token: 0x040013E1 RID: 5089
	public static UIMiniTaskPanel Inst;

	// Token: 0x040013E2 RID: 5090
	public GameObject ScaleObj;

	// Token: 0x040013E3 RID: 5091
	public GameObject ZhuiZongObj;

	// Token: 0x040013E4 RID: 5092
	public Text WorldTimeText;

	// Token: 0x040013E5 RID: 5093
	public Text TaskText;

	// Token: 0x040013E6 RID: 5094
	public Text PlaceText;

	// Token: 0x040013E7 RID: 5095
	public Text TitleText;

	// Token: 0x040013E8 RID: 5096
	public Image PlaceImage;

	// Token: 0x040013E9 RID: 5097
	public Image TaskIcon;

	// Token: 0x040013EA RID: 5098
	public RectTransform TaskTextBG;

	// Token: 0x040013EB RID: 5099
	public List<Sprite> TaskIconSprites;

	// Token: 0x040013EC RID: 5100
	private JSONObject curTask;

	// Token: 0x040013ED RID: 5101
	private int taskID;

	// Token: 0x040013EE RID: 5102
	private DateTime endTime;

	// Token: 0x040013EF RID: 5103
	[HideInInspector]
	public headMag oldHead;

	// Token: 0x040013F0 RID: 5104
	private float refreshCD;
}
