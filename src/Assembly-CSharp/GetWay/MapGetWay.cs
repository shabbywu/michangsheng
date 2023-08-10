using System.Collections.Generic;
using UnityEngine;

namespace GetWay;

public class MapGetWay : IGetWay
{
	private static MapGetWay _inst;

	public List<MapNode> OpenList;

	public List<MapNode> CloseList;

	public int CurTalk;

	public bool IsStop;

	public Dictionary<int, List<int>> Dict = new Dictionary<int, List<int>>();

	public Dictionary<int, MapNode> NodeDict = new Dictionary<int, MapNode>();

	private bool _isInit;

	public static MapGetWay Inst
	{
		get
		{
			if (_inst == null)
			{
				_inst = new MapGetWay();
			}
			return _inst;
		}
	}

	private bool Init()
	{
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)MapNodeManager.inst == (Object)null)
		{
			return false;
		}
		int childCount = MapNodeManager.inst.MapNodeParent.transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			MapComponent component = ((Component)MapNodeManager.inst.MapNodeParent.transform.GetChild(i)).GetComponent<MapComponent>();
			Dict.Add(int.Parse(((Object)((Component)component).gameObject).name), new List<int>(component.nextIndex));
			NodeDict.Add(component.NodeIndex, new MapNode(component.NodeIndex, ((Component)component).transform.position.x, ((Component)component).transform.position.y));
		}
		_isInit = true;
		return true;
	}

	public List<int> GetBestList(int startIndex, int tagetIndex)
	{
		if (!_isInit && !Init())
		{
			Debug.LogError((object)"MapNodeManager并未初始化");
			return null;
		}
		List<List<int>> list = new List<List<int>>();
		foreach (int item in Dict[startIndex])
		{
			list.Add(GetNearlyBest(startIndex, item, tagetIndex));
		}
		List<int> list2 = null;
		foreach (List<int> item2 in list)
		{
			if (item2 != null)
			{
				if (list2 == null)
				{
					list2 = item2;
				}
				else if (list2.Count > item2.Count)
				{
					list2 = item2;
				}
			}
		}
		if (list2 == null)
		{
			Debug.LogError((object)"无法前往");
		}
		return list2;
	}

	public List<int> GetNearlyBest(int baseIndex, int startIndex, int tagetIndex)
	{
		MapNode mapNode = NodeDict[startIndex];
		MapNode mapNode2 = NodeDict[tagetIndex];
		foreach (int key in NodeDict.Keys)
		{
			NodeDict[key].Parent = null;
			NodeDict[key].F = 0f;
		}
		CloseList = new List<MapNode>();
		OpenList = new List<MapNode>();
		CloseList.Add(mapNode);
		do
		{
			GetFindNearlyNode(mapNode.Index, tagetIndex);
			OpenList.Sort(SortOpenList);
			if (OpenList.Count < 1)
			{
				return null;
			}
			CloseList.Add(OpenList[0]);
			mapNode = OpenList[0];
			OpenList.RemoveAt(0);
		}
		while (mapNode != mapNode2);
		List<int> list = new List<int>();
		while (mapNode2.Parent != null)
		{
			list.Add(mapNode2.Parent.Index);
			mapNode2 = mapNode2.Parent;
		}
		list.Reverse();
		list.Add(tagetIndex);
		if (list[1] == baseIndex)
		{
			list.RemoveRange(0, 2);
		}
		return list;
	}

	public void GetFindNearlyNode(int id, int endId)
	{
		_ = NodeDict[id];
		new List<MapNode>();
		foreach (int item in Dict[id])
		{
			MapNode mapNode = NodeDict[item];
			if (!CloseList.Contains(mapNode) && !OpenList.Contains(mapNode))
			{
				mapNode.Parent = NodeDict[id];
				mapNode.F += 1f;
				OpenList.Add(mapNode);
			}
		}
	}

	public void StopAuToMove()
	{
		Debug.Log((object)"终止寻路");
		Inst.IsStop = true;
		MapMoveTips.Hide();
	}

	public bool IsNearly(int index1, int index2)
	{
		if (!_isInit && !Init())
		{
			Debug.LogError((object)"MapNodeManager并未初始化");
			return false;
		}
		return Dict[index1].Contains(index2);
	}

	private int SortOpenList(MapNode a, MapNode b)
	{
		if (a.F >= b.F)
		{
			return 1;
		}
		return -1;
	}
}
