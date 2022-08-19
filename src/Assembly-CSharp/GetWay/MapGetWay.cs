using System;
using System.Collections.Generic;
using UnityEngine;

namespace GetWay
{
	// Token: 0x0200073D RID: 1853
	public class MapGetWay : IGetWay
	{
		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06003B06 RID: 15110 RVA: 0x00195E53 File Offset: 0x00194053
		public static MapGetWay Inst
		{
			get
			{
				if (MapGetWay._inst == null)
				{
					MapGetWay._inst = new MapGetWay();
				}
				return MapGetWay._inst;
			}
		}

		// Token: 0x06003B07 RID: 15111 RVA: 0x00195E6C File Offset: 0x0019406C
		private bool Init()
		{
			if (MapNodeManager.inst == null)
			{
				return false;
			}
			int childCount = MapNodeManager.inst.MapNodeParent.transform.childCount;
			for (int i = 0; i < childCount; i++)
			{
				MapComponent component = MapNodeManager.inst.MapNodeParent.transform.GetChild(i).GetComponent<MapComponent>();
				this.Dict.Add(int.Parse(component.gameObject.name), new List<int>(component.nextIndex));
				this.NodeDict.Add(component.NodeIndex, new MapNode(component.NodeIndex, component.transform.position.x, component.transform.position.y));
			}
			this._isInit = true;
			return true;
		}

		// Token: 0x06003B08 RID: 15112 RVA: 0x00195F34 File Offset: 0x00194134
		public List<int> GetBestList(int startIndex, int tagetIndex)
		{
			if (!this._isInit && !this.Init())
			{
				Debug.LogError("MapNodeManager并未初始化");
				return null;
			}
			List<List<int>> list = new List<List<int>>();
			foreach (int startIndex2 in this.Dict[startIndex])
			{
				list.Add(this.GetNearlyBest(startIndex, startIndex2, tagetIndex));
			}
			List<int> list2 = null;
			foreach (List<int> list3 in list)
			{
				if (list3 != null)
				{
					if (list2 == null)
					{
						list2 = list3;
					}
					else if (list2.Count > list3.Count)
					{
						list2 = list3;
					}
				}
			}
			if (list2 == null)
			{
				Debug.LogError("无法前往");
			}
			return list2;
		}

		// Token: 0x06003B09 RID: 15113 RVA: 0x00196020 File Offset: 0x00194220
		public List<int> GetNearlyBest(int baseIndex, int startIndex, int tagetIndex)
		{
			MapNode mapNode = this.NodeDict[startIndex];
			MapNode mapNode2 = this.NodeDict[tagetIndex];
			foreach (int key in this.NodeDict.Keys)
			{
				this.NodeDict[key].Parent = null;
				this.NodeDict[key].F = 0f;
			}
			this.CloseList = new List<MapNode>();
			this.OpenList = new List<MapNode>();
			this.CloseList.Add(mapNode);
			for (;;)
			{
				this.GetFindNearlyNode(mapNode.Index, tagetIndex);
				this.OpenList.Sort(new Comparison<MapNode>(this.SortOpenList));
				if (this.OpenList.Count < 1)
				{
					break;
				}
				this.CloseList.Add(this.OpenList[0]);
				mapNode = this.OpenList[0];
				this.OpenList.RemoveAt(0);
				if (mapNode == mapNode2)
				{
					goto Block_3;
				}
			}
			return null;
			Block_3:
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

		// Token: 0x06003B0A RID: 15114 RVA: 0x0019618C File Offset: 0x0019438C
		public void GetFindNearlyNode(int id, int endId)
		{
			MapNode mapNode = this.NodeDict[id];
			new List<MapNode>();
			foreach (int key in this.Dict[id])
			{
				MapNode mapNode2 = this.NodeDict[key];
				if (!this.CloseList.Contains(mapNode2) && !this.OpenList.Contains(mapNode2))
				{
					mapNode2.Parent = this.NodeDict[id];
					mapNode2.F += 1f;
					this.OpenList.Add(mapNode2);
				}
			}
		}

		// Token: 0x06003B0B RID: 15115 RVA: 0x0019624C File Offset: 0x0019444C
		public void StopAuToMove()
		{
			Debug.Log("终止寻路");
			MapGetWay.Inst.IsStop = true;
			MapMoveTips.Hide();
		}

		// Token: 0x06003B0C RID: 15116 RVA: 0x00196268 File Offset: 0x00194468
		public bool IsNearly(int index1, int index2)
		{
			if (!this._isInit && !this.Init())
			{
				Debug.LogError("MapNodeManager并未初始化");
				return false;
			}
			return this.Dict[index1].Contains(index2);
		}

		// Token: 0x06003B0D RID: 15117 RVA: 0x00196298 File Offset: 0x00194498
		private int SortOpenList(MapNode a, MapNode b)
		{
			if (a.F >= b.F)
			{
				return 1;
			}
			return -1;
		}

		// Token: 0x04003337 RID: 13111
		private static MapGetWay _inst;

		// Token: 0x04003338 RID: 13112
		public List<MapNode> OpenList;

		// Token: 0x04003339 RID: 13113
		public List<MapNode> CloseList;

		// Token: 0x0400333A RID: 13114
		public int CurTalk;

		// Token: 0x0400333B RID: 13115
		public bool IsStop;

		// Token: 0x0400333C RID: 13116
		public Dictionary<int, List<int>> Dict = new Dictionary<int, List<int>>();

		// Token: 0x0400333D RID: 13117
		public Dictionary<int, MapNode> NodeDict = new Dictionary<int, MapNode>();

		// Token: 0x0400333E RID: 13118
		private bool _isInit;
	}
}
