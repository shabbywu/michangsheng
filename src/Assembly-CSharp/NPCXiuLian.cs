using System;
using System.Collections.Generic;

// Token: 0x020003AC RID: 940
public class NPCXiuLian
{
	// Token: 0x06001A18 RID: 6680 RVA: 0x000E6E84 File Offset: 0x000E5084
	public void NpcBiGuan(int npcId)
	{
		if (PlayerEx.IsDaoLv(npcId))
		{
			NpcJieSuanManager.inst.npcTeShu.NpcDaoLuToDongFu(npcId);
		}
		NpcJieSuanManager.inst.npcUseItem.autoUseItem(npcId);
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		int num = jsonobject["xiuLianSpeed"].I * 2;
		if (jsonobject.HasField("JinDanData"))
		{
			float num2 = jsonobject["JinDanData"]["JinDanAddSpeed"].f / 100f;
			num += (int)(num2 * (float)num);
		}
		NpcJieSuanManager.inst.npcSetField.AddNpcExp(npcId, num);
	}

	// Token: 0x06001A19 RID: 6681 RVA: 0x000E6F2C File Offset: 0x000E512C
	public void NpcXiuLianShenTong(int npcID)
	{
		int i = jsonData.instance.AvatarJsonData[npcID.ToString()]["Type"].I;
		int i2 = jsonData.instance.NpcThreeMapBingDate[i.ToString()]["GuangChang"][NpcJieSuanManager.inst.getRandomInt(0, jsonData.instance.NpcThreeMapBingDate[i.ToString()]["GuangChang"].Count - 1)].I;
		NpcJieSuanManager.inst.npcMap.AddNpcToThreeScene(npcID, i2);
	}

	// Token: 0x06001A1A RID: 6682 RVA: 0x000E6FD0 File Offset: 0x000E51D0
	public void NpcLunDao(int npcId)
	{
		int i = jsonData.instance.AvatarJsonData[npcId.ToString()]["Type"].I;
		int i2 = jsonData.instance.NpcThreeMapBingDate[i.ToString()]["GuangChang"][NpcJieSuanManager.inst.getRandomInt(0, jsonData.instance.NpcThreeMapBingDate[i.ToString()]["GuangChang"].Count - 1)].I;
		NpcJieSuanManager.inst.npcMap.AddNpcToThreeScene(npcId, i2);
		NpcJieSuanManager.inst.lunDaoNpcList.Add(npcId);
	}

	// Token: 0x06001A1B RID: 6683 RVA: 0x000E7084 File Offset: 0x000E5284
	public void NextNpcLunDao()
	{
		for (int i = NpcJieSuanManager.inst.lunDaoNpcList.Count - 1; i >= 0; i--)
		{
			if (NpcJieSuanManager.inst.npcDeath.npcDeathJson.HasField(NpcJieSuanManager.inst.lunDaoNpcList[i].ToString()))
			{
				NpcJieSuanManager.inst.lunDaoNpcList.RemoveAt(i);
			}
		}
		if (NpcJieSuanManager.inst.lunDaoNpcList.Count < 2)
		{
			return;
		}
		while (NpcJieSuanManager.inst.lunDaoNpcList.Count >= 2)
		{
			int randomInt = NpcJieSuanManager.inst.getRandomInt(0, NpcJieSuanManager.inst.lunDaoNpcList.Count - 1);
			int npcId = NpcJieSuanManager.inst.lunDaoNpcList[randomInt];
			NpcJieSuanManager.inst.lunDaoNpcList.RemoveAt(randomInt);
			randomInt = NpcJieSuanManager.inst.getRandomInt(0, NpcJieSuanManager.inst.lunDaoNpcList.Count - 1);
			int npcId2 = NpcJieSuanManager.inst.lunDaoNpcList[randomInt];
			NpcJieSuanManager.inst.lunDaoNpcList.RemoveAt(randomInt);
			if (NpcJieSuanManager.inst.getRandomInt(1, 100) < 10)
			{
				NpcJieSuanManager.inst.npcNoteBook.NoteLunDaoFail(npcId, jsonData.instance.AvatarRandomJsonData[npcId2.ToString()]["Name"].str);
				NpcJieSuanManager.inst.npcNoteBook.NoteLunDaoFail(npcId2, jsonData.instance.AvatarRandomJsonData[npcId.ToString()]["Name"].str);
			}
			else
			{
				float num = jsonData.instance.AvatarJsonData[npcId.ToString()]["wuXin"].f / 100f;
				float num2 = jsonData.instance.AvatarJsonData[npcId2.ToString()]["wuXin"].f / 100f;
				JSONObject jsonobject = jsonData.instance.WuDaoZhiData[jsonData.instance.AvatarJsonData[npcId.ToString()]["WuDaoValueLevel"].I.ToString()];
				NpcJieSuanManager.inst.npcSetField.AddNpcWuDaoZhi(npcId, jsonobject["LevelUpExp"].I / jsonobject["LevelUpNum"].I);
				JSONObject jsonobject2 = jsonData.instance.WuDaoZhiData[jsonData.instance.AvatarJsonData[npcId2.ToString()]["WuDaoValueLevel"].I.ToString()];
				NpcJieSuanManager.inst.npcSetField.AddNpcWuDaoZhi(npcId2, jsonobject2["LevelUpExp"].I / jsonobject2["LevelUpNum"].I);
				int addExp = (int)((float)jsonData.instance.NpcLevelShouYiDate[jsonData.instance.AvatarJsonData[npcId.ToString()]["Level"].I.ToString()]["wudaoexp"].I * (1f + num));
				int addExp2 = (int)((float)jsonData.instance.NpcLevelShouYiDate[jsonData.instance.AvatarJsonData[npcId2.ToString()]["Level"].I.ToString()]["wudaoexp"].I * (1f + num2));
				int randomInt2 = NpcJieSuanManager.inst.getRandomInt(1, 10);
				NpcJieSuanManager.inst.npcSetField.AddNpcWuDaoExp(npcId, randomInt2, addExp);
				NpcJieSuanManager.inst.npcSetField.AddNpcWuDaoExp(npcId2, randomInt2, addExp2);
				NpcJieSuanManager.inst.npcNoteBook.NoteLunDaoSuccess(npcId, jsonData.instance.AvatarRandomJsonData[npcId2.ToString()]["Name"].str);
				NpcJieSuanManager.inst.npcNoteBook.NoteLunDaoSuccess(npcId2, jsonData.instance.AvatarRandomJsonData[npcId.ToString()]["Name"].str);
			}
		}
		NpcJieSuanManager.inst.lunDaoNpcList = new List<int>();
	}

	// Token: 0x06001A1C RID: 6684 RVA: 0x000DBFA0 File Offset: 0x000DA1A0
	public bool IsCanNomalTuPo(int npcID = -1, JSONObject actionDate = null)
	{
		JSONObject jsonobject = new JSONObject();
		if (npcID == -1)
		{
			jsonobject = actionDate;
		}
		else
		{
			jsonobject = jsonData.instance.AvatarJsonData[npcID.ToString()];
		}
		int i = jsonobject["exp"].I;
		int num = jsonobject["Level"].I + 1;
		return i >= jsonobject["NextExp"].I && jsonobject["NextExp"].I != 0 && num != 4 && num != 7 && num != 10 && num != 13 && num != 16;
	}
}
