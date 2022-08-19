using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200032D RID: 813
public class MainUISetLinGen : MonoBehaviour
{
	// Token: 0x06001C05 RID: 7173 RVA: 0x000C8BDC File Offset: 0x000C6DDC
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

	// Token: 0x06001C06 RID: 7174 RVA: 0x000C8CF8 File Offset: 0x000C6EF8
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

	// Token: 0x06001C07 RID: 7175 RVA: 0x000C8E2D File Offset: 0x000C702D
	public bool CheckLingGen()
	{
		return base.gameObject.activeSelf && this.hasSelectLinst.Count != MainUIPlayerInfo.inst.linggenNum;
	}

	// Token: 0x06001C08 RID: 7176 RVA: 0x000C8E58 File Offset: 0x000C7058
	private void UpdataPrecent()
	{
		int num = MainUIPlayerInfo.inst.lingGenList[0] + MainUIPlayerInfo.inst.lingGenList[1] + MainUIPlayerInfo.inst.lingGenList[2] + MainUIPlayerInfo.inst.lingGenList[3] + MainUIPlayerInfo.inst.lingGenList[4];
		for (int i = 0; i < 5; i++)
		{
			this.lingGenPrecentList[i].text = MainUIPlayerInfo.inst.lingGenList[i] * 100 / num + "%";
		}
	}

	// Token: 0x040016A1 RID: 5793
	public List<MainUILinGenCell> lingGensList;

	// Token: 0x040016A2 RID: 5794
	public List<MainUILinGenCell> hasSelectLinst;

	// Token: 0x040016A3 RID: 5795
	public List<Text> lingGenPrecentList;

	// Token: 0x040016A4 RID: 5796
	public Text tips;

	// Token: 0x040016A5 RID: 5797
	private bool isInit;
}
