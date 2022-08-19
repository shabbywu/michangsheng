using System;
using System.Collections.Generic;

// Token: 0x02000224 RID: 548
public class NPCSpeedJieSuan
{
	// Token: 0x060015BE RID: 5566 RVA: 0x0009157C File Offset: 0x0008F77C
	public NPCSpeedJieSuan()
	{
		this.lianDanTagList.Add(22);
		this.lianQiTagList.Add(23);
	}

	// Token: 0x060015BF RID: 5567 RVA: 0x000915B4 File Offset: 0x0008F7B4
	public void DoSpeedJieSuan(int times = 1)
	{
		List<int> list = new List<int>();
		Tools.instance.getPlayer().fakeTimes += times;
		foreach (JSONObject jsonobject in jsonData.instance.AvatarJsonData.list)
		{
			if (jsonobject["id"].I >= 20000 && !jsonobject.HasField("IsFly"))
			{
				list.Add(jsonobject["id"].I);
			}
		}
		foreach (int npcId in list)
		{
			NpcJieSuanManager.inst.npcStatus.ReduceStatusTime(npcId, times);
			NpcJieSuanManager.inst.npcSetField.AddNpcMoney(npcId, (int)((double)(jsonData.instance.NpcLevelShouYiDate[jsonData.instance.AvatarJsonData[npcId.ToString()]["Level"].I.ToString()]["money"].I * times) * 0.4));
			NpcJieSuanManager.inst.GuDingAddExp(npcId, (float)times * 1.3f);
			int i = jsonData.instance.AvatarJsonData[npcId.ToString()]["NPCTag"].I;
			if (NpcJieSuanManager.inst.JieSuanTimes > 0 && NpcJieSuanManager.inst.JieSuanTimes % 12 == 0)
			{
				if (this.lianDanTagList.Contains(i))
				{
					NpcJieSuanManager.inst.npcSetField.AddNpcWuDaoExp(npcId, 21, 200);
				}
				if (this.lianDanTagList.Contains(i))
				{
					NpcJieSuanManager.inst.npcSetField.AddNpcWuDaoExp(npcId, 22, 200);
				}
				if (NpcJieSuanManager.inst.getRandomInt(1, 100) <= 40)
				{
					NpcJieSuanManager.inst.npcLiLian.NPCNingZhouYouLi(npcId);
				}
			}
			if (this.IsSpeedCanBigTuPo(npcId))
			{
				this.NpcSpeedBigTuPo(npcId);
			}
			NpcJieSuanManager.inst.npcSetField.AddNpcAge(npcId, times);
		}
		Tools.instance.getPlayer().ElderTaskMag.UpdateTaskProcess.CheckHasExecutingTask(times);
	}

	// Token: 0x060015C0 RID: 5568 RVA: 0x00091838 File Offset: 0x0008FA38
	private bool IsSpeedCanBigTuPo(int npcId)
	{
		int i = NpcJieSuanManager.inst.GetNpcData(npcId)["Level"].I;
		return NpcJieSuanManager.inst.npcStatus.IsInTargetStatus(npcId, 2);
	}

	// Token: 0x060015C1 RID: 5569 RVA: 0x0009186C File Offset: 0x0008FA6C
	private void NpcSpeedBigTuPo(int npcId)
	{
		int i = NpcJieSuanManager.inst.GetNpcData(npcId)["Level"].I;
		if (i == 3)
		{
			NpcJieSuanManager.inst.npcTuPo.NpcTuPoZhuJi(npcId, true);
			return;
		}
		if (i == 6)
		{
			NpcJieSuanManager.inst.npcTuPo.NpcTuPoJinDan(npcId, true);
			return;
		}
		if (i == 9)
		{
			NpcJieSuanManager.inst.npcTuPo.NpcTuPoYuanYing(npcId, true);
			return;
		}
		if (i == 12)
		{
			NpcJieSuanManager.inst.npcTuPo.NpcTuPoHuaShen(npcId, true);
		}
	}

	// Token: 0x04001040 RID: 4160
	private List<int> lianDanTagList = new List<int>();

	// Token: 0x04001041 RID: 4161
	private List<int> lianQiTagList = new List<int>();
}
