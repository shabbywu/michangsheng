using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using script.EventMsg;

public class NPCDeath
{
	public JSONObject npcDeathJson = new JSONObject();

	public List<int> deathTypeList = new List<int> { 1, 2, 3, 4, 5, 10 };

	public void SetNpcDeath(int deathType, int npcId, int killNpcId = 0, bool after = false)
	{
		if (after)
		{
			NpcJieSuanManager.inst.afterDeathList.Add(new List<int> { deathType, npcId, killNpcId });
			return;
		}
		try
		{
			if (jsonData.instance.AvatarJsonData[npcId.ToString()].HasField("BindingNpcID"))
			{
				if (!npcDeathJson.HasField("deathImportantList"))
				{
					npcDeathJson.SetField("deathImportantList", JSONObject.arr);
				}
				npcDeathJson["deathImportantList"].Add(jsonData.instance.AvatarJsonData[npcId.ToString()]["BindingNpcID"].I);
				NpcJieSuanManager.inst.ImportantNpcBangDingDictionary.Remove(jsonData.instance.AvatarJsonData[npcId.ToString()]["BindingNpcID"].I);
			}
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
		}
		if (Tools.instance.getPlayer().emailDateMag.cyNpcList.Contains(npcId) && killNpcId != 0)
		{
			Tools.instance.getPlayer().emailDateMag.AuToSendToPlayer(npcId, 999, 999, NpcJieSuanManager.inst.JieSuanTime);
		}
		JSONObject jSONObject = new JSONObject();
		jSONObject.SetField("deathType", deathType);
		jSONObject.SetField("deathId", npcId);
		jSONObject.SetField("deathName", jsonData.instance.AvatarRandomJsonData[npcId.ToString()]["Name"]);
		jSONObject.SetField("deathChengHao", jsonData.instance.AvatarJsonData[npcId.ToString()]["Title"].str);
		jSONObject.SetField("deathTime", NpcJieSuanManager.inst.JieSuanTime);
		jSONObject.SetField("type", jsonData.instance.AvatarJsonData[npcId.ToString()]["Type"]);
		if (killNpcId != 0)
		{
			jSONObject.SetField("killNpcId", killNpcId);
		}
		npcDeathJson.SetField(npcId.ToString(), jSONObject);
		int i = jsonData.instance.AvatarJsonData[npcId.ToString()]["ChengHaoID"].I;
		if (NpcJieSuanManager.inst.npcChengHao.npcOnlyChengHao.HasField(i.ToString()) && NpcJieSuanManager.inst.npcChengHao.npcOnlyChengHao[i.ToString()].I != 0)
		{
			NpcJieSuanManager.inst.npcChengHao.DeleteOnlyChengHao(i);
		}
		if (npcId >= 20000 && jsonData.instance.AvatarJsonData[npcId.ToString()]["Level"].I >= 7 && deathType == 1)
		{
			EventMag.Inst.SaveEvent(npcId, 11);
		}
		jsonData.instance.AvatarJsonData.RemoveField(npcId.ToString());
		jsonData.instance.AvatarBackpackJsonData.RemoveField(npcId.ToString());
		jsonData.instance.AvatarRandomJsonData.RemoveField(npcId.ToString());
		NpcJieSuanManager.inst.npcMap.RemoveNpcByList(npcId);
		if (NpcJieSuanManager.inst.isCanJieSuan)
		{
			NpcJieSuanManager.inst.isUpDateNpcList = true;
		}
	}

	public bool NpcYiWaiPanDing(int actionId, int npcId)
	{
		JSONObject npcData = NpcJieSuanManager.inst.GetNpcData(npcId);
		Avatar player = Tools.instance.getPlayer();
		if (!jsonData.instance.NpcYiWaiDeathDate.HasField(actionId.ToString()))
		{
			return false;
		}
		if (npcData["isImportant"].b)
		{
			return false;
		}
		int num = 0;
		JSONObject jSONObject = jsonData.instance.NpcYiWaiDeathDate[actionId.ToString()];
		try
		{
			num = (int)jsonData.instance.AvatarRandomJsonData[npcId.ToString()]["HaoGanDu"].n;
		}
		catch (Exception)
		{
			Debug.LogError((object)$"{jsonData.instance.AvatarRandomJsonData.HasField(npcId.ToString())}");
		}
		if (num < jSONObject["HaoGanDu"].I && NpcJieSuanManager.inst.getRandomInt(1, 1000) <= jSONObject["SiWangJiLv"].I)
		{
			NpcJieSuanManager.inst.npcDeath.SetNpcDeath(jSONObject["SiWangLeiXing"].I, npcId);
			return true;
		}
		if (player.deathType == 0)
		{
			player.deathType = deathTypeList[NpcJieSuanManager.inst.getRandomInt(0, deathTypeList.Count - 1)];
		}
		if (num < jSONObject["HaoGanDu"].I && player.fakeTimes >= 6 && npcData["Type"].I == player.deathType)
		{
			Tools.instance.getPlayer().fakeTimes -= 6;
			player.deathType = deathTypeList[NpcJieSuanManager.inst.getRandomInt(0, deathTypeList.Count - 1)];
			NpcJieSuanManager.inst.npcDeath.SetNpcDeath(jSONObject["SiWangLeiXing"].I, npcId);
			return true;
		}
		return false;
	}
}
