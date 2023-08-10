using System.Collections.Generic;

public class NPCSpeedJieSuan
{
	private List<int> lianDanTagList = new List<int>();

	private List<int> lianQiTagList = new List<int>();

	public NPCSpeedJieSuan()
	{
		lianDanTagList.Add(22);
		lianQiTagList.Add(23);
	}

	public void DoSpeedJieSuan(int times = 1)
	{
		List<int> list = new List<int>();
		int num = 0;
		Tools.instance.getPlayer().fakeTimes += times;
		foreach (JSONObject item in jsonData.instance.AvatarJsonData.list)
		{
			if (item["id"].I >= 20000 && !item.HasField("IsFly"))
			{
				list.Add(item["id"].I);
			}
		}
		foreach (int item2 in list)
		{
			NpcJieSuanManager.inst.npcStatus.ReduceStatusTime(item2, times);
			NpcJieSuanManager.inst.npcSetField.AddNpcMoney(item2, (int)((double)(jsonData.instance.NpcLevelShouYiDate[jsonData.instance.AvatarJsonData[item2.ToString()]["Level"].I.ToString()]["money"].I * times) * 0.4));
			NpcJieSuanManager.inst.GuDingAddExp(item2, (float)times * 1.3f);
			num = jsonData.instance.AvatarJsonData[item2.ToString()]["NPCTag"].I;
			if (NpcJieSuanManager.inst.JieSuanTimes > 0 && NpcJieSuanManager.inst.JieSuanTimes % 12 == 0)
			{
				if (lianDanTagList.Contains(num))
				{
					NpcJieSuanManager.inst.npcSetField.AddNpcWuDaoExp(item2, 21, 200);
				}
				if (lianDanTagList.Contains(num))
				{
					NpcJieSuanManager.inst.npcSetField.AddNpcWuDaoExp(item2, 22, 200);
				}
				if (NpcJieSuanManager.inst.getRandomInt(1, 100) <= 40)
				{
					NpcJieSuanManager.inst.npcLiLian.NPCNingZhouYouLi(item2);
				}
			}
			if (IsSpeedCanBigTuPo(item2))
			{
				NpcSpeedBigTuPo(item2);
			}
			NpcJieSuanManager.inst.npcSetField.AddNpcAge(item2, times);
		}
		Tools.instance.getPlayer().ElderTaskMag.UpdateTaskProcess.CheckHasExecutingTask(times);
	}

	private bool IsSpeedCanBigTuPo(int npcId)
	{
		_ = NpcJieSuanManager.inst.GetNpcData(npcId)["Level"].I;
		if (NpcJieSuanManager.inst.npcStatus.IsInTargetStatus(npcId, 2))
		{
			return true;
		}
		return false;
	}

	private void NpcSpeedBigTuPo(int npcId)
	{
		switch (NpcJieSuanManager.inst.GetNpcData(npcId)["Level"].I)
		{
		case 3:
			NpcJieSuanManager.inst.npcTuPo.NpcTuPoZhuJi(npcId, isKuaiSu: true);
			break;
		case 6:
			NpcJieSuanManager.inst.npcTuPo.NpcTuPoJinDan(npcId, isKuaiSu: true);
			break;
		case 9:
			NpcJieSuanManager.inst.npcTuPo.NpcTuPoYuanYing(npcId, isKuaiSu: true);
			break;
		case 12:
			NpcJieSuanManager.inst.npcTuPo.NpcTuPoHuaShen(npcId, isKuaiSu: true);
			break;
		}
	}
}
