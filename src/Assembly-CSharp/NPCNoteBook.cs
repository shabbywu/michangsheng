using System;
using UnityEngine;

// Token: 0x02000214 RID: 532
public class NPCNoteBook
{
	// Token: 0x06001535 RID: 5429 RVA: 0x00088A9C File Offset: 0x00086C9C
	public int GetEventCount(int npcId, int eventId)
	{
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		if (!jsonobject["NoteBook"].HasField(eventId.ToString()))
		{
			return 0;
		}
		return jsonobject["NoteBook"][eventId.ToString()].Count;
	}

	// Token: 0x06001536 RID: 5430 RVA: 0x00088AF7 File Offset: 0x00086CF7
	public void CreateEventID(JSONObject npc, int id)
	{
		if (!npc["NoteBook"].HasField(id.ToString()))
		{
			npc["NoteBook"].SetField(id.ToString(), JSONObject.arr);
		}
	}

	// Token: 0x06001537 RID: 5431 RVA: 0x00088B30 File Offset: 0x00086D30
	public void NoteUseDanYao(int npcId, int eventId, int itemId, int useNum)
	{
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		this.CreateEventID(jsonobject, eventId);
		JSONObject jsonobject2 = new JSONObject();
		jsonobject2.SetField("danyao", itemId);
		jsonobject2.SetField("num", useNum);
		jsonobject2.SetField("time", NpcJieSuanManager.inst.JieSuanTime);
		jsonobject["NoteBook"][eventId.ToString()].Add(jsonobject2);
	}

	// Token: 0x06001538 RID: 5432 RVA: 0x00088BB0 File Offset: 0x00086DB0
	public void NoteSmallTuPo(int npcId)
	{
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		this.CreateEventID(jsonobject, 21);
		int i = jsonobject["Level"].I;
		JSONObject jsonobject2 = new JSONObject();
		jsonobject2.SetField("jingjie", i);
		jsonobject2.SetField("time", NpcJieSuanManager.inst.JieSuanTime);
		jsonobject["NoteBook"][21.ToString()].Add(jsonobject2);
	}

	// Token: 0x06001539 RID: 5433 RVA: 0x00088C38 File Offset: 0x00086E38
	public void NoteLianDan(int npcId, int itemId, int quality, int num)
	{
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		this.CreateEventID(jsonobject, 31);
		JSONObject jsonobject2 = new JSONObject();
		jsonobject2.SetField("danyao", itemId);
		jsonobject2.SetField("num", num);
		jsonobject2.SetField("time", NpcJieSuanManager.inst.JieSuanTime);
		jsonobject["NoteBook"][31.ToString()].Add(jsonobject2);
	}

	// Token: 0x0600153A RID: 5434 RVA: 0x00088CBC File Offset: 0x00086EBC
	public void NoteLianQi(int npcId, int quality, int equipType, string name)
	{
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		this.CreateEventID(jsonobject, 32);
		JSONObject jsonobject2 = new JSONObject();
		jsonobject2.SetField("fabaopinjie", quality);
		jsonobject2.SetField("leixing", equipType);
		jsonobject2.SetField("zhuangbei", name);
		jsonobject2.SetField("time", NpcJieSuanManager.inst.JieSuanTime);
		jsonobject["NoteBook"][32.ToString()].Add(jsonobject2);
	}

	// Token: 0x0600153B RID: 5435 RVA: 0x00088D4C File Offset: 0x00086F4C
	public void NoteQiYu(int npcId, int qiYuId, int itemId = -1, int itemNum = -1)
	{
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		this.CreateEventID(jsonobject, 33);
		JSONObject jsonobject2 = new JSONObject();
		jsonobject2.SetField("qiYuId", qiYuId);
		if (itemId != -1)
		{
			jsonobject2.SetField("item", itemId);
			jsonobject2.SetField("num", itemNum);
		}
		jsonobject2.SetField("time", NpcJieSuanManager.inst.JieSuanTime);
		jsonobject["NoteBook"][33.ToString()].Add(jsonobject2);
	}

	// Token: 0x0600153C RID: 5436 RVA: 0x00088DE0 File Offset: 0x00086FE0
	public void NoteBigTuPoFail(int npcId, int eventId)
	{
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		this.CreateEventID(jsonobject, eventId);
		JSONObject jsonobject2 = new JSONObject();
		jsonobject2.SetField("time", NpcJieSuanManager.inst.JieSuanTime);
		jsonobject["NoteBook"][eventId.ToString()].Add(jsonobject2);
	}

	// Token: 0x0600153D RID: 5437 RVA: 0x00088E44 File Offset: 0x00087044
	public void NoteZhuJiSuccess(int npcId)
	{
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		this.CreateEventID(jsonobject, 22);
		JSONObject jsonobject2 = new JSONObject();
		jsonobject2.SetField("time", NpcJieSuanManager.inst.JieSuanTime);
		jsonobject["NoteBook"][22.ToString()].Add(jsonobject2);
	}

	// Token: 0x0600153E RID: 5438 RVA: 0x00088EAC File Offset: 0x000870AC
	public void NoteJinDanSuccess(int npcId, int level)
	{
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		this.CreateEventID(jsonobject, 23);
		JSONObject jsonobject2 = new JSONObject();
		jsonobject2.SetField("cnnum", level);
		jsonobject2.SetField("time", NpcJieSuanManager.inst.JieSuanTime);
		jsonobject["NoteBook"][23.ToString()].Add(jsonobject2);
	}

	// Token: 0x0600153F RID: 5439 RVA: 0x00088F20 File Offset: 0x00087120
	public void NoteYuanYingSuccess(int npcId)
	{
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		this.CreateEventID(jsonobject, 24);
		JSONObject jsonobject2 = new JSONObject();
		jsonobject2.SetField("time", NpcJieSuanManager.inst.JieSuanTime);
		jsonobject["NoteBook"][24.ToString()].Add(jsonobject2);
	}

	// Token: 0x06001540 RID: 5440 RVA: 0x00088F88 File Offset: 0x00087188
	public void NoteHuaShenSuccess(int npcId)
	{
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		this.CreateEventID(jsonobject, 25);
		JSONObject jsonobject2 = new JSONObject();
		jsonobject2.SetField("time", NpcJieSuanManager.inst.JieSuanTime);
		jsonobject["NoteBook"][25.ToString()].Add(jsonobject2);
	}

	// Token: 0x06001541 RID: 5441 RVA: 0x00088FF0 File Offset: 0x000871F0
	public void NoteLunDaoSuccess(int npcId, string name)
	{
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		this.CreateEventID(jsonobject, 50);
		JSONObject jsonobject2 = new JSONObject();
		jsonobject2.SetField("npcname", name);
		jsonobject2.SetField("time", NpcJieSuanManager.inst.JieSuanTime);
		jsonobject["NoteBook"][50.ToString()].Add(jsonobject2);
	}

	// Token: 0x06001542 RID: 5442 RVA: 0x00089064 File Offset: 0x00087264
	public void NoteLunDaoFail(int npcId, string name)
	{
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		this.CreateEventID(jsonobject, 51);
		JSONObject jsonobject2 = new JSONObject();
		jsonobject2.SetField("npcname", name);
		jsonobject2.SetField("time", NpcJieSuanManager.inst.JieSuanTime);
		jsonobject["NoteBook"][51.ToString()].Add(jsonobject2);
	}

	// Token: 0x06001543 RID: 5443 RVA: 0x000890D8 File Offset: 0x000872D8
	public void NotePaiMai(int npcId, int itemId, string name = "")
	{
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		this.CreateEventID(jsonobject, 60);
		JSONObject jsonobject2 = new JSONObject();
		jsonobject2.SetField("item", itemId);
		jsonobject2.SetField("itemName", name);
		jsonobject2.SetField("time", NpcJieSuanManager.inst.JieSuanTime);
		jsonobject["NoteBook"][60.ToString()].Add(jsonobject2);
	}

	// Token: 0x06001544 RID: 5444 RVA: 0x00089158 File Offset: 0x00087358
	public void NoteImprotantEvent(int npcId, int eventId, string time)
	{
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		this.CreateEventID(jsonobject, 101);
		JSONObject jsonobject2 = new JSONObject();
		jsonobject2.SetField("gudingshijian", eventId);
		jsonobject2.SetField("time", time);
		jsonobject["NoteBook"][101.ToString()].Add(jsonobject2);
	}

	// Token: 0x06001545 RID: 5445 RVA: 0x000891C4 File Offset: 0x000873C4
	public void NoteJieShaSuccess(int npcId, string name = "")
	{
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		this.CreateEventID(jsonobject, 40);
		JSONObject jsonobject2 = new JSONObject();
		jsonobject2.SetField("npcname", name);
		jsonobject2.SetField("time", NpcJieSuanManager.inst.JieSuanTime);
		jsonobject["NoteBook"][40.ToString()].Add(jsonobject2);
	}

	// Token: 0x06001546 RID: 5446 RVA: 0x00089238 File Offset: 0x00087438
	public void NoteJieShaFail1(int npcId, string name = "")
	{
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		this.CreateEventID(jsonobject, 41);
		JSONObject jsonobject2 = new JSONObject();
		jsonobject2.SetField("npcname", name);
		jsonobject2.SetField("time", NpcJieSuanManager.inst.JieSuanTime);
		jsonobject["NoteBook"][41.ToString()].Add(jsonobject2);
	}

	// Token: 0x06001547 RID: 5447 RVA: 0x000892AC File Offset: 0x000874AC
	public void NoteJieShaFail2(int npcId, string name = "")
	{
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		this.CreateEventID(jsonobject, 42);
		JSONObject jsonobject2 = new JSONObject();
		jsonobject2.SetField("npcname", name);
		jsonobject2.SetField("time", NpcJieSuanManager.inst.JieSuanTime);
		jsonobject["NoteBook"][42.ToString()].Add(jsonobject2);
	}

	// Token: 0x06001548 RID: 5448 RVA: 0x00089320 File Offset: 0x00087520
	public void NoteFanShaSuccess(int npcId, string name = "")
	{
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		this.CreateEventID(jsonobject, 43);
		JSONObject jsonobject2 = new JSONObject();
		jsonobject2.SetField("npcname", name);
		jsonobject2.SetField("time", NpcJieSuanManager.inst.JieSuanTime);
		jsonobject["NoteBook"][43.ToString()].Add(jsonobject2);
	}

	// Token: 0x06001549 RID: 5449 RVA: 0x00089394 File Offset: 0x00087594
	public void NoteFanShaFail1(int npcId, string name = "")
	{
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		this.CreateEventID(jsonobject, 45);
		JSONObject jsonobject2 = new JSONObject();
		jsonobject2.SetField("npcname", name);
		jsonobject2.SetField("time", NpcJieSuanManager.inst.JieSuanTime);
		jsonobject["NoteBook"][45.ToString()].Add(jsonobject2);
	}

	// Token: 0x0600154A RID: 5450 RVA: 0x00089408 File Offset: 0x00087608
	public void NoteFanShaFail2(int npcId, string name = "")
	{
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		this.CreateEventID(jsonobject, 44);
		JSONObject jsonobject2 = new JSONObject();
		jsonobject2.SetField("npcname", name);
		jsonobject2.SetField("time", NpcJieSuanManager.inst.JieSuanTime);
		jsonobject["NoteBook"][44.ToString()].Add(jsonobject2);
	}

	// Token: 0x0600154B RID: 5451 RVA: 0x0008947C File Offset: 0x0008767C
	public void NoteTianJiDaBi(int npcId, int rank, string time)
	{
		try
		{
			JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcId.ToString()];
			int id;
			if (rank == 1)
			{
				id = 70;
			}
			else if (rank >= 2 && rank <= 10)
			{
				id = 71;
			}
			else
			{
				id = 72;
			}
			this.CreateEventID(jsonobject, id);
			JSONObject jsonobject2 = new JSONObject();
			jsonobject2.SetField("rank", rank);
			jsonobject2.SetField("time", time);
			jsonobject["NoteBook"][id.ToString()].Add(jsonobject2);
		}
		catch (Exception ex)
		{
			Debug.LogError(string.Format("为NPC记录天机大比数据出错，NPCID:{0} 排名:{1} 时间:{2}，异常:{3}", new object[]
			{
				npcId,
				rank,
				time,
				ex
			}));
		}
	}
}
