using System;
using UnityEngine;
using UnityEngine.Events;

namespace script.NpcAction;

public class NpcData
{
	public int NpcId;

	public JSONObject NpcBaseJson;

	public JSONObject NpcFaceJso;

	public JSONObject NpcBagJson;

	public UnityAction SetPlace;

	public bool IsInit;

	public NpcData(int npcId)
	{
		IsInit = false;
		try
		{
			NpcId = npcId;
			NpcBaseJson = jsonData.instance.AvatarJsonData[npcId.ToString()].Copy();
			NpcFaceJso = jsonData.instance.AvatarRandomJsonData[npcId.ToString()].Copy();
			NpcBagJson = jsonData.instance.AvatarBackpackJsonData[npcId.ToString()]["Backpack"].Copy();
			IsInit = true;
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("初始化Npc数据错误,npcId为:" + npcId));
			Debug.LogError((object)ex);
		}
	}

	public void BackWriter()
	{
		jsonData.instance.AvatarJsonData.SetField(NpcId.ToString(), NpcBaseJson.Copy());
		jsonData.instance.AvatarRandomJsonData.SetField(NpcId.ToString(), NpcFaceJso.Copy());
		jsonData.instance.AvatarBackpackJsonData[NpcId.ToString()].SetField("Backpack", NpcBagJson.Copy());
	}
}
