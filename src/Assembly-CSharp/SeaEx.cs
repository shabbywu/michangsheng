using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

public static class SeaEx
{
	private static int smallSeaSideLen = 7;

	private static int smallSeaColCount = 19;

	private static int smallSeaRowCount = 10;

	private static int bigSeaColCount = smallSeaColCount * smallSeaSideLen;

	private static int bigSeaRowCount = smallSeaRowCount * smallSeaSideLen;

	private static int totalSeaNodeCount = bigSeaColCount * bigSeaRowCount;

	private static SeaNodeData[] seaNodeDatas;

	public static Dictionary<int, List<int>> BigSeaHasSmallSeaIDDict = new Dictionary<int, List<int>>();

	public static Dictionary<int, int> SmallSeaSafeLevelDict = new Dictionary<int, int>();

	private static bool inited;

	public static void Init()
	{
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		if (inited)
		{
			return;
		}
		inited = true;
		seaNodeDatas = new SeaNodeData[totalSeaNodeCount];
		foreach (KeyValuePair<string, JToken> endlessSeaHaiYuDatum in jsonData.instance.EndlessSeaHaiYuData)
		{
			int key = (int)endlessSeaHaiYuDatum.Value[(object)"id"];
			BigSeaHasSmallSeaIDDict.Add(key, new List<int>());
			foreach (JToken item in (JArray)endlessSeaHaiYuDatum.Value[(object)"shuxing"])
			{
				BigSeaHasSmallSeaIDDict[key].Add((int)item);
			}
		}
		foreach (KeyValuePair<string, JToken> item2 in jsonData.instance.EndlessSeaType)
		{
			int key2 = (int)item2.Value[(object)"id"];
			int value = (int)item2.Value[(object)"weixianLv"];
			SmallSeaSafeLevelDict[key2] = value;
		}
	}

	public static SeaNodeData GetSeaNodeDataByNodeIndex(int nodeIndex)
	{
		Init();
		if (nodeIndex < 1 || nodeIndex > seaNodeDatas.Length)
		{
			Debug.LogError((object)$"获取海上点{nodeIndex}的数据时出错，海上数据列表长度为{seaNodeDatas.Length}");
			return null;
		}
		if (seaNodeDatas[nodeIndex - 1] == null)
		{
			seaNodeDatas[nodeIndex - 1] = new SeaNodeData(nodeIndex);
		}
		return seaNodeDatas[nodeIndex - 1];
	}

	public static SeaNodeData GetSeaNodeDataByNodePos(Vector2Int nodePos)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return GetSeaNodeDataByNodeIndex(GetNodeIndexByNodePos(nodePos));
	}

	public static int GetSmallSeaIDByNodeIndex(int nodeIndex)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		Vector2Int smallSeaPosByNodePos = GetSmallSeaPosByNodePos(GetNodePosByNodeIndex(nodeIndex));
		return bigSeaColCount / smallSeaSideLen * ((Vector2Int)(ref smallSeaPosByNodePos)).y + ((Vector2Int)(ref smallSeaPosByNodePos)).x + 1;
	}

	public static Vector2Int GetNodePosByNodeIndex(int nodeIndex)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		int num = (nodeIndex - 1) % bigSeaColCount;
		int num2 = (nodeIndex - 1) / bigSeaColCount;
		return new Vector2Int(num, num2);
	}

	public static int GetNodeIndexByNodePos(Vector2Int nodePos)
	{
		return ((Vector2Int)(ref nodePos)).y * bigSeaColCount + ((Vector2Int)(ref nodePos)).x + 1;
	}

	public static Vector2Int GetSmallSeaPosByNodePos(Vector2Int nodePos)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		int num = ((Vector2Int)(ref nodePos)).x / smallSeaSideLen;
		int num2 = ((Vector2Int)(ref nodePos)).y / smallSeaSideLen;
		return new Vector2Int(num, num2);
	}
}
