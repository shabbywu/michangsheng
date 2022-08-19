using System;
using System.Collections.Generic;
using Bag;
using JSONClass;
using KBEngine;

namespace script.MenPaiTask
{
	// Token: 0x02000A07 RID: 2567
	[Serializable]
	public class ElderTask
	{
		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06004710 RID: 18192 RVA: 0x0006EC50 File Offset: 0x0006CE50
		private Avatar player
		{
			get
			{
				return Tools.instance.getPlayer();
			}
		}

		// Token: 0x06004711 RID: 18193 RVA: 0x001E210D File Offset: 0x001E030D
		public bool BingNpc(int npcId)
		{
			this.CalcNeedCostTime(npcId);
			if (this.NeedCostTime >= 60)
			{
				this.UnBingNpc();
				return false;
			}
			this.NpcId = npcId;
			this.HasCostTime = 0;
			return true;
		}

		// Token: 0x06004712 RID: 18194 RVA: 0x001E2137 File Offset: 0x001E0337
		public void UnBingNpc()
		{
			this.NpcId = 0;
			this.HasCostTime = 0;
			this.NeedCostTime = 0;
		}

		// Token: 0x06004713 RID: 18195 RVA: 0x001E214E File Offset: 0x001E034E
		public void AddNeedItem(BaseItem item)
		{
			this.needItemList.Add(item);
		}

		// Token: 0x06004714 RID: 18196 RVA: 0x001E215C File Offset: 0x001E035C
		public void CalcNeedCostTime(int npcId)
		{
			this.NeedCostTime = 0;
			int i = jsonData.instance.AvatarJsonData[npcId.ToString()]["Level"].I;
			int time = ElderTaskItemCost.DataDict[i].time;
			if (this.Money % time != 0)
			{
				this.NeedCostTime = this.Money / time + 1;
				return;
			}
			this.NeedCostTime = this.Money / time;
		}

		// Token: 0x04004854 RID: 18516
		public readonly List<BaseItem> needItemList = new List<BaseItem>(5);

		// Token: 0x04004855 RID: 18517
		public int Money;

		// Token: 0x04004856 RID: 18518
		public int HasCostTime;

		// Token: 0x04004857 RID: 18519
		public int NeedCostTime;

		// Token: 0x04004858 RID: 18520
		public int NpcId;
	}
}
