using System.Collections.Generic;
using UnityEngine;

public class SeaNodeData
{
	public int NodeIndex;

	public Vector2Int NodePos;

	public int SmallSeaID;

	public int BigSeaID;

	public Vector2Int SmallSeaPos;

	public int SmallSeaDangerLevel;

	public SeaNodeData(int nodeIndex)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		NodeIndex = nodeIndex;
		SmallSeaID = SeaEx.GetSmallSeaIDByNodeIndex(nodeIndex);
		NodePos = SeaEx.GetNodePosByNodeIndex(nodeIndex);
		SmallSeaPos = SeaEx.GetSmallSeaPosByNodePos(NodePos);
		SmallSeaDangerLevel = SeaEx.SmallSeaSafeLevelDict[SmallSeaID];
		foreach (KeyValuePair<int, List<int>> item in SeaEx.BigSeaHasSmallSeaIDDict)
		{
			if (item.Value.Contains(SmallSeaID))
			{
				BigSeaID = item.Key;
				break;
			}
		}
	}
}
