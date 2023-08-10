using System;
using System.Collections.Generic;
using Fungus;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

public class UIMiniTaskPanel : MonoBehaviour
{
	public static UIMiniTaskPanel Inst;

	public GameObject ScaleObj;

	public GameObject ZhuiZongObj;

	public Text WorldTimeText;

	public Text TaskText;

	public Text PlaceText;

	public Text TitleText;

	public Image PlaceImage;

	public Image TaskIcon;

	public RectTransform TaskTextBG;

	public List<Sprite> TaskIconSprites;

	private JSONObject curTask;

	private int taskID;

	private DateTime endTime;

	[HideInInspector]
	public headMag oldHead;

	private float refreshCD;

	private void Awake()
	{
		Inst = this;
	}

	private void Start()
	{
	}

	private void Update()
	{
		if ((!((Object)(object)PanelMamager.inst != (Object)null) || !((Object)(object)PanelMamager.inst.UISceneGameObject != (Object)null)) && ScaleObj.activeInHierarchy)
		{
			ScaleObj.SetActive(false);
		}
	}

	public void RefreshUI()
	{
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		refreshCD -= Time.deltaTime;
		if (!(refreshCD < 0f))
		{
			return;
		}
		refreshCD = 1f;
		Avatar player = Tools.instance.getPlayer();
		if (player == null)
		{
			return;
		}
		DateTime nowTime = player.worldTimeMag.getNowTime();
		WorldTimeText.text = $"{nowTime.Year}年{nowTime.Month}月{nowTime.Day}日";
		string screenName = Tools.getScreenName();
		if ((Object)(object)UIFuBenShengYuTimePanel.Inst != (Object)null)
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
					string text = ((num > 0) ? $"{num}年" : ((num2 <= 0) ? $"{num3}日" : $"{num2}月"));
					UIFuBenShengYuTimePanel.Inst.TimeText.text = text;
				}
				else
				{
					if (oldHead.isOut)
					{
						return;
					}
					GameObject val = GameObject.Find("OutFuBenTalk");
					if ((Object)(object)val != (Object)null)
					{
						Flowchart component = val.GetComponent<Flowchart>();
						Block block = component.FindBlock("OutFuBen");
						component.ExecuteBlock(block, 0, delegate
						{
							AllMapManage.instance.backToLastInFuBenScene.Try();
						});
					}
					else
					{
						AllMapManage.instance.backToLastInFuBenScene.Try();
					}
					oldHead.isOut = true;
				}
			}
			else
			{
				UIFuBenShengYuTimePanel.Inst.ScaleObj.SetActive(false);
			}
		}
		if (RandomFuBen.IsInRandomFuBen)
		{
			PlaceText.text = (string)player.RandomFuBenList[RandomFuBen.NowRanDomFuBenID.ToString()][(object)"Name"];
		}
		else if (screenName == "S101")
		{
			PlaceText.text = DongFuManager.GetDongFuName(DongFuManager.NowDongFuID);
		}
		else
		{
			if (!jsonData.instance.SceneNameJsonData.HasField(screenName))
			{
				return;
			}
			PlaceText.text = jsonData.instance.SceneNameJsonData[screenName]["EventName"].Str;
		}
		((Graphic)PlaceText).rectTransform.sizeDelta = new Vector2(PlaceText.preferredWidth, ((Graphic)PlaceText).rectTransform.sizeDelta.y);
		((Graphic)PlaceImage).rectTransform.anchoredPosition = new Vector2(((Graphic)PlaceText).rectTransform.anchoredPosition.x - ((Graphic)PlaceText).rectTransform.sizeDelta.x - 10f, ((Graphic)PlaceImage).rectTransform.anchoredPosition.y);
		RefreshTaskZhuiZong();
	}

	private void RefreshTaskZhuiZong()
	{
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		Avatar player = PlayerEx.Player;
		if (player.TaskZhuiZhong.HasField("curTask") && player.TaskZhuiZhong["CurTaskID"].I != -1)
		{
			if (player.TaskZhuiZhong["curType"].I == 1)
			{
				RefreshTask();
			}
			else if (player.TaskZhuiZhong["curType"].I == 0)
			{
				RefreshChuanWen();
			}
			else
			{
				RefreshWeiTuo();
			}
			if (!ZhuiZongObj.activeSelf)
			{
				ZhuiZongObj.SetActive(true);
			}
			TaskTextBG.anchoredPosition = new Vector2(TaskTextBG.anchoredPosition.x, 0f - TaskText.preferredHeight - 49f);
		}
		else if (ZhuiZongObj.activeSelf)
		{
			ZhuiZongObj.SetActive(false);
		}
	}

	private void RefreshTask()
	{
		Avatar player = PlayerEx.Player;
		curTask = player.TaskZhuiZhong["curTask"];
		if (TaskUIManager.checkIsGuoShi(curTask))
		{
			player.TaskZhuiZhong.SetField("CurTaskID", -1);
			UIPopTip.Inst.Pop("追踪任务已过期", PopTipIconType.任务进度);
			return;
		}
		taskID = curTask["id"].I;
		JSONObject jSONObject = jsonData.instance.TaskJsonData[taskID.ToString()];
		TitleText.text = jSONObject["Name"].Str;
		JSONObject jSONObject2 = player.taskMag._TaskData["Task"].list.Find((JSONObject j) => j["id"].I == taskID);
		int nowZiXiangIndex = jSONObject2["NowIndex"].I;
		JSONObject jSONObject3 = jsonData.instance.TaskInfoJsonData.list.Find((JSONObject j) => j["TaskID"].I == taskID && j["TaskIndex"].I == nowZiXiangIndex);
		TaskText.text = jSONObject3["Desc"].Str.STVarReplace();
		TaskIcon.sprite = TaskIconSprites[1];
	}

	private void RefreshChuanWen()
	{
		Avatar player = PlayerEx.Player;
		if (player.TaskZhuiZhong["CurisChuanWen"].b)
		{
			taskID = player.TaskZhuiZhong["CurTaskID"].I;
			curTask = player.TaskZhuiZhong["curTask"];
			if (!player.nomelTaskMag.HasNTask(taskID) || TaskUIManager.CheckWeiTuoIsOut(curTask))
			{
				player.TaskZhuiZhong.SetField("CurTaskID", -1);
				UIPopTip.Inst.Pop("追踪传闻已过期", PopTipIconType.任务进度);
				return;
			}
			NTaskXiangXi nTaskXiangXiData = player.nomelTaskMag.GetNTaskXiangXiData(taskID);
			TitleText.text = nTaskXiangXiData.name;
			endTime = Tools.GetEndTime(player.NomelTaskJson[taskID.ToString()]["StartTime"].str, 0, nTaskXiangXiData.shiXian);
			try
			{
				TaskText.text = Tools.TimeToShengYuTime(Tools.getShengYuShiJian(player.worldTimeMag.getNowTime(), endTime), "剩余时间：").ToCN();
			}
			catch (Exception)
			{
				player.TaskZhuiZhong.SetField("CurTaskID", -1);
				UIPopTip.Inst.Pop("追踪传闻已过期", PopTipIconType.任务进度);
				return;
			}
		}
		else
		{
			curTask = player.TaskZhuiZhong["curTask"];
			if (TaskUIManager.checkIsGuoShi(curTask))
			{
				player.TaskZhuiZhong.SetField("CurTaskID", -1);
				UIPopTip.Inst.Pop("追踪传闻已过期", PopTipIconType.任务进度);
				return;
			}
			taskID = curTask["id"].I;
			string shengYuShiJi = TaskDescManager.getShengYuShiJi(curTask);
			if (shengYuShiJi.Contains("年"))
			{
				TaskText.text = "剩余时间：" + shengYuShiJi;
			}
			else
			{
				TaskText.text = shengYuShiJi.Replace(" ", "");
			}
			TitleText.text = TaskJsonData.DataDict[curTask["id"].I].Name;
		}
		TaskIcon.sprite = TaskIconSprites[0];
	}

	private void RefreshWeiTuo()
	{
		Avatar player = PlayerEx.Player;
		taskID = player.TaskZhuiZhong["CurTaskID"].I;
		curTask = player.TaskZhuiZhong["curTask"];
		if (!player.nomelTaskMag.HasNTask(taskID) || TaskUIManager.CheckWeiTuoIsOut(curTask))
		{
			player.TaskZhuiZhong.SetField("CurTaskID", -1);
			UIPopTip.Inst.Pop("追踪委托已过期", PopTipIconType.任务进度);
			return;
		}
		NTaskXiangXi nTaskXiangXiData = player.nomelTaskMag.GetNTaskXiangXiData(taskID);
		TitleText.text = nTaskXiangXiData.name;
		TaskIcon.sprite = TaskIconSprites[2];
		int num = 0;
		foreach (JSONObject nTaskXiangXi in player.nomelTaskMag.GetNTaskXiangXiList(taskID))
		{
			if (player.nomelTaskMag.XiangXiTaskIsEnd(nTaskXiangXi, taskID, num))
			{
				num++;
				continue;
			}
			int i = player.NomelTaskJson[taskID.ToString()]["TaskChild"][num].I;
			NTaskSuiJI nTaskSuiJI = NTaskSuiJI.DataDict[i];
			string text = nTaskXiangXi["desc"].str.Replace(nTaskXiangXi["TaskID"].str, nTaskSuiJI.name);
			if (nTaskXiangXi["Place"].str != "0" && text.Contains(nTaskXiangXi["Place"].str))
			{
				int whereChilidID = player.nomelTaskMag.getWhereChilidID(taskID, num);
				text = text.Replace(nTaskXiangXi["Place"].str, NTaskSuiJI.DataDict[whereChilidID].name);
			}
			string text2 = text.STVarReplace().ToCN();
			TaskText.text = text2;
			break;
		}
	}

	public void OnTaskBtnClick()
	{
		if (Tools.instance.canClick())
		{
			PanelMamager.inst.OpenPanel(PanelMamager.PanelType.任务, 1);
		}
	}
}
