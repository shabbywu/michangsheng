using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200049B RID: 1179
public class MainUISetLinGen : MonoBehaviour
{
	// Token: 0x06001F57 RID: 8023 RVA: 0x0010E124 File Offset: 0x0010C324
	public void Init()
	{
		if (!this.isInit)
		{
			for (int i = 0; i < this.lingGensList.Count; i++)
			{
				this.lingGensList[i].clickEvent = new UnityAction<int>(this.ClickMethod);
			}
			this.isInit = true;
		}
		this.tips.text = "共选择" + MainUIPlayerInfo.inst.linggenNum.ToCNNumber() + "个灵根";
		int num = MainUIPlayerInfo.inst.linggenNum - this.hasSelectLinst.Count;
		if (num > 0)
		{
			for (int j = 0; j < this.lingGensList.Count; j++)
			{
				if (!this.lingGensList[j].isOn)
				{
					this.lingGensList[j].OnPointerUp(null);
					num--;
				}
				if (num <= 0)
				{
					break;
				}
			}
		}
		else if (num < 0)
		{
			for (int k = this.hasSelectLinst.Count - 1; k >= 0; k--)
			{
				this.hasSelectLinst[k].OnPointerUp(null);
				num++;
				if (num >= 0)
				{
					break;
				}
			}
		}
		base.gameObject.SetActive(true);
	}

	// Token: 0x06001F58 RID: 8024 RVA: 0x0010E240 File Offset: 0x0010C440
	public void ClickMethod(int i)
	{
		if (this.lingGensList[i].isOn)
		{
			if (this.hasSelectLinst.Count < MainUIPlayerInfo.inst.linggenNum)
			{
				this.hasSelectLinst.Add(this.lingGensList[i]);
				List<int> lingGenList = MainUIPlayerInfo.inst.lingGenList;
				lingGenList[i] += 10;
			}
			else if (this.hasSelectLinst.Count == MainUIPlayerInfo.inst.linggenNum)
			{
				this.hasSelectLinst[0].isOn = false;
				this.hasSelectLinst[0].OnvalueChange();
				this.hasSelectLinst.Add(this.lingGensList[i]);
				List<int> lingGenList = MainUIPlayerInfo.inst.lingGenList;
				lingGenList[i] += 10;
			}
			else
			{
				this.lingGensList[i].isOn = false;
			}
		}
		else
		{
			this.hasSelectLinst.Remove(this.lingGensList[i]);
			List<int> lingGenList = MainUIPlayerInfo.inst.lingGenList;
			lingGenList[i] -= 10;
		}
		this.UpdataPrecent();
	}

	// Token: 0x06001F59 RID: 8025 RVA: 0x00019E23 File Offset: 0x00018023
	public bool CheckLingGen()
	{
		return base.gameObject.activeSelf && this.hasSelectLinst.Count != MainUIPlayerInfo.inst.linggenNum;
	}

	// Token: 0x06001F5A RID: 8026 RVA: 0x0010E378 File Offset: 0x0010C578
	private void UpdataPrecent()
	{
		int num = MainUIPlayerInfo.inst.lingGenList[0] + MainUIPlayerInfo.inst.lingGenList[1] + MainUIPlayerInfo.inst.lingGenList[2] + MainUIPlayerInfo.inst.lingGenList[3] + MainUIPlayerInfo.inst.lingGenList[4];
		for (int i = 0; i < 5; i++)
		{
			this.lingGenPrecentList[i].text = MainUIPlayerInfo.inst.lingGenList[i] * 100 / num + "%";
		}
	}

	// Token: 0x04001AD7 RID: 6871
	public List<MainUILinGenCell> lingGensList;

	// Token: 0x04001AD8 RID: 6872
	public List<MainUILinGenCell> hasSelectLinst;

	// Token: 0x04001AD9 RID: 6873
	public List<Text> lingGenPrecentList;

	// Token: 0x04001ADA RID: 6874
	public Text tips;

	// Token: 0x04001ADB RID: 6875
	private bool isInit;
}
