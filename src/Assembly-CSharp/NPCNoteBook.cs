using System;
using UnityEngine;

public class NPCNoteBook
{
	public int GetEventCount(int npcId, int eventId)
	{
		JSONObject jSONObject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		if (!jSONObject["NoteBook"].HasField(eventId.ToString()))
		{
			return 0;
		}
		return jSONObject["NoteBook"][eventId.ToString()].Count;
	}

	public void CreateEventID(JSONObject npc, int id)
	{
		if (!npc["NoteBook"].HasField(id.ToString()))
		{
			npc["NoteBook"].SetField(id.ToString(), JSONObject.arr);
		}
	}

	public void NoteUseDanYao(int npcId, int eventId, int itemId, int useNum)
	{
		JSONObject jSONObject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		CreateEventID(jSONObject, eventId);
		JSONObject jSONObject2 = new JSONObject();
		jSONObject2.SetField("danyao", itemId);
		jSONObject2.SetField("num", useNum);
		jSONObject2.SetField("time", NpcJieSuanManager.inst.JieSuanTime);
		jSONObject["NoteBook"][eventId.ToString()].Add(jSONObject2);
	}

	public void NoteSmallTuPo(int npcId)
	{
		JSONObject jSONObject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		CreateEventID(jSONObject, 21);
		int i = jSONObject["Level"].I;
		JSONObject jSONObject2 = new JSONObject();
		jSONObject2.SetField("jingjie", i);
		jSONObject2.SetField("time", NpcJieSuanManager.inst.JieSuanTime);
		jSONObject["NoteBook"][21.ToString()].Add(jSONObject2);
	}

	public void NoteLianDan(int npcId, int itemId, int quality, int num)
	{
		JSONObject jSONObject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		CreateEventID(jSONObject, 31);
		JSONObject jSONObject2 = new JSONObject();
		jSONObject2.SetField("danyao", itemId);
		jSONObject2.SetField("num", num);
		jSONObject2.SetField("time", NpcJieSuanManager.inst.JieSuanTime);
		jSONObject["NoteBook"][31.ToString()].Add(jSONObject2);
	}

	public void NoteLianQi(int npcId, int quality, int equipType, string name)
	{
		JSONObject jSONObject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		CreateEventID(jSONObject, 32);
		JSONObject jSONObject2 = new JSONObject();
		jSONObject2.SetField("fabaopinjie", quality);
		jSONObject2.SetField("leixing", equipType);
		jSONObject2.SetField("zhuangbei", name);
		jSONObject2.SetField("time", NpcJieSuanManager.inst.JieSuanTime);
		jSONObject["NoteBook"][32.ToString()].Add(jSONObject2);
	}

	public void NoteQiYu(int npcId, int qiYuId, int itemId = -1, int itemNum = -1)
	{
		JSONObject jSONObject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		CreateEventID(jSONObject, 33);
		JSONObject jSONObject2 = new JSONObject();
		jSONObject2.SetField("qiYuId", qiYuId);
		if (itemId != -1)
		{
			jSONObject2.SetField("item", itemId);
			jSONObject2.SetField("num", itemNum);
		}
		jSONObject2.SetField("time", NpcJieSuanManager.inst.JieSuanTime);
		jSONObject["NoteBook"][33.ToString()].Add(jSONObject2);
	}

	public void NoteBigTuPoFail(int npcId, int eventId)
	{
		JSONObject jSONObject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		CreateEventID(jSONObject, eventId);
		JSONObject jSONObject2 = new JSONObject();
		jSONObject2.SetField("time", NpcJieSuanManager.inst.JieSuanTime);
		jSONObject["NoteBook"][eventId.ToString()].Add(jSONObject2);
	}

	public void NoteZhuJiSuccess(int npcId)
	{
		JSONObject jSONObject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		CreateEventID(jSONObject, 22);
		JSONObject jSONObject2 = new JSONObject();
		jSONObject2.SetField("time", NpcJieSuanManager.inst.JieSuanTime);
		jSONObject["NoteBook"][22.ToString()].Add(jSONObject2);
	}

	public void NoteJinDanSuccess(int npcId, int level)
	{
		JSONObject jSONObject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		CreateEventID(jSONObject, 23);
		JSONObject jSONObject2 = new JSONObject();
		jSONObject2.SetField("cnnum", level);
		jSONObject2.SetField("time", NpcJieSuanManager.inst.JieSuanTime);
		jSONObject["NoteBook"][23.ToString()].Add(jSONObject2);
	}

	public void NoteYuanYingSuccess(int npcId)
	{
		JSONObject jSONObject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		CreateEventID(jSONObject, 24);
		JSONObject jSONObject2 = new JSONObject();
		jSONObject2.SetField("time", NpcJieSuanManager.inst.JieSuanTime);
		jSONObject["NoteBook"][24.ToString()].Add(jSONObject2);
	}

	public void NoteHuaShenSuccess(int npcId)
	{
		JSONObject jSONObject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		CreateEventID(jSONObject, 25);
		JSONObject jSONObject2 = new JSONObject();
		jSONObject2.SetField("time", NpcJieSuanManager.inst.JieSuanTime);
		jSONObject["NoteBook"][25.ToString()].Add(jSONObject2);
	}

	public void NoteLunDaoSuccess(int npcId, string name)
	{
		JSONObject jSONObject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		CreateEventID(jSONObject, 50);
		JSONObject jSONObject2 = new JSONObject();
		jSONObject2.SetField("npcname", name);
		jSONObject2.SetField("time", NpcJieSuanManager.inst.JieSuanTime);
		jSONObject["NoteBook"][50.ToString()].Add(jSONObject2);
	}

	public void NoteLunDaoFail(int npcId, string name)
	{
		JSONObject jSONObject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		CreateEventID(jSONObject, 51);
		JSONObject jSONObject2 = new JSONObject();
		jSONObject2.SetField("npcname", name);
		jSONObject2.SetField("time", NpcJieSuanManager.inst.JieSuanTime);
		jSONObject["NoteBook"][51.ToString()].Add(jSONObject2);
	}

	public void NotePaiMai(int npcId, int itemId, string name = "")
	{
		JSONObject jSONObject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		CreateEventID(jSONObject, 60);
		JSONObject jSONObject2 = new JSONObject();
		jSONObject2.SetField("item", itemId);
		jSONObject2.SetField("itemName", name);
		jSONObject2.SetField("time", NpcJieSuanManager.inst.JieSuanTime);
		jSONObject["NoteBook"][60.ToString()].Add(jSONObject2);
	}

	public void NoteImprotantEvent(int npcId, int eventId, string time)
	{
		JSONObject jSONObject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		CreateEventID(jSONObject, 101);
		JSONObject jSONObject2 = new JSONObject();
		jSONObject2.SetField("gudingshijian", eventId);
		jSONObject2.SetField("time", time);
		jSONObject["NoteBook"][101.ToString()].Add(jSONObject2);
	}

	public void NoteJieShaSuccess(int npcId, string name = "")
	{
		JSONObject jSONObject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		CreateEventID(jSONObject, 40);
		JSONObject jSONObject2 = new JSONObject();
		jSONObject2.SetField("npcname", name);
		jSONObject2.SetField("time", NpcJieSuanManager.inst.JieSuanTime);
		jSONObject["NoteBook"][40.ToString()].Add(jSONObject2);
	}

	public void NoteJieShaFail1(int npcId, string name = "")
	{
		JSONObject jSONObject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		CreateEventID(jSONObject, 41);
		JSONObject jSONObject2 = new JSONObject();
		jSONObject2.SetField("npcname", name);
		jSONObject2.SetField("time", NpcJieSuanManager.inst.JieSuanTime);
		jSONObject["NoteBook"][41.ToString()].Add(jSONObject2);
	}

	public void NoteJieShaFail2(int npcId, string name = "")
	{
		JSONObject jSONObject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		CreateEventID(jSONObject, 42);
		JSONObject jSONObject2 = new JSONObject();
		jSONObject2.SetField("npcname", name);
		jSONObject2.SetField("time", NpcJieSuanManager.inst.JieSuanTime);
		jSONObject["NoteBook"][42.ToString()].Add(jSONObject2);
	}

	public void NoteFanShaSuccess(int npcId, string name = "")
	{
		JSONObject jSONObject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		CreateEventID(jSONObject, 43);
		JSONObject jSONObject2 = new JSONObject();
		jSONObject2.SetField("npcname", name);
		jSONObject2.SetField("time", NpcJieSuanManager.inst.JieSuanTime);
		jSONObject["NoteBook"][43.ToString()].Add(jSONObject2);
	}

	public void NoteFanShaFail1(int npcId, string name = "")
	{
		JSONObject jSONObject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		CreateEventID(jSONObject, 45);
		JSONObject jSONObject2 = new JSONObject();
		jSONObject2.SetField("npcname", name);
		jSONObject2.SetField("time", NpcJieSuanManager.inst.JieSuanTime);
		jSONObject["NoteBook"][45.ToString()].Add(jSONObject2);
	}

	public void NoteFanShaFail2(int npcId, string name = "")
	{
		JSONObject jSONObject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		CreateEventID(jSONObject, 44);
		JSONObject jSONObject2 = new JSONObject();
		jSONObject2.SetField("npcname", name);
		jSONObject2.SetField("time", NpcJieSuanManager.inst.JieSuanTime);
		jSONObject["NoteBook"][44.ToString()].Add(jSONObject2);
	}

	public void NoteTianJiDaBi(int npcId, int rank, string time)
	{
		try
		{
			JSONObject jSONObject = jsonData.instance.AvatarJsonData[npcId.ToString()];
			int id;
			switch (rank)
			{
			case 1:
				id = 70;
				break;
			case 2:
			case 3:
			case 4:
			case 5:
			case 6:
			case 7:
			case 8:
			case 9:
			case 10:
				id = 71;
				break;
			default:
				id = 72;
				break;
			}
			CreateEventID(jSONObject, id);
			JSONObject jSONObject2 = new JSONObject();
			jSONObject2.SetField("rank", rank);
			jSONObject2.SetField("time", time);
			jSONObject["NoteBook"][id.ToString()].Add(jSONObject2);
		}
		catch (Exception ex)
		{
			Debug.LogError((object)$"为NPC记录天机大比数据出错，NPCID:{npcId} 排名:{rank} 时间:{time}，异常:{ex}");
		}
	}
}
