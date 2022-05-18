using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002DA RID: 730
public class SeaNodeData
{
	// Token: 0x06001603 RID: 5635 RVA: 0x000C6124 File Offset: 0x000C4324
	public SeaNodeData(int nodeIndex)
	{
		this.NodeIndex = nodeIndex;
		this.SmallSeaID = SeaEx.GetSmallSeaIDByNodeIndex(nodeIndex);
		this.NodePos = SeaEx.GetNodePosByNodeIndex(nodeIndex);
		this.SmallSeaPos = SeaEx.GetSmallSeaPosByNodePos(this.NodePos);
		this.SmallSeaDangerLevel = SeaEx.SmallSeaSafeLevelDict[this.SmallSeaID];
		foreach (KeyValuePair<int, List<int>> keyValuePair in SeaEx.BigSeaHasSmallSeaIDDict)
		{
			if (keyValuePair.Value.Contains(this.SmallSeaID))
			{
				this.BigSeaID = keyValuePair.Key;
				break;
			}
		}
	}

	// Token: 0x040011D5 RID: 4565
	public int NodeIndex;

	// Token: 0x040011D6 RID: 4566
	public Vector2Int NodePos;

	// Token: 0x040011D7 RID: 4567
	public int SmallSeaID;

	// Token: 0x040011D8 RID: 4568
	public int BigSeaID;

	// Token: 0x040011D9 RID: 4569
	public Vector2Int SmallSeaPos;

	// Token: 0x040011DA RID: 4570
	public int SmallSeaDangerLevel;
}
