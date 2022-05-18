using System;

// Token: 0x0200032C RID: 812
public class NPCNoteBook
{
	// Token: 0x060017E6 RID: 6118 RVA: 0x000D102C File Offset: 0x000CF22C
	public int GetEventCount(int npcId, int eventId)
	{
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		if (!jsonobject["NoteBook"].HasField(eventId.ToString()))
		{
			return 0;
		}
		return jsonobject["NoteBook"][eventId.ToString()].Count;
	}

	// Token: 0x060017E7 RID: 6119 RVA: 0x000150F1 File Offset: 0x000132F1
	public void CreateEventID(JSONObject npc, int id)
	{
		if (!npc["NoteBook"].HasField(id.ToString()))
		{
			npc["NoteBook"].SetField(id.ToString(), JSONObject.arr);
		}
	}

	// Token: 0x060017E8 RID: 6120 RVA: 0x000D1088 File Offset: 0x000CF288
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

	// Token: 0x060017E9 RID: 6121 RVA: 0x000D1108 File Offset: 0x000CF308
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

	// Token: 0x060017EA RID: 6122 RVA: 0x000D1190 File Offset: 0x000CF390
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

	// Token: 0x060017EB RID: 6123 RVA: 0x000D1214 File Offset: 0x000CF414
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

	// Token: 0x060017EC RID: 6124 RVA: 0x000D12A4 File Offset: 0x000CF4A4
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

	// Token: 0x060017ED RID: 6125 RVA: 0x000D1338 File Offset: 0x000CF538
	public void NoteBigTuPoFail(int npcId, int eventId)
	{
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		this.CreateEventID(jsonobject, eventId);
		JSONObject jsonobject2 = new JSONObject();
		jsonobject2.SetField("time", NpcJieSuanManager.inst.JieSuanTime);
		jsonobject["NoteBook"][eventId.ToString()].Add(jsonobject2);
	}

	// Token: 0x060017EE RID: 6126 RVA: 0x000D139C File Offset: 0x000CF59C
	public void NoteZhuJiSuccess(int npcId)
	{
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		this.CreateEventID(jsonobject, 22);
		JSONObject jsonobject2 = new JSONObject();
		jsonobject2.SetField("time", NpcJieSuanManager.inst.JieSuanTime);
		jsonobject["NoteBook"][22.ToString()].Add(jsonobject2);
	}

	// Token: 0x060017EF RID: 6127 RVA: 0x000D1404 File Offset: 0x000CF604
	public void NoteJinDanSuccess(int npcId, int level)
	{
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		this.CreateEventID(jsonobject, 23);
		JSONObject jsonobject2 = new JSONObject();
		jsonobject2.SetField("cnnum", level);
		jsonobject2.SetField("time", NpcJieSuanManager.inst.JieSuanTime);
		jsonobject["NoteBook"][23.ToString()].Add(jsonobject2);
	}

	// Token: 0x060017F0 RID: 6128 RVA: 0x000D1478 File Offset: 0x000CF678
	public void NoteYuanYingSuccess(int npcId)
	{
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		this.CreateEventID(jsonobject, 24);
		JSONObject jsonobject2 = new JSONObject();
		jsonobject2.SetField("time", NpcJieSuanManager.inst.JieSuanTime);
		jsonobject["NoteBook"][24.ToString()].Add(jsonobject2);
	}

	// Token: 0x060017F1 RID: 6129 RVA: 0x000D14E0 File Offset: 0x000CF6E0
	public void NoteHuaShenSuccess(int npcId)
	{
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		this.CreateEventID(jsonobject, 25);
		JSONObject jsonobject2 = new JSONObject();
		jsonobject2.SetField("time", NpcJieSuanManager.inst.JieSuanTime);
		jsonobject["NoteBook"][25.ToString()].Add(jsonobject2);
	}

	// Token: 0x060017F2 RID: 6130 RVA: 0x000D1548 File Offset: 0x000CF748
	public void NoteLunDaoSuccess(int npcId, string name)
	{
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		this.CreateEventID(jsonobject, 50);
		JSONObject jsonobject2 = new JSONObject();
		jsonobject2.SetField("npcname", name);
		jsonobject2.SetField("time", NpcJieSuanManager.inst.JieSuanTime);
		jsonobject["NoteBook"][50.ToString()].Add(jsonobject2);
	}

	// Token: 0x060017F3 RID: 6131 RVA: 0x000D15BC File Offset: 0x000CF7BC
	public void NoteLunDaoFail(int npcId, string name)
	{
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		this.CreateEventID(jsonobject, 51);
		JSONObject jsonobject2 = new JSONObject();
		jsonobject2.SetField("npcname", name);
		jsonobject2.SetField("time", NpcJieSuanManager.inst.JieSuanTime);
		jsonobject["NoteBook"][51.ToString()].Add(jsonobject2);
	}

	// Token: 0x060017F4 RID: 6132 RVA: 0x000D1630 File Offset: 0x000CF830
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

	// Token: 0x060017F5 RID: 6133 RVA: 0x000D16B0 File Offset: 0x000CF8B0
	public void NoteImprotantEvent(int npcId, int eventId, string time)
	{
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		this.CreateEventID(jsonobject, 101);
		JSONObject jsonobject2 = new JSONObject();
		jsonobject2.SetField("gudingshijian", eventId);
		jsonobject2.SetField("time", time);
		jsonobject["NoteBook"][101.ToString()].Add(jsonobject2);
	}

	// Token: 0x060017F6 RID: 6134 RVA: 0x000D171C File Offset: 0x000CF91C
	public void NoteJieShaSuccess(int npcId, string name = "")
	{
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		this.CreateEventID(jsonobject, 40);
		JSONObject jsonobject2 = new JSONObject();
		jsonobject2.SetField("npcname", name);
		jsonobject2.SetField("time", NpcJieSuanManager.inst.JieSuanTime);
		jsonobject["NoteBook"][40.ToString()].Add(jsonobject2);
	}

	// Token: 0x060017F7 RID: 6135 RVA: 0x000D1790 File Offset: 0x000CF990
	public void NoteJieShaFail1(int npcId, string name = "")
	{
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		this.CreateEventID(jsonobject, 41);
		JSONObject jsonobject2 = new JSONObject();
		jsonobject2.SetField("npcname", name);
		jsonobject2.SetField("time", NpcJieSuanManager.inst.JieSuanTime);
		jsonobject["NoteBook"][41.ToString()].Add(jsonobject2);
	}

	// Token: 0x060017F8 RID: 6136 RVA: 0x000D1804 File Offset: 0x000CFA04
	public void NoteJieShaFail2(int npcId, string name = "")
	{
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		this.CreateEventID(jsonobject, 42);
		JSONObject jsonobject2 = new JSONObject();
		jsonobject2.SetField("npcname", name);
		jsonobject2.SetField("time", NpcJieSuanManager.inst.JieSuanTime);
		jsonobject["NoteBook"][42.ToString()].Add(jsonobject2);
	}

	// Token: 0x060017F9 RID: 6137 RVA: 0x000D1878 File Offset: 0x000CFA78
	public void NoteFanShaSuccess(int npcId, string name = "")
	{
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		this.CreateEventID(jsonobject, 43);
		JSONObject jsonobject2 = new JSONObject();
		jsonobject2.SetField("npcname", name);
		jsonobject2.SetField("time", NpcJieSuanManager.inst.JieSuanTime);
		jsonobject["NoteBook"][43.ToString()].Add(jsonobject2);
	}

	// Token: 0x060017FA RID: 6138 RVA: 0x000D18EC File Offset: 0x000CFAEC
	public void NoteFanShaFail1(int npcId, string name = "")
	{
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		this.CreateEventID(jsonobject, 45);
		JSONObject jsonobject2 = new JSONObject();
		jsonobject2.SetField("npcname", name);
		jsonobject2.SetField("time", NpcJieSuanManager.inst.JieSuanTime);
		jsonobject["NoteBook"][45.ToString()].Add(jsonobject2);
	}

	// Token: 0x060017FB RID: 6139 RVA: 0x000D1960 File Offset: 0x000CFB60
	public void NoteFanShaFail2(int npcId, string name = "")
	{
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		this.CreateEventID(jsonobject, 44);
		JSONObject jsonobject2 = new JSONObject();
		jsonobject2.SetField("npcname", name);
		jsonobject2.SetField("time", NpcJieSuanManager.inst.JieSuanTime);
		jsonobject["NoteBook"][44.ToString()].Add(jsonobject2);
	}

	// Token: 0x060017FC RID: 6140 RVA: 0x000D19D4 File Offset: 0x000CFBD4
	public void NoteTianJiDaBi(int npcId, int rank, string time)
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
}
