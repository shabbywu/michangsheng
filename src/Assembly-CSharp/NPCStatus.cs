public class NPCStatus
{
	public void SetNpcStatus(int npcId, int status)
	{
		JSONObject npcData = NpcJieSuanManager.inst.GetNpcData(npcId);
		int i = jsonData.instance.NpcStatusDate[status.ToString()]["Time"].I;
		JSONObject jSONObject = new JSONObject();
		jSONObject.SetField("StatusId", status);
		jSONObject.SetField("StatusTime", i);
		int i2 = npcData["Status"]["StatusId"].I;
		NpcUpDateShuXing(i2, status, npcData);
		npcData.SetField("Status", jSONObject);
	}

	public bool IsInTargetStatus(int npcId, int targetStatusId)
	{
		if (jsonData.instance.AvatarJsonData[npcId.ToString()]["Status"]["StatusId"].I == targetStatusId)
		{
			return true;
		}
		return false;
	}

	public void ReduceStatusTime(int npcId, int times)
	{
		JSONObject npcData = NpcJieSuanManager.inst.GetNpcData(npcId);
		int num = npcData["Status"]["StatusTime"].I - times;
		if (num <= 0)
		{
			SetNpcStatus(npcId, 1);
		}
		else
		{
			npcData["Status"].SetField("StatusTime", num);
		}
	}

	public void NpcUpDateShuXing(int lastStatus, int curStatus, JSONObject npcData)
	{
		int i = npcData["id"].I;
		if (lastStatus != curStatus)
		{
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
}
