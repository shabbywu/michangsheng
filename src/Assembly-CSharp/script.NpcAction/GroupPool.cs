using System.Collections.Generic;

namespace script.NpcAction;

public class GroupPool
{
	public List<NpcDataGroup> Pools;

	public GroupPool()
	{
		Pools = new List<NpcDataGroup>();
		for (int i = 0; i < Loom.maxThreads; i++)
		{
			Pools.Add(new NpcDataGroup());
		}
	}

	public NpcDataGroup GetGroup()
	{
		foreach (NpcDataGroup pool in Pools)
		{
			if (pool.IsFree)
			{
				return pool;
			}
		}
		NpcDataGroup npcDataGroup = new NpcDataGroup();
		npcDataGroup.IsFree = false;
		Pools.Add(npcDataGroup);
		return npcDataGroup;
	}

	public void BackGroup(NpcDataGroup group)
	{
		group.Clear();
	}
}
