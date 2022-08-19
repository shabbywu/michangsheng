using System;
using System.Collections.Generic;
using JSONClass;
using KBEngine;

namespace script.MenPaiTask
{
	// Token: 0x02000A05 RID: 2565
	[Serializable]
	public class MenPaiTaskMag
	{
		// Token: 0x06004707 RID: 18183 RVA: 0x001E1E6C File Offset: 0x001E006C
		private void InitTaskDict()
		{
			this.TaskDict = new Dictionary<int, int>();
			this.TaskDict.Add(1, 0);
			this.TaskDict.Add(3, 1);
			this.TaskDict.Add(4, 2);
			this.TaskDict.Add(5, 3);
			this.TaskDict.Add(6, 4);
		}

		// Token: 0x06004708 RID: 18184 RVA: 0x00004095 File Offset: 0x00002295
		public void Init()
		{
		}

		// Token: 0x06004709 RID: 18185 RVA: 0x001E1EC8 File Offset: 0x001E00C8
		public void SendTask(int npcId)
		{
			int taskId = this.GetTaskId();
			Avatar player = Tools.instance.getPlayer();
			if (!player.nomelTaskMag.IsNTaskStart(taskId))
			{
				player.nomelTaskMag.StartNTask(taskId, 1);
				this.NowTaskId = taskId;
				int chengHao = Tools.instance.getPlayer().chengHao;
				int cd = MenPaiFengLuBiao.DataDict[chengHao].CD;
				player.emailDateMag.AuToSendToPlayer(npcId, 995, 995, NpcJieSuanManager.inst.JieSuanTime, null);
				while (this.NextTime <= NpcJieSuanManager.inst.GetNowTime() && cd > 0)
				{
					this.NextTime = this.NextTime.AddMonths(cd);
				}
			}
		}

		// Token: 0x0600470A RID: 18186 RVA: 0x001E1F80 File Offset: 0x001E0180
		private int GetTaskId()
		{
			if (this.TaskDict == null || this.TaskDict.Count < 1)
			{
				this.InitTaskDict();
			}
			Avatar player = Tools.instance.getPlayer();
			return MenPaiFengLuBiao.DataDict[player.chengHao].RenWu[this.TaskDict[(int)player.menPai]];
		}

		// Token: 0x0600470B RID: 18187 RVA: 0x001E1FE0 File Offset: 0x001E01E0
		public bool CheckNeedSend()
		{
			Avatar player = Tools.instance.getPlayer();
			return player.menPai >= 1 && player.chengHao >= 6 && player.chengHao <= 9 && !(NpcJieSuanManager.inst.GetNowTime() < this.NextTime);
		}

		// Token: 0x0400484F RID: 18511
		public int NowTaskId;

		// Token: 0x04004850 RID: 18512
		public DateTime StartTime;

		// Token: 0x04004851 RID: 18513
		public DateTime NextTime;

		// Token: 0x04004852 RID: 18514
		public bool IsInit;

		// Token: 0x04004853 RID: 18515
		public Dictionary<int, int> TaskDict;
	}
}
