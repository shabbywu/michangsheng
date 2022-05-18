using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200030E RID: 782
public class NPCChengHao
{
	// Token: 0x06001748 RID: 5960 RVA: 0x000CCDA0 File Offset: 0x000CAFA0
	public NPCChengHao()
	{
		this.npcChengHaoDictionary = new Dictionary<int, List<int>>();
		this.npcOnlyChengHao = new JSONObject();
		JSONObject npcchengHaoData = jsonData.instance.NPCChengHaoData;
		for (int i = 0; i < npcchengHaoData.Count; i++)
		{
			int i2 = npcchengHaoData[i]["NPCType"].I;
			int i3 = npcchengHaoData[i]["id"].I;
			if (this.npcChengHaoDictionary.ContainsKey(i2))
			{
				this.npcChengHaoDictionary[i2].Add(i3);
			}
			else
			{
				this.npcChengHaoDictionary.Add(i2, new List<int>
				{
					i3
				});
			}
		}
	}

	// Token: 0x06001749 RID: 5961 RVA: 0x000CCE54 File Offset: 0x000CB054
	public bool IsCanUpToChengHao(int npcId, ref int targetId)
	{
		JSONObject npcData = NpcJieSuanManager.inst.getNpcData(npcId);
		int i = npcData["ChengHaoID"].I;
		if (jsonData.instance.NPCChengHaoData[i.ToString()]["MaxLevel"].I == 1)
		{
			return false;
		}
		List<int> highLevelChengHaoId = this.GetHighLevelChengHaoId(i);
		if (highLevelChengHaoId.Count == 0)
		{
			return false;
		}
		int num = 0;
		int i2 = npcData["Level"].I;
		foreach (int num2 in highLevelChengHaoId)
		{
			if (!this.npcOnlyChengHao.HasField(num2.ToString()) || this.npcOnlyChengHao[num2.ToString()].I == 0)
			{
				JSONObject jsonobject = jsonData.instance.NPCChengHaoData[num2.ToString()];
				if (NpcJieSuanManager.inst.IsInScope(i2, jsonobject["Level"][0].I, jsonobject["Level"][1].I) && npcData["GongXian"].I >= jsonobject["GongXian"].I && num2 > num)
				{
					num = num2;
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

	// Token: 0x0600174A RID: 5962 RVA: 0x000CCFC8 File Offset: 0x000CB1C8
	public void UpDateChengHao(int npcId, int id)
	{
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		int i = jsonobject["ChengHaoID"].I;
		try
		{
			if (this.npcOnlyChengHao.HasField(i.ToString()) && this.npcOnlyChengHao[i.ToString()].I == npcId)
			{
				this.DeleteOnlyChengHao(i);
			}
		}
		catch (Exception)
		{
			Debug.LogError(string.Format("更换称号出错,称号ID:{0}", i));
			Debug.LogError(string.Format("唯一称号数据:{0}", this.npcOnlyChengHao));
		}
		jsonobject.SetField("Title", jsonData.instance.NPCChengHaoData[id.ToString()]["ChengHao"].str.ToCN());
		jsonobject.SetField("ChengHaoID", id);
		if (jsonData.instance.NPCChengHaoData[id.ToString()]["IsOnly"].I == 1)
		{
			this.npcOnlyChengHao.SetField(id.ToString(), npcId);
		}
	}

	// Token: 0x0600174B RID: 5963 RVA: 0x0001491A File Offset: 0x00012B1A
	public void DeleteOnlyChengHao(int chengHaoId)
	{
		this.npcOnlyChengHao.SetField(chengHaoId.ToString(), 0);
	}

	// Token: 0x0600174C RID: 5964 RVA: 0x000CD0F0 File Offset: 0x000CB2F0
	public List<int> GetHighLevelChengHaoId(int chengHaoId)
	{
		int i = jsonData.instance.NPCChengHaoData[chengHaoId.ToString()]["NPCType"].I;
		List<int> list = new List<int>();
		foreach (int num in this.npcChengHaoDictionary[i])
		{
			if (num > chengHaoId)
			{
				list.Add(num);
			}
		}
		return list;
	}

	// Token: 0x040012AF RID: 4783
	private Dictionary<int, List<int>> npcChengHaoDictionary;

	// Token: 0x040012B0 RID: 4784
	public JSONObject npcOnlyChengHao;
}
