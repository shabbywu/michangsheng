using System;

// Token: 0x02000225 RID: 549
public class NPCStatus
{
	// Token: 0x060015C3 RID: 5571 RVA: 0x000918F0 File Offset: 0x0008FAF0
	public void SetNpcStatus(int npcId, int status)
	{
		JSONObject npcData = NpcJieSuanManager.inst.GetNpcData(npcId);
		int i = jsonData.instance.NpcStatusDate[status.ToString()]["Time"].I;
		JSONObject jsonobject = new JSONObject();
		jsonobject.SetField("StatusId", status);
		jsonobject.SetField("StatusTime", i);
		int i2 = npcData["Status"]["StatusId"].I;
		this.NpcUpDateShuXing(i2, status, npcData);
		npcData.SetField("Status", jsonobject);
	}

	// Token: 0x060015C4 RID: 5572 RVA: 0x0009197D File Offset: 0x0008FB7D
	public bool IsInTargetStatus(int npcId, int targetStatusId)
	{
		return jsonData.instance.AvatarJsonData[npcId.ToString()]["Status"]["StatusId"].I == targetStatusId;
	}

	// Token: 0x060015C5 RID: 5573 RVA: 0x000919B4 File Offset: 0x0008FBB4
	public void ReduceStatusTime(int npcId, int times)
	{
		JSONObject npcData = NpcJieSuanManager.inst.GetNpcData(npcId);
		int num = npcData["Status"]["StatusTime"].I - times;
		if (num <= 0)
		{
			this.SetNpcStatus(npcId, 1);
			return;
		}
		npcData["Status"].SetField("StatusTime", num);
	}

	// Token: 0x060015C6 RID: 5574 RVA: 0x00091A10 File Offset: 0x0008FC10
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
