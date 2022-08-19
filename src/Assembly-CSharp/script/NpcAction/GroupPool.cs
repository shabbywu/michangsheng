using System;
using System.Collections.Generic;

namespace script.NpcAction
{
	// Token: 0x020009F0 RID: 2544
	public class GroupPool
	{
		// Token: 0x0600468D RID: 18061 RVA: 0x001DD7C0 File Offset: 0x001DB9C0
		public GroupPool()
		{
			this.Pools = new List<NpcDataGroup>();
			for (int i = 0; i < Loom.maxThreads; i++)
			{
				this.Pools.Add(new NpcDataGroup());
			}
		}

		// Token: 0x0600468E RID: 18062 RVA: 0x001DD800 File Offset: 0x001DBA00
		public NpcDataGroup GetGroup()
		{
			foreach (NpcDataGroup npcDataGroup in this.Pools)
			{
				if (npcDataGroup.IsFree)
				{
					return npcDataGroup;
				}
			}
			NpcDataGroup npcDataGroup2 = new NpcDataGroup();
			npcDataGroup2.IsFree = false;
			this.Pools.Add(npcDataGroup2);
			return npcDataGroup2;
		}

		// Token: 0x0600468F RID: 18063 RVA: 0x001DD874 File Offset: 0x001DBA74
		public void BackGroup(NpcDataGroup group)
		{
			group.Clear();
		}

		// Token: 0x040047F2 RID: 18418
		public List<NpcDataGroup> Pools;
	}
}
