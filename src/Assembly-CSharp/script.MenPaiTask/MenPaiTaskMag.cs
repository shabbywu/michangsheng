using System;
using System.Collections.Generic;
using JSONClass;
using KBEngine;

namespace script.MenPaiTask;

[Serializable]
public class MenPaiTaskMag
{
	public int NowTaskId;

	public DateTime StartTime;

	public DateTime NextTime;

	public bool IsInit;

	public Dictionary<int, int> TaskDict;

	private void InitTaskDict()
	{
		TaskDict = new Dictionary<int, int>();
		TaskDict.Add(1, 0);
		TaskDict.Add(3, 1);
		TaskDict.Add(4, 2);
		TaskDict.Add(5, 3);
		TaskDict.Add(6, 4);
	}

	public void Init()
	{
	}

	public void SendTask(int npcId)
	{
		int taskId = GetTaskId();
		Avatar player = Tools.instance.getPlayer();
		if (!player.nomelTaskMag.IsNTaskStart(taskId))
		{
			player.nomelTaskMag.StartNTask(taskId);
			NowTaskId = taskId;
			int chengHao = Tools.instance.getPlayer().chengHao;
			int cD = MenPaiFengLuBiao.DataDict[chengHao].CD;
			player.emailDateMag.AuToSendToPlayer(npcId, 995, 995, NpcJieSuanManager.inst.JieSuanTime);
			while (NextTime <= NpcJieSuanManager.inst.GetNowTime() && cD > 0)
			{
				NextTime = NextTime.AddMonths(cD);
			}
		}
	}

	private int GetTaskId()
	{
		if (TaskDict == null || TaskDict.Count < 1)
		{
			InitTaskDict();
		}
		Avatar player = Tools.instance.getPlayer();
		return MenPaiFengLuBiao.DataDict[player.chengHao].RenWu[TaskDict[player.menPai]];
	}

	public bool CheckNeedSend()
	{
		Avatar player = Tools.instance.getPlayer();
		if (player.menPai < 1 || player.chengHao < 6 || player.chengHao > 9)
		{
			return false;
		}
		if (NpcJieSuanManager.inst.GetNowTime() < NextTime)
		{
			return false;
		}
		return true;
	}
}
