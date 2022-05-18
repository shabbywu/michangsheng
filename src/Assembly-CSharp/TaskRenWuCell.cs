using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000512 RID: 1298
public class TaskRenWuCell : MonoBehaviour
{
	// Token: 0x06002171 RID: 8561 RVA: 0x00117538 File Offset: 0x00115738
	public void ClickImage()
	{
		if (this.toggle.isOn)
		{
			this.DefaultImage.SetActive(false);
			this.TaskName.color = new Color(0.94509804f, 0.85882354f, 0.6627451f);
			this.ClikeImage.SetActive(true);
			if (TaskUIManager.inst.getCurType() == 1)
			{
				this.taskDescManager.setCurTaskDesc(this.CurTask);
				TaskUIManager.inst.setCurSelectIsChuanWen(false);
			}
			else if (TaskUIManager.inst.getCurType() == 0)
			{
				if (this.getIsChuanWen())
				{
					this.taskDescManager.setChuanWenWenTuo(this.CurTask);
					TaskUIManager.inst.setCurSelectIsChuanWen(true);
				}
				else
				{
					this.taskDescManager.setChuanMiaoShu(this.CurTask);
					TaskUIManager.inst.setCurSelectIsChuanWen(false);
				}
			}
			else
			{
				this.taskDescManager.setWeiTuoDesc(this.CurTask);
				TaskUIManager.inst.setCurSelectIsChuanWen(false);
			}
			TaskUIManager.inst.setCurTaskID((int)this.CurTask["id"].n);
			TaskUIManager.inst.setCurTask(this.CurTask);
			this.taskDescManager.checkIsZhuiZhong();
			return;
		}
		this.DefaultImage.SetActive(true);
		this.TaskName.color = new Color(0.70980394f, 0.94509804f, 0.91764706f);
		this.ClikeImage.SetActive(false);
	}

	// Token: 0x06002172 RID: 8562 RVA: 0x0001B897 File Offset: 0x00019A97
	public void setTaskName(string name)
	{
		this.TaskName.text = name;
	}

	// Token: 0x06002173 RID: 8563 RVA: 0x0001B8A5 File Offset: 0x00019AA5
	public void setTaskInfo(JSONObject task)
	{
		this.CurTask = task;
	}

	// Token: 0x06002174 RID: 8564 RVA: 0x0001B8AE File Offset: 0x00019AAE
	public void setIsChuanWen(bool flag)
	{
		this.isChuanWen = flag;
	}

	// Token: 0x06002175 RID: 8565 RVA: 0x0001B8B7 File Offset: 0x00019AB7
	public bool getIsChuanWen()
	{
		return this.isChuanWen;
	}

	// Token: 0x06002176 RID: 8566 RVA: 0x0001B8A5 File Offset: 0x00019AA5
	public void setWeTuoInfo(JSONObject WeiTuo)
	{
		this.CurTask = WeiTuo;
	}

	// Token: 0x04001CF8 RID: 7416
	[SerializeField]
	private GameObject DefaultImage;

	// Token: 0x04001CF9 RID: 7417
	[SerializeField]
	private GameObject ClikeImage;

	// Token: 0x04001CFA RID: 7418
	[SerializeField]
	private Text TaskName;

	// Token: 0x04001CFB RID: 7419
	[SerializeField]
	private Toggle toggle;

	// Token: 0x04001CFC RID: 7420
	private JSONObject CurTask;

	// Token: 0x04001CFD RID: 7421
	[SerializeField]
	private TaskDescManager taskDescManager;

	// Token: 0x04001CFE RID: 7422
	private bool isChuanWen;

	// Token: 0x04001CFF RID: 7423
	[SerializeField]
	private GameObject ZhuiZhongImage;
}
