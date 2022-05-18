using System;
using Fungus;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000403 RID: 1027
public class UIMiniTaskPanel : MonoBehaviour
{
	// Token: 0x06001BC5 RID: 7109 RVA: 0x000174DA File Offset: 0x000156DA
	private void Awake()
	{
		UIMiniTaskPanel.Inst = this;
	}

	// Token: 0x06001BC6 RID: 7110 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x06001BC7 RID: 7111 RVA: 0x000174E2 File Offset: 0x000156E2
	private void Update()
	{
		if ((!(PanelMamager.inst != null) || !(PanelMamager.inst.UISceneGameObject != null)) && this.ScaleObj.activeInHierarchy)
		{
			this.ScaleObj.SetActive(false);
		}
	}

	// Token: 0x06001BC8 RID: 7112 RVA: 0x000F80C0 File Offset: 0x000F62C0
	public void RefreshUI()
	{
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
		if (player.TaskZhuiZhong.HasField("curTask") && player.TaskZhuiZhong["CurTaskID"].I != -1)
		{
			if (player.TaskZhuiZhong["curType"].I == 1)
			{
				this.curTask = player.TaskZhuiZhong["curTask"];
				if (TaskUIManager.checkIsGuoShi(this.curTask))
				{
					player.TaskZhuiZhong.SetField("CurTaskID", -1);
					UIPopTip.Inst.Pop("追踪任务已过期", PopTipIconType.任务进度);
					return;
				}
				this.TitleText.text = jsonData.instance.TaskJsonData[this.curTask["id"].n.ToString()]["Name"].Str;
				this.taskID = (int)this.curTask["id"].n;
				this.ShengYuTimeText.text = TaskDescManager.getShengYuShiJi(this.curTask);
			}
			else if (player.TaskZhuiZhong["curType"].I == 0)
			{
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
						this.ShengYuTimeText.text = Tools.TimeToShengYuTime(Tools.getShengYuShiJian(player.worldTimeMag.getNowTime(), this.endTime), "").ToCN();
						goto IL_719;
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
				this.ShengYuTimeText.text = TaskDescManager.getShengYuShiJi(this.curTask);
				this.TitleText.text = TaskJsonData.DataDict[this.curTask["id"].I].Name;
			}
			else
			{
				this.taskID = player.TaskZhuiZhong["CurTaskID"].I;
				this.curTask = player.TaskZhuiZhong["curTask"];
				if (!player.nomelTaskMag.HasNTask(this.taskID) || TaskUIManager.CheckWeiTuoIsOut(this.curTask))
				{
					player.TaskZhuiZhong.SetField("CurTaskID", -1);
					UIPopTip.Inst.Pop("追踪委托已过期", PopTipIconType.任务进度);
					return;
				}
				NTaskXiangXi ntaskXiangXiData2 = player.nomelTaskMag.GetNTaskXiangXiData(this.taskID);
				this.endTime = Tools.GetEndTime(player.NomelTaskJson[this.taskID.ToString()]["StartTime"].str, 0, ntaskXiangXiData2.shiXian, 0);
				this.ShengYuTimeText.text = Tools.TimeToShengYuTime(Tools.getShengYuShiJian(player.worldTimeMag.getNowTime(), this.endTime), "").ToCN();
				this.TitleText.text = ntaskXiangXiData2.name;
			}
			IL_719:
			if (!this.ZhuiZongObj.activeSelf)
			{
				this.ZhuiZongObj.SetActive(true);
				return;
			}
		}
		else if (this.ZhuiZongObj.activeSelf)
		{
			this.ZhuiZongObj.SetActive(false);
		}
	}

	// Token: 0x06001BC9 RID: 7113 RVA: 0x0001751C File Offset: 0x0001571C
	public void OnTaskBtnClick()
	{
		if (Tools.instance.canClick(false, true))
		{
			PanelMamager.inst.OpenPanel(PanelMamager.PanelType.任务, 1);
		}
	}

	// Token: 0x04001791 RID: 6033
	public static UIMiniTaskPanel Inst;

	// Token: 0x04001792 RID: 6034
	public GameObject ScaleObj;

	// Token: 0x04001793 RID: 6035
	public GameObject ZhuiZongObj;

	// Token: 0x04001794 RID: 6036
	public Text WorldTimeText;

	// Token: 0x04001795 RID: 6037
	public Text ShengYuTimeText;

	// Token: 0x04001796 RID: 6038
	public Text PlaceText;

	// Token: 0x04001797 RID: 6039
	public Text TitleText;

	// Token: 0x04001798 RID: 6040
	public Image PlaceImage;

	// Token: 0x04001799 RID: 6041
	private JSONObject curTask;

	// Token: 0x0400179A RID: 6042
	private int taskID;

	// Token: 0x0400179B RID: 6043
	private DateTime endTime;

	// Token: 0x0400179C RID: 6044
	[HideInInspector]
	public headMag oldHead;
}
