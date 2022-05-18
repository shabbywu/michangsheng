using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003C8 RID: 968
public class CyPdData
{
	// Token: 0x06001ABE RID: 6846 RVA: 0x000ECAA4 File Offset: 0x000EACA4
	public void SetStaticFuHao(string msg)
	{
		if (msg == "=")
		{
			this.staticFuHao = 1;
			return;
		}
		if (msg == "<")
		{
			this.staticFuHao = 2;
			return;
		}
		if (!(msg == ">"))
		{
			return;
		}
		this.staticFuHao = 3;
	}

	// Token: 0x06001ABF RID: 6847 RVA: 0x000ECAF0 File Offset: 0x000EACF0
	public void SetHaoGanFuHao(string msg)
	{
		if (msg == "=")
		{
			this.haoGanFuHao = 1;
			return;
		}
		if (msg == "<")
		{
			this.haoGanFuHao = 2;
			return;
		}
		if (!(msg == ">"))
		{
			return;
		}
		this.haoGanFuHao = 3;
	}

	// Token: 0x06001AC0 RID: 6848 RVA: 0x000ECB3C File Offset: 0x000EAD3C
	public bool StaticValuePd()
	{
		int num = GlobalValue.Get(this.staticId, "CyPdData.StaticValuePd");
		return (this.staticFuHao == 1 && num == this.staticValue) || (this.staticFuHao == 2 && num < this.staticValue) || (this.staticFuHao == 3 && num > this.staticValue);
	}

	// Token: 0x06001AC1 RID: 6849 RVA: 0x00016B38 File Offset: 0x00014D38
	public bool HaoGanPd(int haogan)
	{
		return (this.haoGanFuHao == 1 && haogan == this.needHaoGanDu) || (this.haoGanFuHao == 2 && haogan < this.needHaoGanDu) || (this.haoGanFuHao == 3 && haogan > this.needHaoGanDu);
	}

	// Token: 0x06001AC2 RID: 6850 RVA: 0x000ECB98 File Offset: 0x000EAD98
	public bool IsinTime()
	{
		if (this.startTime == "")
		{
			return true;
		}
		try
		{
			DateTime t = DateTime.Parse(NpcJieSuanManager.inst.JieSuanTime);
			DateTime t2 = DateTime.Parse(this.startTime);
			DateTime t3 = DateTime.Parse(this.endTime);
			if (t >= t2 && t <= t3)
			{
				return true;
			}
		}
		catch (Exception ex)
		{
			Debug.LogError(ex);
			return false;
		}
		return false;
	}

	// Token: 0x06001AC3 RID: 6851 RVA: 0x000ECC18 File Offset: 0x000EAE18
	public int GetRate(int haogan)
	{
		int num = (haogan - 40) * 100 / 150;
		if (num < 0)
		{
			num = 0;
		}
		return num + this.baseRate;
	}

	// Token: 0x06001AC4 RID: 6852 RVA: 0x000ECC44 File Offset: 0x000EAE44
	public List<int> GetItem()
	{
		List<int> list = new List<int>();
		int randomInt = NpcJieSuanManager.inst.getRandomInt(0, NpcJieSuanManager.inst.cyDictionary[this.id].Count - 1);
		int randomInt2 = NpcJieSuanManager.inst.getRandomInt(NpcJieSuanManager.inst.cyDictionary[this.id][randomInt][0], NpcJieSuanManager.inst.cyDictionary[this.id][randomInt][1]);
		int i = this.itemPrice;
		int i2 = jsonData.instance.ItemJsonData[randomInt2.ToString()]["price"].I;
		list.Add(randomInt2);
		list.Add(1);
		for (i -= i2; i > 0; i -= i2)
		{
			List<int> list2 = list;
			list2[1] = list2[1] + 1;
		}
		return list;
	}

	// Token: 0x0400162A RID: 5674
	public int id;

	// Token: 0x0400162B RID: 5675
	public List<int> npcActionList;

	// Token: 0x0400162C RID: 5676
	public int npcType;

	// Token: 0x0400162D RID: 5677
	public int minLevel;

	// Token: 0x0400162E RID: 5678
	public int maxLevel;

	// Token: 0x0400162F RID: 5679
	public int staticId;

	// Token: 0x04001630 RID: 5680
	public int staticFuHao;

	// Token: 0x04001631 RID: 5681
	public int staticValue;

	// Token: 0x04001632 RID: 5682
	public int needHaoGanDu;

	// Token: 0x04001633 RID: 5683
	public int haoGanFuHao;

	// Token: 0x04001634 RID: 5684
	public string startTime = "";

	// Token: 0x04001635 RID: 5685
	public string endTime = "";

	// Token: 0x04001636 RID: 5686
	public int npcState;

	// Token: 0x04001637 RID: 5687
	public bool isOnly;

	// Token: 0x04001638 RID: 5688
	public int cyType;

	// Token: 0x04001639 RID: 5689
	public int baseRate;

	// Token: 0x0400163A RID: 5690
	public int actionId;

	// Token: 0x0400163B RID: 5691
	public int itemPrice;

	// Token: 0x0400163C RID: 5692
	public int outTime;

	// Token: 0x0400163D RID: 5693
	public int addHaoGan;

	// Token: 0x0400163E RID: 5694
	public int talkId;

	// Token: 0x0400163F RID: 5695
	public int qingFen;
}
