using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SeaGrid : FubenGrid
{
	[Serializable]
	public class ToOhtherSceneNode
	{
		public enum FangXiang
		{
			UP,
			Down,
			Left,
			Right
		}

		public int SeaID;

		public string ToSceneName;

		public string fungusName;

		public FangXiang fangxiang;

		public List<int> getToSceneList()
		{
			List<int> list = new List<int>();
			int indexX = FuBenMap.getIndexX(SeaID, EndlessSeaMag.MapWide / 7);
			int indexY = FuBenMap.getIndexY(SeaID, EndlessSeaMag.MapWide / 7);
			for (int i = 0; i < 7; i++)
			{
				int num = 0;
				int num2 = 0;
				switch (fangxiang)
				{
				case FangXiang.UP:
					num = i;
					num2 = 0;
					break;
				case FangXiang.Down:
					num = i;
					num2 = 6;
					break;
				case FangXiang.Left:
					num = 0;
					num2 = i;
					break;
				case FangXiang.Right:
					num = 6;
					num2 = i;
					break;
				}
				int x = indexX * 7 + num;
				int y = indexY * 7 + num2;
				list.Add(FuBenMap.getIndex(x, y, EndlessSeaMag.MapWide));
			}
			return list;
		}
	}

	[Tooltip("当前场景拥有的海域")]
	public List<int> HaiYuIDList;

	public GameObject SeaStaticNode;

	public GameObject ToOtherScene;

	public List<ToOhtherSceneNode> ToOtherSceneList = new List<ToOhtherSceneNode>();

	private void Awake()
	{
		WASDMove.DelMoreComponent(((Component)this).gameObject);
		((Component)this).gameObject.AddComponent<WASDMove>();
	}

	public List<int> HaiYuToindex()
	{
		List<int> list = new List<int>();
		new FuBenMap(7, 7);
		foreach (int haiYuID in HaiYuIDList)
		{
			int num = 1;
			for (int i = 0; i < 7; i++)
			{
				for (int j = 0; j < 7; j++)
				{
					int indexX = FuBenMap.getIndexX(haiYuID, EndlessSeaMag.MapWide / 7);
					int indexY = FuBenMap.getIndexY(haiYuID, EndlessSeaMag.MapWide / 7);
					int indexX2 = FuBenMap.getIndexX(num, 7);
					int indexY2 = FuBenMap.getIndexY(num, 7);
					int x = indexX * 7 + indexX2;
					int y = indexY * 7 + indexY2;
					int index = FuBenMap.getIndex(x, y, EndlessSeaMag.MapWide);
					list.Add(index);
					num++;
				}
			}
		}
		return list;
	}

	private void Update()
	{
	}

	public override void addNodeIndex(int index, MapInstComport comp)
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		foreach (int item in DestroyNode)
		{
			if (index == item || comp.NodeIndex == item)
			{
				return;
			}
		}
		foreach (Vector2 item2 in RemoveLine)
		{
			if ((float)comp.NodeIndex == item2.x && (float)index == item2.y)
			{
				return;
			}
		}
		if (index > 0)
		{
			comp.nextIndex.Add(index);
		}
	}
}
