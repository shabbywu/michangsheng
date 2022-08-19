using System;

// Token: 0x02000216 RID: 534
public class NpcSetField
{
	// Token: 0x06001581 RID: 5505 RVA: 0x0008EF58 File Offset: 0x0008D158
	public void AddNpcExp(int npcId, int addNum)
	{
		JSONObject npcData = NpcJieSuanManager.inst.GetNpcData(npcId);
		int num = npcData["exp"].I + addNum;
		if (num >= 144411000)
		{
			num = 144411000;
		}
		if (npcData["Level"].I % 3 == 0 && num >= jsonData.instance.LevelUpDataJsonData[npcData["Level"].I.ToString()]["MaxExp"].I)
		{
			num = jsonData.instance.LevelUpDataJsonData[npcData["Level"].I.ToString()]["MaxExp"].I;
		}
		npcData.SetField("exp", num);
		if (NpcJieSuanManager.inst.npcTuPo.IsCanSmallTuPo(npcId, null))
		{
			NpcJieSuanManager.inst.npcTuPo.NpcSmallToPo(npcData, npcData["Level"].I + 1);
			NpcJieSuanManager.inst.UpdateNpcWuDao(npcId);
			int id = 0;
			if (NpcJieSuanManager.inst.npcChengHao.IsCanUpToChengHao(npcId, ref id))
			{
				NpcJieSuanManager.inst.npcChengHao.UpDateChengHao(npcId, id);
			}
		}
		if (npcData["exp"].I >= npcData["NextExp"].I && npcData["NextExp"].I != 0 && (npcData["Level"].I == 3 || npcData["Level"].I == 6 || npcData["Level"].I == 9 || npcData["Level"].I == 12))
		{
			NpcJieSuanManager.inst.npcStatus.SetNpcStatus(npcId, 2);
		}
	}

	// Token: 0x06001582 RID: 5506 RVA: 0x0008F120 File Offset: 0x0008D320
	public void AddNpcLevel(int npcId, int addNum)
	{
		int val = NpcJieSuanManager.inst.GetNpcData(npcId)["Level"].I + addNum;
		NpcJieSuanManager.inst.GetNpcData(npcId).SetField("Level", val);
	}

	// Token: 0x06001583 RID: 5507 RVA: 0x0008F160 File Offset: 0x0008D360
	public void AddNpcShenShi(int npcId, int addNum)
	{
		int val = NpcJieSuanManager.inst.GetNpcData(npcId)["shengShi"].I + addNum;
		NpcJieSuanManager.inst.GetNpcData(npcId).SetField("shengShi", val);
	}

	// Token: 0x06001584 RID: 5508 RVA: 0x0008F1A0 File Offset: 0x0008D3A0
	public void AddNpcHp(int npcId, int addNum)
	{
		int val = NpcJieSuanManager.inst.GetNpcData(npcId)["HP"].I + addNum;
		NpcJieSuanManager.inst.GetNpcData(npcId).SetField("HP", val);
	}

	// Token: 0x06001585 RID: 5509 RVA: 0x0008F1E0 File Offset: 0x0008D3E0
	public bool AddNpcAge(int npcId, int addNum)
	{
		int val = NpcJieSuanManager.inst.GetNpcData(npcId)["age"].I + addNum;
		NpcJieSuanManager.inst.GetNpcData(npcId).SetField("age", val);
		if (NpcJieSuanManager.inst.GetNpcShengYuTime(npcId) <= 0)
		{
			NpcJieSuanManager.inst.npcDeath.SetNpcDeath(1, npcId, 0, false);
			return false;
		}
		return true;
	}

	// Token: 0x06001586 RID: 5510 RVA: 0x0008F244 File Offset: 0x0008D444
	public void AddNpcShouYuan(int npcId, int addNum)
	{
		int val = NpcJieSuanManager.inst.GetNpcData(npcId)["shouYuan"].I + addNum;
		NpcJieSuanManager.inst.GetNpcData(npcId).SetField("shouYuan", val);
	}

	// Token: 0x06001587 RID: 5511 RVA: 0x0008F284 File Offset: 0x0008D484
	public void AddNpcZhiZi(int npcId, int addNum)
	{
		int num = NpcJieSuanManager.inst.GetNpcData(npcId)["ziZhi"].I + addNum;
		if (num > 200)
		{
			num = 200;
		}
		NpcJieSuanManager.inst.GetNpcData(npcId).SetField("ziZhi", num);
	}

	// Token: 0x06001588 RID: 5512 RVA: 0x0008F2D4 File Offset: 0x0008D4D4
	public void AddNpcWuXing(int npcId, int addNum)
	{
		int num = NpcJieSuanManager.inst.GetNpcData(npcId)["wuXin"].I + addNum;
		if (num > 200)
		{
			num = 200;
		}
		NpcJieSuanManager.inst.GetNpcData(npcId).SetField("wuXin", num);
	}

	// Token: 0x06001589 RID: 5513 RVA: 0x0008F324 File Offset: 0x0008D524
	public void AddNpcDunSu(int npcId, int addNum)
	{
		int val = NpcJieSuanManager.inst.GetNpcData(npcId)["dunSu"].I + addNum;
		NpcJieSuanManager.inst.GetNpcData(npcId).SetField("dunSu", val);
	}

	// Token: 0x0600158A RID: 5514 RVA: 0x0008F364 File Offset: 0x0008D564
	public void AddNpcMoney(int npcId, int money)
	{
		int num = jsonData.instance.AvatarBackpackJsonData[npcId.ToString()]["money"].I + money;
		if (num < 0)
		{
			num = 0;
		}
		jsonData.instance.AvatarBackpackJsonData[npcId.ToString()].SetField("money", num);
	}

	// Token: 0x0600158B RID: 5515 RVA: 0x0008F3C0 File Offset: 0x0008D5C0
	public void NpcAddGongXian(int npcId, int addNum)
	{
		int i = jsonData.instance.AvatarJsonData[npcId.ToString()]["GongXian"].I;
		jsonData.instance.AvatarJsonData[npcId.ToString()].SetField("GongXian", i + addNum);
		int id = 0;
		if (NpcJieSuanManager.inst.npcChengHao.IsCanUpToChengHao(npcId, ref id))
		{
			NpcJieSuanManager.inst.npcChengHao.UpDateChengHao(npcId, id);
		}
	}

	// Token: 0x0600158C RID: 5516 RVA: 0x0008F440 File Offset: 0x0008D640
	public void AddNpcWuDaoExp(int npcId, int wudaoId, int addExp)
	{
		int num = jsonData.instance.AvatarJsonData[npcId.ToString()]["wuDaoJson"][wudaoId.ToString()]["level"].I;
		int num2 = jsonData.instance.AvatarJsonData[npcId.ToString()]["wuDaoJson"][wudaoId.ToString()]["exp"].I;
		int i = jsonData.instance.WuDaoJinJieJson[num.ToString()]["Max"].I;
		num2 += addExp;
		if (num2 >= i)
		{
			num++;
		}
		jsonData.instance.AvatarJsonData[npcId.ToString()]["wuDaoJson"][wudaoId.ToString()].SetField("level", num);
		jsonData.instance.AvatarJsonData[npcId.ToString()]["wuDaoJson"][wudaoId.ToString()].SetField("exp", num2);
	}

	// Token: 0x0600158D RID: 5517 RVA: 0x0008F568 File Offset: 0x0008D768
	public void AddNpcWuDaoZhi(int npcId, int value)
	{
		JSONObject npcData = NpcJieSuanManager.inst.GetNpcData(npcId);
		int i = npcData["WuDaoValue"].I;
		int i2 = npcData["EWWuDaoDian"].I;
		npcData.SetField("WuDaoValue", i + value);
		int num = npcData["WuDaoValueLevel"].I;
		int j = i + value;
		int i3 = jsonData.instance.WuDaoZhiData[num.ToString()]["LevelUpExp"].I;
		while (j > i3)
		{
			num++;
			if (!jsonData.instance.WuDaoZhiData.HasField(num.ToString()))
			{
				return;
			}
			npcData.SetField("WuDaoValueLevel", num);
			j -= i3;
			this.AddNpcWuDaoDian(npcId, 1);
		}
	}

	// Token: 0x0600158E RID: 5518 RVA: 0x0008F62C File Offset: 0x0008D82C
	public void AddNpcWuDaoDian(int npcId, int num)
	{
		JSONObject npcData = NpcJieSuanManager.inst.GetNpcData(npcId);
		int i = npcData["EWWuDaoDian"].I;
		npcData.SetField("EWWuDaoDian", i + num);
	}

	// Token: 0x0600158F RID: 5519 RVA: 0x0008F662 File Offset: 0x0008D862
	public void SetTag(int npcId, bool tag)
	{
		NpcJieSuanManager.inst.GetNpcData(npcId).SetField("IsTag", tag);
	}
}
