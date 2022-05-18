using System;

// Token: 0x02000330 RID: 816
public class NpcSetField
{
	// Token: 0x06001835 RID: 6197 RVA: 0x000D7B74 File Offset: 0x000D5D74
	public void AddNpcExp(int npcId, int addNum)
	{
		JSONObject npcData = NpcJieSuanManager.inst.getNpcData(npcId);
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

	// Token: 0x06001836 RID: 6198 RVA: 0x000D7D3C File Offset: 0x000D5F3C
	public void AddNpcLevel(int npcId, int addNum)
	{
		int val = NpcJieSuanManager.inst.getNpcData(npcId)["Level"].I + addNum;
		NpcJieSuanManager.inst.getNpcData(npcId).SetField("Level", val);
	}

	// Token: 0x06001837 RID: 6199 RVA: 0x000D7D7C File Offset: 0x000D5F7C
	public void AddNpcShenShi(int npcId, int addNum)
	{
		int val = NpcJieSuanManager.inst.getNpcData(npcId)["shengShi"].I + addNum;
		NpcJieSuanManager.inst.getNpcData(npcId).SetField("shengShi", val);
	}

	// Token: 0x06001838 RID: 6200 RVA: 0x000D7DBC File Offset: 0x000D5FBC
	public void AddNpcHp(int npcId, int addNum)
	{
		int val = NpcJieSuanManager.inst.getNpcData(npcId)["HP"].I + addNum;
		NpcJieSuanManager.inst.getNpcData(npcId).SetField("HP", val);
	}

	// Token: 0x06001839 RID: 6201 RVA: 0x000D7DFC File Offset: 0x000D5FFC
	public bool AddNpcAge(int npcId, int addNum)
	{
		int val = NpcJieSuanManager.inst.getNpcData(npcId)["age"].I + addNum;
		NpcJieSuanManager.inst.getNpcData(npcId).SetField("age", val);
		if (NpcJieSuanManager.inst.GetNpcShengYuTime(npcId) <= 0)
		{
			NpcJieSuanManager.inst.npcDeath.SetNpcDeath(1, npcId, 0, false);
			return false;
		}
		return true;
	}

	// Token: 0x0600183A RID: 6202 RVA: 0x000D7E60 File Offset: 0x000D6060
	public void AddNpcShouYuan(int npcId, int addNum)
	{
		int val = NpcJieSuanManager.inst.getNpcData(npcId)["shouYuan"].I + addNum;
		NpcJieSuanManager.inst.getNpcData(npcId).SetField("shouYuan", val);
	}

	// Token: 0x0600183B RID: 6203 RVA: 0x000D7EA0 File Offset: 0x000D60A0
	public void AddNpcZhiZi(int npcId, int addNum)
	{
		int num = NpcJieSuanManager.inst.getNpcData(npcId)["ziZhi"].I + addNum;
		if (num > 200)
		{
			num = 200;
		}
		NpcJieSuanManager.inst.getNpcData(npcId).SetField("ziZhi", num);
	}

	// Token: 0x0600183C RID: 6204 RVA: 0x000D7EF0 File Offset: 0x000D60F0
	public void AddNpcWuXing(int npcId, int addNum)
	{
		int num = NpcJieSuanManager.inst.getNpcData(npcId)["wuXin"].I + addNum;
		if (num > 200)
		{
			num = 200;
		}
		NpcJieSuanManager.inst.getNpcData(npcId).SetField("wuXin", num);
	}

	// Token: 0x0600183D RID: 6205 RVA: 0x000D7F40 File Offset: 0x000D6140
	public void AddNpcDunSu(int npcId, int addNum)
	{
		int val = NpcJieSuanManager.inst.getNpcData(npcId)["dunSu"].I + addNum;
		NpcJieSuanManager.inst.getNpcData(npcId).SetField("dunSu", val);
	}

	// Token: 0x0600183E RID: 6206 RVA: 0x000D7F80 File Offset: 0x000D6180
	public void AddNpcMoney(int npcId, int money)
	{
		int num = jsonData.instance.AvatarBackpackJsonData[npcId.ToString()]["money"].I + money;
		if (num < 0)
		{
			num = 0;
		}
		jsonData.instance.AvatarBackpackJsonData[npcId.ToString()].SetField("money", num);
	}

	// Token: 0x0600183F RID: 6207 RVA: 0x000D7FDC File Offset: 0x000D61DC
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

	// Token: 0x06001840 RID: 6208 RVA: 0x000D805C File Offset: 0x000D625C
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

	// Token: 0x06001841 RID: 6209 RVA: 0x000D8184 File Offset: 0x000D6384
	public void AddNpcWuDaoZhi(int npcId, int value)
	{
		JSONObject npcData = NpcJieSuanManager.inst.getNpcData(npcId);
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

	// Token: 0x06001842 RID: 6210 RVA: 0x000D8248 File Offset: 0x000D6448
	public void AddNpcWuDaoDian(int npcId, int num)
	{
		JSONObject npcData = NpcJieSuanManager.inst.getNpcData(npcId);
		int i = npcData["EWWuDaoDian"].I;
		npcData.SetField("EWWuDaoDian", i + num);
	}

	// Token: 0x06001843 RID: 6211 RVA: 0x00015205 File Offset: 0x00013405
	public void SetTag(int npcId, bool tag)
	{
		NpcJieSuanManager.inst.getNpcData(npcId).SetField("IsTag", tag);
	}
}
