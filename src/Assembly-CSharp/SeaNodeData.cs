using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001CF RID: 463
public class SeaNodeData
{
	// Token: 0x06001347 RID: 4935 RVA: 0x000793D0 File Offset: 0x000775D0
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

	// Token: 0x04000E96 RID: 3734
	public int NodeIndex;

	// Token: 0x04000E97 RID: 3735
	public Vector2Int NodePos;

	// Token: 0x04000E98 RID: 3736
	public int SmallSeaID;

	// Token: 0x04000E99 RID: 3737
	public int BigSeaID;

	// Token: 0x04000E9A RID: 3738
	public Vector2Int SmallSeaPos;

	// Token: 0x04000E9B RID: 3739
	public int SmallSeaDangerLevel;
}
