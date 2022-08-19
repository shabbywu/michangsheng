using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200038D RID: 909
public class TaskRenWuCell : MonoBehaviour
{
	// Token: 0x06001DF6 RID: 7670 RVA: 0x000D38DC File Offset: 0x000D1ADC
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
			TaskUIManager.inst.setCurTaskID(this.CurTask["id"].I);
			TaskUIManager.inst.setCurTask(this.CurTask);
			this.taskDescManager.checkIsZhuiZhong();
			return;
		}
		this.DefaultImage.SetActive(true);
		this.TaskName.color = new Color(0.70980394f, 0.94509804f, 0.91764706f);
		this.ClikeImage.SetActive(false);
	}

	// Token: 0x06001DF7 RID: 7671 RVA: 0x000D3A39 File Offset: 0x000D1C39
	public void setTaskName(string name)
	{
		this.TaskName.text = name;
	}

	// Token: 0x06001DF8 RID: 7672 RVA: 0x000D3A47 File Offset: 0x000D1C47
	public void setTaskInfo(JSONObject task)
	{
		this.CurTask = task;
	}

	// Token: 0x06001DF9 RID: 7673 RVA: 0x000D3A50 File Offset: 0x000D1C50
	public void setIsChuanWen(bool flag)
	{
		this.isChuanWen = flag;
	}

	// Token: 0x06001DFA RID: 7674 RVA: 0x000D3A59 File Offset: 0x000D1C59
	public bool getIsChuanWen()
	{
		return this.isChuanWen;
	}

	// Token: 0x06001DFB RID: 7675 RVA: 0x000D3A47 File Offset: 0x000D1C47
	public void setWeTuoInfo(JSONObject WeiTuo)
	{
		this.CurTask = WeiTuo;
	}

	// Token: 0x04001898 RID: 6296
	[SerializeField]
	private GameObject DefaultImage;

	// Token: 0x04001899 RID: 6297
	[SerializeField]
	private GameObject ClikeImage;

	// Token: 0x0400189A RID: 6298
	[SerializeField]
	private Text TaskName;

	// Token: 0x0400189B RID: 6299
	[SerializeField]
	private Toggle toggle;

	// Token: 0x0400189C RID: 6300
	private JSONObject CurTask;

	// Token: 0x0400189D RID: 6301
	[SerializeField]
	private TaskDescManager taskDescManager;

	// Token: 0x0400189E RID: 6302
	private bool isChuanWen;

	// Token: 0x0400189F RID: 6303
	[SerializeField]
	private GameObject ZhuiZhongImage;
}
