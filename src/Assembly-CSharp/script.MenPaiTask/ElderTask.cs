using System;
using System.Collections.Generic;
using Bag;
using JSONClass;
using KBEngine;

namespace script.MenPaiTask;

[Serializable]
public class ElderTask
{
	public readonly List<BaseItem> needItemList = new List<BaseItem>(5);

	public int Money;

	public int HasCostTime;

	public int NeedCostTime;

	public int NpcId;

	private Avatar player => Tools.instance.getPlayer();

	public bool BingNpc(int npcId)
	{
		CalcNeedCostTime(npcId);
		if (NeedCostTime >= 60)
		{
			UnBingNpc();
			return false;
		}
		NpcId = npcId;
		HasCostTime = 0;
		return true;
	}

	public void UnBingNpc()
	{
		NpcId = 0;
		HasCostTime = 0;
		NeedCostTime = 0;
	}

	public void AddNeedItem(BaseItem item)
	{
		needItemList.Add(item);
	}

	public void CalcNeedCostTime(int npcId)
	{
		NeedCostTime = 0;
		int i = jsonData.instance.AvatarJsonData[npcId.ToString()]["Level"].I;
		int time = ElderTaskItemCost.DataDict[i].time;
		if (Money % time != 0)
		{
			NeedCostTime = Money / time + 1;
		}
		else
		{
			NeedCostTime = Money / time;
		}
	}
}
