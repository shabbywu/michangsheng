public class NpcSetField
{
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
		if (NpcJieSuanManager.inst.npcTuPo.IsCanSmallTuPo(npcId))
		{
			NpcJieSuanManager.inst.npcTuPo.NpcSmallToPo(npcData, npcData["Level"].I + 1);
			NpcJieSuanManager.inst.UpdateNpcWuDao(npcId);
			int targetId = 0;
			if (NpcJieSuanManager.inst.npcChengHao.IsCanUpToChengHao(npcId, ref targetId))
			{
				NpcJieSuanManager.inst.npcChengHao.UpDateChengHao(npcId, targetId);
			}
		}
		if (npcData["exp"].I >= npcData["NextExp"].I && npcData["NextExp"].I != 0 && (npcData["Level"].I == 3 || npcData["Level"].I == 6 || npcData["Level"].I == 9 || npcData["Level"].I == 12))
		{
			NpcJieSuanManager.inst.npcStatus.SetNpcStatus(npcId, 2);
		}
	}

	public void AddNpcLevel(int npcId, int addNum)
	{
		int val = NpcJieSuanManager.inst.GetNpcData(npcId)["Level"].I + addNum;
		NpcJieSuanManager.inst.GetNpcData(npcId).SetField("Level", val);
	}

	public void AddNpcShenShi(int npcId, int addNum)
	{
		int val = NpcJieSuanManager.inst.GetNpcData(npcId)["shengShi"].I + addNum;
		NpcJieSuanManager.inst.GetNpcData(npcId).SetField("shengShi", val);
	}

	public void AddNpcHp(int npcId, int addNum)
	{
		int val = NpcJieSuanManager.inst.GetNpcData(npcId)["HP"].I + addNum;
		NpcJieSuanManager.inst.GetNpcData(npcId).SetField("HP", val);
	}

	public bool AddNpcAge(int npcId, int addNum)
	{
		int val = NpcJieSuanManager.inst.GetNpcData(npcId)["age"].I + addNum;
		NpcJieSuanManager.inst.GetNpcData(npcId).SetField("age", val);
		if (NpcJieSuanManager.inst.GetNpcShengYuTime(npcId) <= 0)
		{
			NpcJieSuanManager.inst.npcDeath.SetNpcDeath(1, npcId);
			return false;
		}
		return true;
	}

	public void AddNpcShouYuan(int npcId, int addNum)
	{
		int val = NpcJieSuanManager.inst.GetNpcData(npcId)["shouYuan"].I + addNum;
		NpcJieSuanManager.inst.GetNpcData(npcId).SetField("shouYuan", val);
	}

	public void AddNpcZhiZi(int npcId, int addNum)
	{
		int num = NpcJieSuanManager.inst.GetNpcData(npcId)["ziZhi"].I + addNum;
		if (num > 200)
		{
			num = 200;
		}
		NpcJieSuanManager.inst.GetNpcData(npcId).SetField("ziZhi", num);
	}

	public void AddNpcWuXing(int npcId, int addNum)
	{
		int num = NpcJieSuanManager.inst.GetNpcData(npcId)["wuXin"].I + addNum;
		if (num > 200)
		{
			num = 200;
		}
		NpcJieSuanManager.inst.GetNpcData(npcId).SetField("wuXin", num);
	}

	public void AddNpcDunSu(int npcId, int addNum)
	{
		int val = NpcJieSuanManager.inst.GetNpcData(npcId)["dunSu"].I + addNum;
		NpcJieSuanManager.inst.GetNpcData(npcId).SetField("dunSu", val);
	}

	public void AddNpcMoney(int npcId, int money)
	{
		int num = jsonData.instance.AvatarBackpackJsonData[npcId.ToString()]["money"].I + money;
		if (num < 0)
		{
			num = 0;
		}
		jsonData.instance.AvatarBackpackJsonData[npcId.ToString()].SetField("money", num);
	}

	public void NpcAddGongXian(int npcId, int addNum)
	{
		int i = jsonData.instance.AvatarJsonData[npcId.ToString()]["GongXian"].I;
		jsonData.instance.AvatarJsonData[npcId.ToString()].SetField("GongXian", i + addNum);
		int targetId = 0;
		if (NpcJieSuanManager.inst.npcChengHao.IsCanUpToChengHao(npcId, ref targetId))
		{
			NpcJieSuanManager.inst.npcChengHao.UpDateChengHao(npcId, targetId);
		}
	}

	public void AddNpcWuDaoExp(int npcId, int wudaoId, int addExp)
	{
		int num = jsonData.instance.AvatarJsonData[npcId.ToString()]["wuDaoJson"][wudaoId.ToString()]["level"].I;
		int i = jsonData.instance.AvatarJsonData[npcId.ToString()]["wuDaoJson"][wudaoId.ToString()]["exp"].I;
		int i2 = jsonData.instance.WuDaoJinJieJson[num.ToString()]["Max"].I;
		i += addExp;
		if (i >= i2)
		{
			num++;
		}
		jsonData.instance.AvatarJsonData[npcId.ToString()]["wuDaoJson"][wudaoId.ToString()].SetField("level", num);
		jsonData.instance.AvatarJsonData[npcId.ToString()]["wuDaoJson"][wudaoId.ToString()].SetField("exp", i);
	}

	public void AddNpcWuDaoZhi(int npcId, int value)
	{
		JSONObject npcData = NpcJieSuanManager.inst.GetNpcData(npcId);
		int i = npcData["WuDaoValue"].I;
		_ = npcData["EWWuDaoDian"].I;
		npcData.SetField("WuDaoValue", i + value);
		int num = npcData["WuDaoValueLevel"].I;
		int num2 = i + value;
		int i2 = jsonData.instance.WuDaoZhiData[num.ToString()]["LevelUpExp"].I;
		while (num2 > i2)
		{
			num++;
			if (jsonData.instance.WuDaoZhiData.HasField(num.ToString()))
			{
				npcData.SetField("WuDaoValueLevel", num);
				num2 -= i2;
				AddNpcWuDaoDian(npcId, 1);
				continue;
			}
			break;
		}
	}

	public void AddNpcWuDaoDian(int npcId, int num)
	{
		JSONObject npcData = NpcJieSuanManager.inst.GetNpcData(npcId);
		int i = npcData["EWWuDaoDian"].I;
		npcData.SetField("EWWuDaoDian", i + num);
	}

	public void SetTag(int npcId, bool tag)
	{
		NpcJieSuanManager.inst.GetNpcData(npcId).SetField("IsTag", tag);
	}
}
