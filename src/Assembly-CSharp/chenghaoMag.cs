using System;
using JSONClass;
using KBEngine;
using UnityEngine;

// Token: 0x020003B8 RID: 952
public class chenghaoMag
{
	// Token: 0x06001EE9 RID: 7913 RVA: 0x000D8731 File Offset: 0x000D6931
	public chenghaoMag(Avatar _avatar)
	{
		this.avatar = _avatar;
	}

	// Token: 0x06001EEA RID: 7914 RVA: 0x000D8740 File Offset: 0x000D6940
	public void TimeAddMoney(int year)
	{
		if (this.avatar.menPai == 0)
		{
			return;
		}
		int oneYearAddMoney = this.GetOneYearAddMoney();
		if (oneYearAddMoney > 0)
		{
			this.AddFengLu(oneYearAddMoney * year);
		}
	}

	// Token: 0x06001EEB RID: 7915 RVA: 0x000D8770 File Offset: 0x000D6970
	public int GetOneYearAddMoney()
	{
		if (this.avatar.menPai <= 0)
		{
			return 0;
		}
		int result = 0;
		try
		{
			int shengWangNum = (!this.avatar.MenPaiHaoGanDu.HasField(this.avatar.menPai.ToString())) ? 0 : this.avatar.MenPaiHaoGanDu[this.avatar.menPai.ToString()].I;
			int shengWangType = this.GetShengWangType(shengWangNum);
			MenPaiFengLuBiao menPaiFengLuBiao = MenPaiFengLuBiao.DataDict[this.avatar.chengHao];
			result = menPaiFengLuBiao.money + ((shengWangType == -1 && menPaiFengLuBiao.addMoney.Count > shengWangType) ? 0 : menPaiFengLuBiao.addMoney[shengWangType]);
		}
		catch (Exception ex)
		{
			Debug.LogError(ex);
		}
		return result;
	}

	// Token: 0x06001EEC RID: 7916 RVA: 0x000D883C File Offset: 0x000D6A3C
	public void AddFengLu(int money)
	{
		this.avatar.AvatarFengLu.Add(money);
	}

	// Token: 0x06001EED RID: 7917 RVA: 0x000D8850 File Offset: 0x000D6A50
	public void GiveMoney()
	{
		int allFengLuMoney = this.GetAllFengLuMoney();
		this.avatar.AddMoney(allFengLuMoney);
		this.avatar.AvatarFengLu = new JSONObject(JSONObject.Type.ARRAY);
	}

	// Token: 0x06001EEE RID: 7918 RVA: 0x000D8884 File Offset: 0x000D6A84
	public int GetAllFengLuMoney()
	{
		int num = 0;
		foreach (JSONObject jsonobject in this.avatar.AvatarFengLu.list)
		{
			num += jsonobject.I;
		}
		return num;
	}

	// Token: 0x06001EEF RID: 7919 RVA: 0x000D88E8 File Offset: 0x000D6AE8
	public int GetShengWangType(int ShengWangNum)
	{
		int result = -1;
		int num = 0;
		foreach (int num2 in MenPaiFengLuBiao.DataDict[this.avatar.chengHao].haogandu)
		{
			if (ShengWangNum < num2)
			{
				break;
			}
			result = num;
			num++;
		}
		return result;
	}

	// Token: 0x0400194B RID: 6475
	private Avatar avatar;
}
