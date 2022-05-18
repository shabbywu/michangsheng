using System;

// Token: 0x02000341 RID: 833
public class NPCStatus
{
	// Token: 0x0600187B RID: 6267 RVA: 0x000DA2F8 File Offset: 0x000D84F8
	public void SetNpcStatus(int npcId, int status)
	{
		JSONObject npcData = NpcJieSuanManager.inst.getNpcData(npcId);
		int i = jsonData.instance.NpcStatusDate[status.ToString()]["Time"].I;
		JSONObject jsonobject = new JSONObject();
		jsonobject.SetField("StatusId", status);
		jsonobject.SetField("StatusTime", i);
		int i2 = npcData["Status"]["StatusId"].I;
		this.NpcUpDateShuXing(i2, status, npcData);
		npcData.SetField("Status", jsonobject);
	}

	// Token: 0x0600187C RID: 6268 RVA: 0x0001542A File Offset: 0x0001362A
	public bool IsInTargetStatus(int npcId, int targetStatusId)
	{
		return jsonData.instance.AvatarJsonData[npcId.ToString()]["Status"]["StatusId"].I == targetStatusId;
	}

	// Token: 0x0600187D RID: 6269 RVA: 0x000DA388 File Offset: 0x000D8588
	public void ReduceStatusTime(int npcId, int times)
	{
		JSONObject npcData = NpcJieSuanManager.inst.getNpcData(npcId);
		int num = npcData["Status"]["StatusTime"].I - times;
		if (num <= 0)
		{
			this.SetNpcStatus(npcId, 1);
			return;
		}
		npcData["Status"].SetField("StatusTime", num);
	}

	// Token: 0x0600187E RID: 6270 RVA: 0x000DA3E4 File Offset: 0x000D85E4
	public void NpcUpDateShuXing(int lastStatus, int curStatus, JSONObject npcData)
	{
		int i = npcData["id"].I;
		if (lastStatus == curStatus)
		{
			return;
		}
		if (lastStatus == 5)
		{
			int i2 = npcData["shengShi"].I;
			NpcJieSuanManager.inst.npcSetField.AddNpcShenShi(i, i2);
		}
		if (curStatus == 5)
		{
			int num = npcData["shengShi"].I / 2;
			NpcJieSuanManager.inst.npcSetField.AddNpcShenShi(i, -num);
		}
	}
}
