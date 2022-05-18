using System;
using System.Collections.Generic;
using UnityEngine;

namespace GetWay
{
	// Token: 0x02000AD5 RID: 2773
	public class MapGetWay : IGetWay
	{
		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x060046BC RID: 18108 RVA: 0x0003270D File Offset: 0x0003090D
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

		// Token: 0x060046BD RID: 18109 RVA: 0x001E4440 File Offset: 0x001E2640
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

		// Token: 0x060046BE RID: 18110 RVA: 0x001E4508 File Offset: 0x001E2708
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

		// Token: 0x060046BF RID: 18111 RVA: 0x001E45F4 File Offset: 0x001E27F4
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

		// Token: 0x060046C0 RID: 18112 RVA: 0x001E4760 File Offset: 0x001E2960
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

		// Token: 0x060046C1 RID: 18113 RVA: 0x00032725 File Offset: 0x00030925
		public void StopAuToMove()
		{
			Debug.Log("终止寻路");
			MapGetWay.Inst.IsStop = true;
			MapMoveTips.Hide();
		}

		// Token: 0x060046C2 RID: 18114 RVA: 0x00032741 File Offset: 0x00030941
		public bool IsNearly(int index1, int index2)
		{
			if (!this._isInit && !this.Init())
			{
				Debug.LogError("MapNodeManager并未初始化");
				return false;
			}
			return this.Dict[index1].Contains(index2);
		}

		// Token: 0x060046C3 RID: 18115 RVA: 0x00032771 File Offset: 0x00030971
		private int SortOpenList(MapNode a, MapNode b)
		{
			if (a.F >= b.F)
			{
				return 1;
			}
			return -1;
		}

		// Token: 0x04003ED3 RID: 16083
		private static MapGetWay _inst;

		// Token: 0x04003ED4 RID: 16084
		public List<MapNode> OpenList;

		// Token: 0x04003ED5 RID: 16085
		public List<MapNode> CloseList;

		// Token: 0x04003ED6 RID: 16086
		public int CurTalk;

		// Token: 0x04003ED7 RID: 16087
		public bool IsStop;

		// Token: 0x04003ED8 RID: 16088
		public Dictionary<int, List<int>> Dict = new Dictionary<int, List<int>>();

		// Token: 0x04003ED9 RID: 16089
		public Dictionary<int, MapNode> NodeDict = new Dictionary<int, MapNode>();

		// Token: 0x04003EDA RID: 16090
		private bool _isInit;
	}
}
