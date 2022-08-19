using System;
using JSONClass;
using KBEngine;
using UnityEngine;

// Token: 0x0200038F RID: 911
public class TaskZhuiZhongManager : MonoBehaviour
{
	// Token: 0x06001E0F RID: 7695 RVA: 0x000D43C1 File Offset: 0x000D25C1
	private void Start()
	{
		this.isStart = true;
		this.avatar = Tools.instance.getPlayer();
	}

	// Token: 0x06001E10 RID: 7696 RVA: 0x000D43DC File Offset: 0x000D25DC
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
					this.TextName.text = Tools.Code64(jsonData.instance.TaskJsonData[this.curTask["id"].I.ToString()]["Name"].str);
					this.taskID = this.curTask["id"].I;
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
							goto IL_4B3;
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
					this.taskID = this.curTask["id"].I;
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
				IL_4B3:
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

	// Token: 0x040018A9 RID: 6313
	private bool isStart;

	// Token: 0x040018AA RID: 6314
	private Avatar avatar;

	// Token: 0x040018AB RID: 6315
	[SerializeField]
	private UILabel time;

	// Token: 0x040018AC RID: 6316
	[SerializeField]
	private UILabel TextName;

	// Token: 0x040018AD RID: 6317
	[SerializeField]
	private GameObject ZhuiZhongTask;

	// Token: 0x040018AE RID: 6318
	private int taskID;

	// Token: 0x040018AF RID: 6319
	private DateTime endTime;

	// Token: 0x040018B0 RID: 6320
	private DateTime StarTime;

	// Token: 0x040018B1 RID: 6321
	private int circulation;

	// Token: 0x040018B2 RID: 6322
	private JSONObject curTask;

	// Token: 0x040018B3 RID: 6323
	[SerializeField]
	private GameObject TimeTips;
}
