using System;
using System.Collections.Generic;
using script.EventMsg;
using UnityEngine;

// Token: 0x020001F9 RID: 505
public class NPCChengHao
{
	// Token: 0x0600149E RID: 5278 RVA: 0x00084138 File Offset: 0x00082338
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

	// Token: 0x0600149F RID: 5279 RVA: 0x000841EC File Offset: 0x000823EC
	public bool IsCanUpToChengHao(int npcId, ref int targetId)
	{
		JSONObject npcData = NpcJieSuanManager.inst.GetNpcData(npcId);
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

	// Token: 0x060014A0 RID: 5280 RVA: 0x00084360 File Offset: 0x00082560
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
		if (jsonobject["Title"].Str.Contains("大长老"))
		{
			EventMag.Inst.SaveEvent(npcId, 21);
		}
		if (jsonobject["Title"].Str.Contains("掌门"))
		{
			EventMag.Inst.SaveEvent(npcId, 22);
		}
		jsonobject.SetField("ChengHaoID", id);
		if (jsonData.instance.NPCChengHaoData[id.ToString()]["IsOnly"].I == 1)
		{
			this.npcOnlyChengHao.SetField(id.ToString(), npcId);
		}
	}

	// Token: 0x060014A1 RID: 5281 RVA: 0x000844D8 File Offset: 0x000826D8
	public void DeleteOnlyChengHao(int chengHaoId)
	{
		this.npcOnlyChengHao.SetField(chengHaoId.ToString(), 0);
	}

	// Token: 0x060014A2 RID: 5282 RVA: 0x000844F0 File Offset: 0x000826F0
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

	// Token: 0x04000F69 RID: 3945
	private Dictionary<int, List<int>> npcChengHaoDictionary;

	// Token: 0x04000F6A RID: 3946
	public JSONObject npcOnlyChengHao;
}
