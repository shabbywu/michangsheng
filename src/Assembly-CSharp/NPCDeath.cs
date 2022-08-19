using System;
using System.Collections.Generic;
using KBEngine;
using script.EventMsg;
using UnityEngine;

// Token: 0x020001FA RID: 506
public class NPCDeath
{
	// Token: 0x060014A3 RID: 5283 RVA: 0x0008457C File Offset: 0x0008277C
	public void SetNpcDeath(int deathType, int npcId, int killNpcId = 0, bool after = false)
	{
		if (after)
		{
			NpcJieSuanManager.inst.afterDeathList.Add(new List<int>
			{
				deathType,
				npcId,
				killNpcId
			});
			return;
		}
		try
		{
			if (jsonData.instance.AvatarJsonData[npcId.ToString()].HasField("BindingNpcID"))
			{
				if (!this.npcDeathJson.HasField("deathImportantList"))
				{
					this.npcDeathJson.SetField("deathImportantList", JSONObject.arr);
				}
				this.npcDeathJson["deathImportantList"].Add(jsonData.instance.AvatarJsonData[npcId.ToString()]["BindingNpcID"].I);
				NpcJieSuanManager.inst.ImportantNpcBangDingDictionary.Remove(jsonData.instance.AvatarJsonData[npcId.ToString()]["BindingNpcID"].I);
			}
		}
		catch (Exception ex)
		{
			Debug.LogError(ex);
		}
		if (Tools.instance.getPlayer().emailDateMag.cyNpcList.Contains(npcId) && killNpcId != 0)
		{
			Tools.instance.getPlayer().emailDateMag.AuToSendToPlayer(npcId, 999, 999, NpcJieSuanManager.inst.JieSuanTime, null);
		}
		JSONObject jsonobject = new JSONObject();
		jsonobject.SetField("deathType", deathType);
		jsonobject.SetField("deathId", npcId);
		jsonobject.SetField("deathName", jsonData.instance.AvatarRandomJsonData[npcId.ToString()]["Name"]);
		jsonobject.SetField("deathChengHao", jsonData.instance.AvatarJsonData[npcId.ToString()]["Title"].str);
		jsonobject.SetField("deathTime", NpcJieSuanManager.inst.JieSuanTime);
		jsonobject.SetField("type", jsonData.instance.AvatarJsonData[npcId.ToString()]["Type"]);
		if (killNpcId != 0)
		{
			jsonobject.SetField("killNpcId", killNpcId);
		}
		this.npcDeathJson.SetField(npcId.ToString(), jsonobject);
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

	// Token: 0x060014A4 RID: 5284 RVA: 0x000848D0 File Offset: 0x00082AD0
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
		JSONObject jsonobject = jsonData.instance.NpcYiWaiDeathDate[actionId.ToString()];
		try
		{
			num = (int)jsonData.instance.AvatarRandomJsonData[npcId.ToString()]["HaoGanDu"].n;
		}
		catch (Exception)
		{
			Debug.LogError(string.Format("{0}", jsonData.instance.AvatarRandomJsonData.HasField(npcId.ToString())));
		}
		if (num < jsonobject["HaoGanDu"].I && NpcJieSuanManager.inst.getRandomInt(1, 1000) <= jsonobject["SiWangJiLv"].I)
		{
			NpcJieSuanManager.inst.npcDeath.SetNpcDeath(jsonobject["SiWangLeiXing"].I, npcId, 0, false);
			return true;
		}
		if (player.deathType == 0)
		{
			player.deathType = this.deathTypeList[NpcJieSuanManager.inst.getRandomInt(0, this.deathTypeList.Count - 1)];
		}
		if (num < jsonobject["HaoGanDu"].I && player.fakeTimes >= 6 && npcData["Type"].I == player.deathType)
		{
			Tools.instance.getPlayer().fakeTimes -= 6;
			player.deathType = this.deathTypeList[NpcJieSuanManager.inst.getRandomInt(0, this.deathTypeList.Count - 1)];
			NpcJieSuanManager.inst.npcDeath.SetNpcDeath(jsonobject["SiWangLeiXing"].I, npcId, 0, false);
			return true;
		}
		return false;
	}

	// Token: 0x04000F6B RID: 3947
	public JSONObject npcDeathJson = new JSONObject();

	// Token: 0x04000F6C RID: 3948
	public List<int> deathTypeList = new List<int>
	{
		1,
		2,
		3,
		4,
		5,
		10
	};
}
