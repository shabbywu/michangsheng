using System;
using System.Collections.Generic;
using JSONClass;
using UnityEngine;

// Token: 0x02000298 RID: 664
public class CyPdData
{
	// Token: 0x060017DE RID: 6110 RVA: 0x000A5AE8 File Offset: 0x000A3CE8
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

	// Token: 0x060017DF RID: 6111 RVA: 0x000A5B34 File Offset: 0x000A3D34
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

	// Token: 0x060017E0 RID: 6112 RVA: 0x000A5B80 File Offset: 0x000A3D80
	public bool StaticValuePd()
	{
		int num = GlobalValue.Get(this.staticId, "CyPdData.StaticValuePd");
		return (this.staticFuHao == 1 && num == this.staticValue) || (this.staticFuHao == 2 && num < this.staticValue) || (this.staticFuHao == 3 && num > this.staticValue);
	}

	// Token: 0x060017E1 RID: 6113 RVA: 0x000A5BDB File Offset: 0x000A3DDB
	public bool HaoGanPd(int haogan)
	{
		return (this.haoGanFuHao == 1 && haogan == this.needHaoGanDu) || (this.haoGanFuHao == 2 && haogan < this.needHaoGanDu) || (this.haoGanFuHao == 3 && haogan > this.needHaoGanDu);
	}

	// Token: 0x060017E2 RID: 6114 RVA: 0x000A5C1C File Offset: 0x000A3E1C
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

	// Token: 0x060017E3 RID: 6115 RVA: 0x000A5C9C File Offset: 0x000A3E9C
	public int GetRate(int haogan)
	{
		int num = (haogan - 40) * 100 / 150;
		if (num < 0)
		{
			num = 0;
		}
		return num + this.baseRate;
	}

	// Token: 0x060017E4 RID: 6116 RVA: 0x000A5CC8 File Offset: 0x000A3EC8
	public List<int> GetItem(int npcId)
	{
		List<int> list = new List<int>();
		if (this.id >= 41 && this.id <= 45)
		{
			foreach (int num in CyNpcSendData.DataDict[this.id].RandomItemID)
			{
				if (NpcJieSuanManager.inst.npcUseItem.GetDanYaoCanUseNum(npcId, num) > 0)
				{
					list.Add(num);
					break;
				}
			}
			if (list.Count > 0)
			{
				int i = this.itemPrice;
				int i2 = jsonData.instance.ItemJsonData[list[0].ToString()]["price"].I;
				list.Add(1);
				for (i -= i2; i > 0; i -= i2)
				{
					List<int> list2 = list;
					list2[1] = list2[1] + 1;
				}
			}
			return list;
		}
		int randomInt = NpcJieSuanManager.inst.getRandomInt(0, NpcJieSuanManager.inst.cyDictionary[this.id].Count - 1);
		int randomInt2 = NpcJieSuanManager.inst.getRandomInt(NpcJieSuanManager.inst.cyDictionary[this.id][randomInt][0], NpcJieSuanManager.inst.cyDictionary[this.id][randomInt][1]);
		int j = this.itemPrice;
		int i3 = jsonData.instance.ItemJsonData[randomInt2.ToString()]["price"].I;
		list.Add(randomInt2);
		list.Add(1);
		for (j -= i3; j > 0; j -= i3)
		{
			List<int> list2 = list;
			list2[1] = list2[1] + 1;
		}
		return list;
	}

	// Token: 0x040012A1 RID: 4769
	public int id;

	// Token: 0x040012A2 RID: 4770
	public List<int> npcActionList;

	// Token: 0x040012A3 RID: 4771
	public int npcType;

	// Token: 0x040012A4 RID: 4772
	public int minLevel;

	// Token: 0x040012A5 RID: 4773
	public int maxLevel;

	// Token: 0x040012A6 RID: 4774
	public int staticId;

	// Token: 0x040012A7 RID: 4775
	public int staticFuHao;

	// Token: 0x040012A8 RID: 4776
	public int staticValue;

	// Token: 0x040012A9 RID: 4777
	public int needHaoGanDu;

	// Token: 0x040012AA RID: 4778
	public int haoGanFuHao;

	// Token: 0x040012AB RID: 4779
	public string startTime = "";

	// Token: 0x040012AC RID: 4780
	public string endTime = "";

	// Token: 0x040012AD RID: 4781
	public int npcState;

	// Token: 0x040012AE RID: 4782
	public bool isOnly;

	// Token: 0x040012AF RID: 4783
	public int cyType;

	// Token: 0x040012B0 RID: 4784
	public int baseRate;

	// Token: 0x040012B1 RID: 4785
	public int actionId;

	// Token: 0x040012B2 RID: 4786
	public int itemPrice;

	// Token: 0x040012B3 RID: 4787
	public int outTime;

	// Token: 0x040012B4 RID: 4788
	public int addHaoGan;

	// Token: 0x040012B5 RID: 4789
	public int talkId;

	// Token: 0x040012B6 RID: 4790
	public int qingFen;
}
