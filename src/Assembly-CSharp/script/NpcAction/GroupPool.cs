using System;
using System.Collections.Generic;

namespace script.NpcAction
{
	// Token: 0x02000ABD RID: 2749
	public class GroupPool
	{
		// Token: 0x06004645 RID: 17989 RVA: 0x001DF598 File Offset: 0x001DD798
		public GroupPool()
		{
			this.Pools = new List<NpcDataGroup>();
			for (int i = 0; i < Loom.maxThreads; i++)
			{
				this.Pools.Add(new NpcDataGroup());
			}
		}

		// Token: 0x06004646 RID: 17990 RVA: 0x001DF5D8 File Offset: 0x001DD7D8
		public NpcDataGroup GetGroup()
		{
			foreach (NpcDataGroup npcDataGroup in this.Pools)
			{
				if (npcDataGroup.IsFree)
				{
					npcDataGroup.NpcDict = new Dictionary<int, NpcData>();
					npcDataGroup.IsFree = false;
					return npcDataGroup;
				}
			}
			NpcDataGroup npcDataGroup2 = new NpcDataGroup();
			npcDataGroup2.IsFree = false;
			this.Pools.Add(npcDataGroup2);
			return npcDataGroup2;
		}

		// Token: 0x06004647 RID: 17991 RVA: 0x00032392 File Offset: 0x00030592
		public void BackGroup(NpcDataGroup group)
		{
			group.IsFree = true;
		}

		// Token: 0x04003E61 RID: 15969
		public List<NpcDataGroup> Pools;
	}
}
