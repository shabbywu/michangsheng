using System;
using JSONClass;
using KBEngine;
using UnityEngine;

public class chenghaoMag
{
	private Avatar avatar;

	public chenghaoMag(Avatar _avatar)
	{
		avatar = _avatar;
	}

	public void TimeAddMoney(int year)
	{
		if (avatar.menPai != 0)
		{
			int oneYearAddMoney = GetOneYearAddMoney();
			if (oneYearAddMoney > 0)
			{
				AddFengLu(oneYearAddMoney * year);
			}
		}
	}

	public int GetOneYearAddMoney()
	{
		if (avatar.menPai <= 0)
		{
			return 0;
		}
		int result = 0;
		try
		{
			int shengWangNum = (avatar.MenPaiHaoGanDu.HasField(avatar.menPai.ToString()) ? avatar.MenPaiHaoGanDu[avatar.menPai.ToString()].I : 0);
			int shengWangType = GetShengWangType(shengWangNum);
			MenPaiFengLuBiao menPaiFengLuBiao = MenPaiFengLuBiao.DataDict[avatar.chengHao];
			result = menPaiFengLuBiao.money + ((shengWangType != -1 || menPaiFengLuBiao.addMoney.Count <= shengWangType) ? menPaiFengLuBiao.addMoney[shengWangType] : 0);
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
		}
		return result;
	}

	public void AddFengLu(int money)
	{
		avatar.AvatarFengLu.Add(money);
	}

	public void GiveMoney()
	{
		int allFengLuMoney = GetAllFengLuMoney();
		avatar.AddMoney(allFengLuMoney);
		avatar.AvatarFengLu = new JSONObject(JSONObject.Type.ARRAY);
	}

	public int GetAllFengLuMoney()
	{
		int num = 0;
		foreach (JSONObject item in avatar.AvatarFengLu.list)
		{
			num += item.I;
		}
		return num;
	}

	public int GetShengWangType(int ShengWangNum)
	{
		int result = -1;
		int num = 0;
		foreach (int item in MenPaiFengLuBiao.DataDict[avatar.chengHao].haogandu)
		{
			if (ShengWangNum >= item)
			{
				result = num;
				num++;
				continue;
			}
			break;
		}
		return result;
	}
}
