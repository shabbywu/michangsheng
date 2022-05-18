using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

// Token: 0x020002D9 RID: 729
public static class SeaEx
{
	// Token: 0x060015FB RID: 5627 RVA: 0x000C5E5C File Offset: 0x000C405C
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

	// Token: 0x060015FC RID: 5628 RVA: 0x000C5FC4 File Offset: 0x000C41C4
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

	// Token: 0x060015FD RID: 5629 RVA: 0x00013B9F File Offset: 0x00011D9F
	public static SeaNodeData GetSeaNodeDataByNodePos(Vector2Int nodePos)
	{
		return SeaEx.GetSeaNodeDataByNodeIndex(SeaEx.GetNodeIndexByNodePos(nodePos));
	}

	// Token: 0x060015FE RID: 5630 RVA: 0x000C602C File Offset: 0x000C422C
	public static int GetSmallSeaIDByNodeIndex(int nodeIndex)
	{
		Vector2Int smallSeaPosByNodePos = SeaEx.GetSmallSeaPosByNodePos(SeaEx.GetNodePosByNodeIndex(nodeIndex));
		return SeaEx.bigSeaColCount / SeaEx.smallSeaSideLen * smallSeaPosByNodePos.y + smallSeaPosByNodePos.x + 1;
	}

	// Token: 0x060015FF RID: 5631 RVA: 0x000C6064 File Offset: 0x000C4264
	public static Vector2Int GetNodePosByNodeIndex(int nodeIndex)
	{
		int num = (nodeIndex - 1) % SeaEx.bigSeaColCount;
		int num2 = (nodeIndex - 1) / SeaEx.bigSeaColCount;
		return new Vector2Int(num, num2);
	}

	// Token: 0x06001600 RID: 5632 RVA: 0x00013BAC File Offset: 0x00011DAC
	public static int GetNodeIndexByNodePos(Vector2Int nodePos)
	{
		return nodePos.y * SeaEx.bigSeaColCount + nodePos.x + 1;
	}

	// Token: 0x06001601 RID: 5633 RVA: 0x000C608C File Offset: 0x000C428C
	public static Vector2Int GetSmallSeaPosByNodePos(Vector2Int nodePos)
	{
		int num = nodePos.x / SeaEx.smallSeaSideLen;
		int num2 = nodePos.y / SeaEx.smallSeaSideLen;
		return new Vector2Int(num, num2);
	}

	// Token: 0x040011CB RID: 4555
	private static int smallSeaSideLen = 7;

	// Token: 0x040011CC RID: 4556
	private static int smallSeaColCount = 19;

	// Token: 0x040011CD RID: 4557
	private static int smallSeaRowCount = 10;

	// Token: 0x040011CE RID: 4558
	private static int bigSeaColCount = SeaEx.smallSeaColCount * SeaEx.smallSeaSideLen;

	// Token: 0x040011CF RID: 4559
	private static int bigSeaRowCount = SeaEx.smallSeaRowCount * SeaEx.smallSeaSideLen;

	// Token: 0x040011D0 RID: 4560
	private static int totalSeaNodeCount = SeaEx.bigSeaColCount * SeaEx.bigSeaRowCount;

	// Token: 0x040011D1 RID: 4561
	private static SeaNodeData[] seaNodeDatas;

	// Token: 0x040011D2 RID: 4562
	public static Dictionary<int, List<int>> BigSeaHasSmallSeaIDDict = new Dictionary<int, List<int>>();

	// Token: 0x040011D3 RID: 4563
	public static Dictionary<int, int> SmallSeaSafeLevelDict = new Dictionary<int, int>();

	// Token: 0x040011D4 RID: 4564
	private static bool inited;
}
