using System;
using JSONClass;
using KBEngine;
using UnityEngine;

// Token: 0x02000544 RID: 1348
public class chenghaoMag
{
	// Token: 0x06002267 RID: 8807 RVA: 0x0001C347 File Offset: 0x0001A547
	public chenghaoMag(Avatar _avatar)
	{
		this.avatar = _avatar;
	}

	// Token: 0x06002268 RID: 8808 RVA: 0x0011B7BC File Offset: 0x001199BC
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

	// Token: 0x06002269 RID: 8809 RVA: 0x0011B7EC File Offset: 0x001199EC
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
			MenPaiFengLuBiao menPaiFengLuBiao = MenPaiFengLuBiao.DataDict[this.avatar.chengHao - 1];
			result = menPaiFengLuBiao.money + ((shengWangType == -1 && menPaiFengLuBiao.addMoney.Count > shengWangType) ? 0 : menPaiFengLuBiao.addMoney[shengWangType]);
		}
		catch (Exception ex)
		{
			Debug.LogError(ex);
		}
		return result;
	}

	// Token: 0x0600226A RID: 8810 RVA: 0x0001C356 File Offset: 0x0001A556
	public void AddFengLu(int money)
	{
		this.avatar.AvatarFengLu.Add(money);
	}

	// Token: 0x0600226B RID: 8811 RVA: 0x0011B8BC File Offset: 0x00119ABC
	public void GiveMoney()
	{
		int allFengLuMoney = this.GetAllFengLuMoney();
		this.avatar.AddMoney(allFengLuMoney);
		this.avatar.AvatarFengLu = new JSONObject(JSONObject.Type.ARRAY);
	}

	// Token: 0x0600226C RID: 8812 RVA: 0x0011B8F0 File Offset: 0x00119AF0
	public int GetAllFengLuMoney()
	{
		int num = 0;
		foreach (JSONObject jsonobject in this.avatar.AvatarFengLu.list)
		{
			num += jsonobject.I;
		}
		return num;
	}

	// Token: 0x0600226D RID: 8813 RVA: 0x0011B954 File Offset: 0x00119B54
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

	// Token: 0x04001DB8 RID: 7608
	private Avatar avatar;
}
