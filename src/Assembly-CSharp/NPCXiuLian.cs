using System;
using System.Collections.Generic;
using UnityEngine;

public class NPCXiuLian
{
	public void NpcBiGuan(int npcId)
	{
		if (PlayerEx.IsDaoLv(npcId))
		{
			NpcJieSuanManager.inst.npcTeShu.NpcDaoLuToDongFu(npcId);
		}
		NpcJieSuanManager.inst.npcUseItem.autoUseItem(npcId);
		JSONObject jSONObject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		int num = jSONObject["xiuLianSpeed"].I * 2;
		if (jSONObject.HasField("JinDanData"))
		{
			float num2 = jSONObject["JinDanData"]["JinDanAddSpeed"].f / 100f;
			num += (int)(num2 * (float)num);
		}
		NpcJieSuanManager.inst.npcSetField.AddNpcExp(npcId, num);
	}

	public void NpcXiuLianShenTong(int npcID)
	{
		int i = jsonData.instance.AvatarJsonData[npcID.ToString()]["Type"].I;
		int i2 = jsonData.instance.NpcThreeMapBingDate[i.ToString()]["GuangChang"][NpcJieSuanManager.inst.getRandomInt(0, jsonData.instance.NpcThreeMapBingDate[i.ToString()]["GuangChang"].Count - 1)].I;
		NpcJieSuanManager.inst.npcMap.AddNpcToThreeScene(npcID, i2);
	}

	public void NpcLunDao(int npcId)
	{
		try
		{
			int i = jsonData.instance.AvatarJsonData[npcId.ToString()]["Type"].I;
			int i2 = jsonData.instance.NpcThreeMapBingDate[i.ToString()]["GuangChang"][NpcJieSuanManager.inst.getRandomInt(0, jsonData.instance.NpcThreeMapBingDate[i.ToString()]["GuangChang"].Count - 1)].I;
			NpcJieSuanManager.inst.npcMap.AddNpcToThreeScene(npcId, i2);
			NpcJieSuanManager.inst.lunDaoNpcList.Add(npcId);
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
			throw;
		}
	}

	public void NextNpcLunDao()
	{
		for (int num = NpcJieSuanManager.inst.lunDaoNpcList.Count - 1; num >= 0; num--)
		{
			int num2 = NpcJieSuanManager.inst.lunDaoNpcList[num];
			if (NpcJieSuanManager.inst.npcDeath.npcDeathJson.HasField(num2.ToString()))
			{
				NpcJieSuanManager.inst.lunDaoNpcList.RemoveAt(num);
			}
			if (!jsonData.instance.AvatarJsonData.HasField(num2.ToString()))
			{
				NpcJieSuanManager.inst.lunDaoNpcList.RemoveAt(num);
			}
		}
		if (NpcJieSuanManager.inst.lunDaoNpcList.Count < 2)
		{
			return;
		}
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		float num6 = 0f;
		float num7 = 0f;
		while (NpcJieSuanManager.inst.lunDaoNpcList.Count >= 2)
		{
			num3 = NpcJieSuanManager.inst.getRandomInt(0, NpcJieSuanManager.inst.lunDaoNpcList.Count - 1);
			num4 = NpcJieSuanManager.inst.lunDaoNpcList[num3];
			NpcJieSuanManager.inst.lunDaoNpcList.RemoveAt(num3);
			num3 = NpcJieSuanManager.inst.getRandomInt(0, NpcJieSuanManager.inst.lunDaoNpcList.Count - 1);
			num5 = NpcJieSuanManager.inst.lunDaoNpcList[num3];
			NpcJieSuanManager.inst.lunDaoNpcList.RemoveAt(num3);
			if (NpcJieSuanManager.inst.getRandomInt(1, 100) < 10)
			{
				NpcJieSuanManager.inst.npcNoteBook.NoteLunDaoFail(num4, jsonData.instance.AvatarRandomJsonData[num5.ToString()]["Name"].str);
				NpcJieSuanManager.inst.npcNoteBook.NoteLunDaoFail(num5, jsonData.instance.AvatarRandomJsonData[num4.ToString()]["Name"].str);
				continue;
			}
			try
			{
				num6 = jsonData.instance.AvatarJsonData[num4.ToString()]["wuXin"].f / 100f;
				num7 = jsonData.instance.AvatarJsonData[num5.ToString()]["wuXin"].f / 100f;
				JSONObject jSONObject = jsonData.instance.WuDaoZhiData[jsonData.instance.AvatarJsonData[num4.ToString()]["WuDaoValueLevel"].I.ToString()];
				NpcJieSuanManager.inst.npcSetField.AddNpcWuDaoZhi(num4, jSONObject["LevelUpExp"].I / jSONObject["LevelUpNum"].I);
				JSONObject jSONObject2 = jsonData.instance.WuDaoZhiData[jsonData.instance.AvatarJsonData[num5.ToString()]["WuDaoValueLevel"].I.ToString()];
				NpcJieSuanManager.inst.npcSetField.AddNpcWuDaoZhi(num5, jSONObject2["LevelUpExp"].I / jSONObject2["LevelUpNum"].I);
				int addExp = (int)((float)jsonData.instance.NpcLevelShouYiDate[jsonData.instance.AvatarJsonData[num4.ToString()]["Level"].I.ToString()]["wudaoexp"].I * (1f + num6));
				int addExp2 = (int)((float)jsonData.instance.NpcLevelShouYiDate[jsonData.instance.AvatarJsonData[num5.ToString()]["Level"].I.ToString()]["wudaoexp"].I * (1f + num7));
				int randomInt = NpcJieSuanManager.inst.getRandomInt(1, 10);
				NpcJieSuanManager.inst.npcSetField.AddNpcWuDaoExp(num4, randomInt, addExp);
				NpcJieSuanManager.inst.npcSetField.AddNpcWuDaoExp(num5, randomInt, addExp2);
				NpcJieSuanManager.inst.npcNoteBook.NoteLunDaoSuccess(num4, jsonData.instance.AvatarRandomJsonData[num5.ToString()]["Name"].str);
				NpcJieSuanManager.inst.npcNoteBook.NoteLunDaoSuccess(num5, jsonData.instance.AvatarRandomJsonData[num4.ToString()]["Name"].str);
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
			}
		}
		NpcJieSuanManager.inst.lunDaoNpcList = new List<int>();
	}

	public bool IsCanNomalTuPo(int npcID = -1, JSONObject actionDate = null)
	{
		JSONObject jSONObject = new JSONObject();
		jSONObject = ((npcID != -1) ? jsonData.instance.AvatarJsonData[npcID.ToString()] : actionDate);
		int i = jSONObject["exp"].I;
		int num = jSONObject["Level"].I + 1;
		if (i >= jSONObject["NextExp"].I && jSONObject["NextExp"].I != 0 && num != 4 && num != 7 && num != 10 && num != 13 && num != 16)
		{
			return true;
		}
		return false;
	}
}
