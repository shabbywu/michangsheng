using System;
using System.Collections.Generic;
using UnityEngine;
using script.EventMsg;

public class NPCChengHao
{
	private Dictionary<int, List<int>> npcChengHaoDictionary;

	public JSONObject npcOnlyChengHao;

	public NPCChengHao()
	{
		npcChengHaoDictionary = new Dictionary<int, List<int>>();
		npcOnlyChengHao = new JSONObject();
		JSONObject nPCChengHaoData = jsonData.instance.NPCChengHaoData;
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < nPCChengHaoData.Count; i++)
		{
			num = nPCChengHaoData[i]["NPCType"].I;
			num2 = nPCChengHaoData[i]["id"].I;
			if (npcChengHaoDictionary.ContainsKey(num))
			{
				npcChengHaoDictionary[num].Add(num2);
				continue;
			}
			npcChengHaoDictionary.Add(num, new List<int> { num2 });
		}
	}

	public bool IsCanUpToChengHao(int npcId, ref int targetId)
	{
		JSONObject npcData = NpcJieSuanManager.inst.GetNpcData(npcId);
		int i = npcData["ChengHaoID"].I;
		if (jsonData.instance.NPCChengHaoData[i.ToString()]["MaxLevel"].I == 1)
		{
			return false;
		}
		List<int> highLevelChengHaoId = GetHighLevelChengHaoId(i);
		if (highLevelChengHaoId.Count == 0)
		{
			return false;
		}
		int num = 0;
		int i2 = npcData["Level"].I;
		foreach (int item in highLevelChengHaoId)
		{
			if (!npcOnlyChengHao.HasField(item.ToString()) || npcOnlyChengHao[item.ToString()].I == 0)
			{
				JSONObject jSONObject = jsonData.instance.NPCChengHaoData[item.ToString()];
				if (NpcJieSuanManager.inst.IsInScope(i2, jSONObject["Level"][0].I, jSONObject["Level"][1].I) && npcData["GongXian"].I >= jSONObject["GongXian"].I && item > num)
				{
					num = item;
				}
			}
		}
		if (num > 0)
		{
			targetId = num;
			return true;
		}
		return false;
	}

	public void UpDateChengHao(int npcId, int id)
	{
		JSONObject jSONObject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		int i = jSONObject["ChengHaoID"].I;
		try
		{
			if (npcOnlyChengHao.HasField(i.ToString()) && npcOnlyChengHao[i.ToString()].I == npcId)
			{
				DeleteOnlyChengHao(i);
			}
		}
		catch (Exception)
		{
			Debug.LogError((object)$"更换称号出错,称号ID:{i}");
			Debug.LogError((object)$"唯一称号数据:{npcOnlyChengHao}");
		}
		jSONObject.SetField("Title", jsonData.instance.NPCChengHaoData[id.ToString()]["ChengHao"].str.ToCN());
		if (jSONObject["Title"].Str.Contains("大长老"))
		{
			EventMag.Inst.SaveEvent(npcId, 21);
		}
		if (jSONObject["Title"].Str.Contains("掌门"))
		{
			EventMag.Inst.SaveEvent(npcId, 22);
		}
		jSONObject.SetField("ChengHaoID", id);
		if (jsonData.instance.NPCChengHaoData[id.ToString()]["IsOnly"].I == 1)
		{
			npcOnlyChengHao.SetField(id.ToString(), npcId);
		}
	}

	public void DeleteOnlyChengHao(int chengHaoId)
	{
		npcOnlyChengHao.SetField(chengHaoId.ToString(), 0);
	}

	public List<int> GetHighLevelChengHaoId(int chengHaoId)
	{
		int i = jsonData.instance.NPCChengHaoData[chengHaoId.ToString()]["NPCType"].I;
		List<int> list = new List<int>();
		foreach (int item in npcChengHaoDictionary[i])
		{
			if (item > chengHaoId)
			{
				list.Add(item);
			}
		}
		return list;
	}
}
