using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

// Token: 0x020001CE RID: 462
public static class SeaEx
{
	// Token: 0x0600133F RID: 4927 RVA: 0x000790E4 File Offset: 0x000772E4
	public static void Init()
	{
		if (!SeaEx.inited)
		{
			SeaEx.inited = true;
			SeaEx.seaNodeDatas = new SeaNodeData[SeaEx.totalSeaNodeCount];
			foreach (KeyValuePair<string, JToken> keyValuePair in jsonData.instance.EndlessSeaHaiYuData)
			{
				int key = (int)keyValuePair.Value["id"];
				SeaEx.BigSeaHasSmallSeaIDDict.Add(key, new List<int>());
				foreach (JToken jtoken in ((JArray)keyValuePair.Value["shuxing"]))
				{
					SeaEx.BigSeaHasSmallSeaIDDict[key].Add((int)jtoken);
				}
			}
			foreach (KeyValuePair<string, JToken> keyValuePair2 in jsonData.instance.EndlessSeaType)
			{
				int key2 = (int)keyValuePair2.Value["id"];
				int value = (int)keyValuePair2.Value["weixianLv"];
				SeaEx.SmallSeaSafeLevelDict[key2] = value;
			}
		}
	}

	// Token: 0x06001340 RID: 4928 RVA: 0x0007924C File Offset: 0x0007744C
	public static SeaNodeData GetSeaNodeDataByNodeIndex(int nodeIndex)
	{
		SeaEx.Init();
		if (nodeIndex < 1 || nodeIndex > SeaEx.seaNodeDatas.Length)
		{
			Debug.LogError(string.Format("获取海上点{0}的数据时出错，海上数据列表长度为{1}", nodeIndex, SeaEx.seaNodeDatas.Length));
			return null;
		}
		if (SeaEx.seaNodeDatas[nodeIndex - 1] == null)
		{
			SeaEx.seaNodeDatas[nodeIndex - 1] = new SeaNodeData(nodeIndex);
		}
		return SeaEx.seaNodeDatas[nodeIndex - 1];
	}

	// Token: 0x06001341 RID: 4929 RVA: 0x000792B2 File Offset: 0x000774B2
	public static SeaNodeData GetSeaNodeDataByNodePos(Vector2Int nodePos)
	{
		return SeaEx.GetSeaNodeDataByNodeIndex(SeaEx.GetNodeIndexByNodePos(nodePos));
	}

	// Token: 0x06001342 RID: 4930 RVA: 0x000792C0 File Offset: 0x000774C0
	public static int GetSmallSeaIDByNodeIndex(int nodeIndex)
	{
		Vector2Int smallSeaPosByNodePos = SeaEx.GetSmallSeaPosByNodePos(SeaEx.GetNodePosByNodeIndex(nodeIndex));
		return SeaEx.bigSeaColCount / SeaEx.smallSeaSideLen * smallSeaPosByNodePos.y + smallSeaPosByNodePos.x + 1;
	}

	// Token: 0x06001343 RID: 4931 RVA: 0x000792F8 File Offset: 0x000774F8
	public static Vector2Int GetNodePosByNodeIndex(int nodeIndex)
	{
		int num = (nodeIndex - 1) % SeaEx.bigSeaColCount;
		int num2 = (nodeIndex - 1) / SeaEx.bigSeaColCount;
		return new Vector2Int(num, num2);
	}

	// Token: 0x06001344 RID: 4932 RVA: 0x0007931E File Offset: 0x0007751E
	public static int GetNodeIndexByNodePos(Vector2Int nodePos)
	{
		return nodePos.y * SeaEx.bigSeaColCount + nodePos.x + 1;
	}

	// Token: 0x06001345 RID: 4933 RVA: 0x00079338 File Offset: 0x00077538
	public static Vector2Int GetSmallSeaPosByNodePos(Vector2Int nodePos)
	{
		int num = nodePos.x / SeaEx.smallSeaSideLen;
		int num2 = nodePos.y / SeaEx.smallSeaSideLen;
		return new Vector2Int(num, num2);
	}

	// Token: 0x04000E8C RID: 3724
	private static int smallSeaSideLen = 7;

	// Token: 0x04000E8D RID: 3725
	private static int smallSeaColCount = 19;

	// Token: 0x04000E8E RID: 3726
	private static int smallSeaRowCount = 10;

	// Token: 0x04000E8F RID: 3727
	private static int bigSeaColCount = SeaEx.smallSeaColCount * SeaEx.smallSeaSideLen;

	// Token: 0x04000E90 RID: 3728
	private static int bigSeaRowCount = SeaEx.smallSeaRowCount * SeaEx.smallSeaSideLen;

	// Token: 0x04000E91 RID: 3729
	private static int totalSeaNodeCount = SeaEx.bigSeaColCount * SeaEx.bigSeaRowCount;

	// Token: 0x04000E92 RID: 3730
	private static SeaNodeData[] seaNodeDatas;

	// Token: 0x04000E93 RID: 3731
	public static Dictionary<int, List<int>> BigSeaHasSmallSeaIDDict = new Dictionary<int, List<int>>();

	// Token: 0x04000E94 RID: 3732
	public static Dictionary<int, int> SmallSeaSafeLevelDict = new Dictionary<int, int>();

	// Token: 0x04000E95 RID: 3733
	private static bool inited;
}
