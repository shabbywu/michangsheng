using System;
using System.Collections.Generic;
using JSONClass;
using KBEngine;

namespace script.MenPaiTask
{
	// Token: 0x02000AD4 RID: 2772
	[Serializable]
	public class MenPaiTaskMag
	{
		// Token: 0x060046B6 RID: 18102 RVA: 0x001E4278 File Offset: 0x001E2478
		private void InitTaskDict()
		{
			this.TaskDict = new Dictionary<int, int>();
			this.TaskDict.Add(1, 0);
			this.TaskDict.Add(3, 1);
			this.TaskDict.Add(4, 2);
			this.TaskDict.Add(5, 3);
			this.TaskDict.Add(6, 4);
		}

		// Token: 0x060046B7 RID: 18103 RVA: 0x000042DD File Offset: 0x000024DD
		public void Init()
		{
		}

		// Token: 0x060046B8 RID: 18104 RVA: 0x001E42D4 File Offset: 0x001E24D4
		public void SendTask(int npcId)
		{
			int taskId = this.GetTaskId();
			Avatar player = Tools.instance.getPlayer();
			if (!player.nomelTaskMag.IsNTaskStart(taskId))
			{
				player.nomelTaskMag.StartNTask(taskId, 1);
				this.NowTaskId = taskId;
				int chengHao = Tools.instance.getPlayer().chengHao;
				int cd = MenPaiFengLuBiao.DataDict[chengHao - 1].CD;
				player.emailDateMag.AuToSendToPlayer(npcId, 995, 995, NpcJieSuanManager.inst.JieSuanTime, null);
				while (this.NextTime <= NpcJieSuanManager.inst.GetNowTime() && cd > 0)
				{
					this.NextTime = this.NextTime.AddMonths(cd);
				}
			}
		}

		// Token: 0x060046B9 RID: 18105 RVA: 0x001E438C File Offset: 0x001E258C
		private int GetTaskId()
		{
			if (this.TaskDict == null || this.TaskDict.Count < 1)
			{
				this.InitTaskDict();
			}
			Avatar player = Tools.instance.getPlayer();
			return MenPaiFengLuBiao.DataDict[player.chengHao - 1].RenWu[this.TaskDict[(int)player.menPai]];
		}

		// Token: 0x060046BA RID: 18106 RVA: 0x001E43F0 File Offset: 0x001E25F0
		public bool CheckNeedSend()
		{
			Avatar player = Tools.instance.getPlayer();
			return player.menPai >= 1 && player.chengHao >= 7 && player.chengHao < 11 && !(NpcJieSuanManager.inst.GetNowTime() < this.NextTime);
		}

		// Token: 0x04003ECE RID: 16078
		public int NowTaskId;

		// Token: 0x04003ECF RID: 16079
		public DateTime StartTime;

		// Token: 0x04003ED0 RID: 16080
		public DateTime NextTime;

		// Token: 0x04003ED1 RID: 16081
		public bool IsInit;

		// Token: 0x04003ED2 RID: 16082
		public Dictionary<int, int> TaskDict;
	}
}
