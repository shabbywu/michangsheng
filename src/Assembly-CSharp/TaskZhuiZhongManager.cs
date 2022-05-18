using System;
using JSONClass;
using KBEngine;
using UnityEngine;

// Token: 0x02000514 RID: 1300
public class TaskZhuiZhongManager : MonoBehaviour
{
	// Token: 0x0600218A RID: 8586 RVA: 0x0001B942 File Offset: 0x00019B42
	private void Start()
	{
		this.isStart = true;
		this.avatar = Tools.instance.getPlayer();
	}

	// Token: 0x0600218B RID: 8587 RVA: 0x00117FE8 File Offset: 0x001161E8
	private void Update()
	{
		if (this.isStart)
		{
			if (this.avatar.TaskZhuiZhong.HasField("curTask") && this.avatar.TaskZhuiZhong["CurTaskID"].I != -1)
			{
				if (this.avatar.TaskZhuiZhong["curType"].I == 1)
				{
					this.curTask = this.avatar.TaskZhuiZhong["curTask"];
					if (TaskUIManager.checkIsGuoShi(this.curTask))
					{
						this.avatar.TaskZhuiZhong.SetField("CurTaskID", -1);
						UIPopTip.Inst.Pop("追踪任务已过期", PopTipIconType.任务进度);
						return;
					}
					this.TextName.text = Tools.Code64(jsonData.instance.TaskJsonData[this.curTask["id"].n.ToString()]["Name"].str);
					this.taskID = (int)this.curTask["id"].n;
					this.time.text = TaskDescManager.getShengYuShiJi(this.curTask);
				}
				else if (this.avatar.TaskZhuiZhong["curType"].I == 0)
				{
					if (this.avatar.TaskZhuiZhong["CurisChuanWen"].b)
					{
						this.taskID = this.avatar.TaskZhuiZhong["CurTaskID"].I;
						this.curTask = this.avatar.TaskZhuiZhong["curTask"];
						if (!this.avatar.nomelTaskMag.HasNTask(this.taskID) || TaskUIManager.CheckWeiTuoIsOut(this.curTask))
						{
							this.avatar.TaskZhuiZhong.SetField("CurTaskID", -1);
							UIPopTip.Inst.Pop("追踪传闻已过期", PopTipIconType.任务进度);
							return;
						}
						NTaskXiangXi ntaskXiangXiData = this.avatar.nomelTaskMag.GetNTaskXiangXiData(this.taskID);
						this.TextName.text = ntaskXiangXiData.name;
						this.endTime = Tools.GetEndTime(this.avatar.NomelTaskJson[this.taskID.ToString()]["StartTime"].str, 0, ntaskXiangXiData.shiXian, 0);
						try
						{
							this.time.text = Tools.TimeToShengYuTime(Tools.getShengYuShiJian(this.avatar.worldTimeMag.getNowTime(), this.endTime), "").ToCN();
							goto IL_4B2;
						}
						catch (Exception)
						{
							this.avatar.TaskZhuiZhong.SetField("CurTaskID", -1);
							UIPopTip.Inst.Pop("追踪传闻已过期", PopTipIconType.任务进度);
							return;
						}
					}
					this.curTask = this.avatar.TaskZhuiZhong["curTask"];
					if (TaskUIManager.checkIsGuoShi(this.curTask))
					{
						this.avatar.TaskZhuiZhong.SetField("CurTaskID", -1);
						UIPopTip.Inst.Pop("追踪传闻已过期", PopTipIconType.任务进度);
						return;
					}
					this.taskID = (int)this.curTask["id"].n;
					this.time.text = TaskDescManager.getShengYuShiJi(this.curTask);
					this.TextName.text = Tools.Code64(jsonData.instance.TaskJsonData[this.curTask["id"].n.ToString()]["Name"].str);
				}
				else
				{
					this.taskID = this.avatar.TaskZhuiZhong["CurTaskID"].I;
					this.curTask = this.avatar.TaskZhuiZhong["curTask"];
					if (!this.avatar.nomelTaskMag.HasNTask(this.taskID) || TaskUIManager.CheckWeiTuoIsOut(this.curTask))
					{
						this.avatar.TaskZhuiZhong.SetField("CurTaskID", -1);
						UIPopTip.Inst.Pop("追踪委托已过期", PopTipIconType.任务进度);
						return;
					}
					NTaskXiangXi ntaskXiangXiData2 = this.avatar.nomelTaskMag.GetNTaskXiangXiData(this.taskID);
					this.endTime = Tools.GetEndTime(this.avatar.NomelTaskJson[this.taskID.ToString()]["StartTime"].str, 0, ntaskXiangXiData2.shiXian, 0);
					this.time.text = Tools.TimeToShengYuTime(Tools.getShengYuShiJian(this.avatar.worldTimeMag.getNowTime(), this.endTime), "").ToCN();
					this.TextName.text = ntaskXiangXiData2.name;
				}
				IL_4B2:
				if (!this.ZhuiZhongTask.activeSelf)
				{
					this.ZhuiZhongTask.SetActive(true);
					return;
				}
			}
			else if (this.ZhuiZhongTask.activeSelf)
			{
				this.ZhuiZhongTask.SetActive(false);
			}
		}
	}

	// Token: 0x04001D09 RID: 7433
	private bool isStart;

	// Token: 0x04001D0A RID: 7434
	private Avatar avatar;

	// Token: 0x04001D0B RID: 7435
	[SerializeField]
	private UILabel time;

	// Token: 0x04001D0C RID: 7436
	[SerializeField]
	private UILabel TextName;

	// Token: 0x04001D0D RID: 7437
	[SerializeField]
	private GameObject ZhuiZhongTask;

	// Token: 0x04001D0E RID: 7438
	private int taskID;

	// Token: 0x04001D0F RID: 7439
	private DateTime endTime;

	// Token: 0x04001D10 RID: 7440
	private DateTime StarTime;

	// Token: 0x04001D11 RID: 7441
	private int circulation;

	// Token: 0x04001D12 RID: 7442
	private JSONObject curTask;

	// Token: 0x04001D13 RID: 7443
	[SerializeField]
	private GameObject TimeTips;
}
