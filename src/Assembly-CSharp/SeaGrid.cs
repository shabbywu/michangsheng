using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003CD RID: 973
[ExecuteInEditMode]
public class SeaGrid : FubenGrid
{
	// Token: 0x06001FB7 RID: 8119 RVA: 0x000DF94C File Offset: 0x000DDB4C
	private void Awake()
	{
		WASDMove.DelMoreComponent(base.gameObject);
		base.gameObject.AddComponent<WASDMove>();
	}

	// Token: 0x06001FB8 RID: 8120 RVA: 0x000DF968 File Offset: 0x000DDB68
	public List<int> HaiYuToindex()
	{
		List<int> list = new List<int>();
		new FuBenMap(7, 7);
		foreach (int index in this.HaiYuIDList)
		{
			int num = 1;
			for (int i = 0; i < 7; i++)
			{
				for (int j = 0; j < 7; j++)
				{
					int indexX = FuBenMap.getIndexX(index, EndlessSeaMag.MapWide / 7);
					int indexY = FuBenMap.getIndexY(index, EndlessSeaMag.MapWide / 7);
					int indexX2 = FuBenMap.getIndexX(num, 7);
					int indexY2 = FuBenMap.getIndexY(num, 7);
					int x = indexX * 7 + indexX2;
					int y = indexY * 7 + indexY2;
					int index2 = FuBenMap.getIndex(x, y, EndlessSeaMag.MapWide);
					list.Add(index2);
					num++;
				}
			}
		}
		return list;
	}

	// Token: 0x06001FB9 RID: 8121 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x06001FBA RID: 8122 RVA: 0x000DFA48 File Offset: 0x000DDC48
	public override void addNodeIndex(int index, MapInstComport comp)
	{
		foreach (int num in this.DestroyNode)
		{
			if (index == num || comp.NodeIndex == num)
			{
				return;
			}
		}
		foreach (Vector2 vector in this.RemoveLine)
		{
			if ((float)comp.NodeIndex == vector.x && (float)index == vector.y)
			{
				return;
			}
		}
		if (index > 0)
		{
			comp.nextIndex.Add(index);
		}
	}

	// Token: 0x040019CC RID: 6604
	[Tooltip("当前场景拥有的海域")]
	public List<int> HaiYuIDList;

	// Token: 0x040019CD RID: 6605
	public GameObject SeaStaticNode;

	// Token: 0x040019CE RID: 6606
	public GameObject ToOtherScene;

	// Token: 0x040019CF RID: 6607
	public List<SeaGrid.ToOhtherSceneNode> ToOtherSceneList = new List<SeaGrid.ToOhtherSceneNode>();

	// Token: 0x02001379 RID: 4985
	[Serializable]
	public class ToOhtherSceneNode
	{
		// Token: 0x06007C1C RID: 31772 RVA: 0x002C34E4 File Offset: 0x002C16E4
		public List<int> getToSceneList()
		{
			List<int> list = new List<int>();
			int indexX = FuBenMap.getIndexX(this.SeaID, EndlessSeaMag.MapWide / 7);
			int indexY = FuBenMap.getIndexY(this.SeaID, EndlessSeaMag.MapWide / 7);
			for (int i = 0; i < 7; i++)
			{
				int num = 0;
				int num2 = 0;
				switch (this.fangxiang)
				{
				case SeaGrid.ToOhtherSceneNode.FangXiang.UP:
					num = i;
					num2 = 0;
					break;
				case SeaGrid.ToOhtherSceneNode.FangXiang.Down:
					num = i;
					num2 = 6;
					break;
				case SeaGrid.ToOhtherSceneNode.FangXiang.Left:
					num = 0;
					num2 = i;
					break;
				case SeaGrid.ToOhtherSceneNode.FangXiang.Right:
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

		// Token: 0x04006896 RID: 26774
		public int SeaID;

		// Token: 0x04006897 RID: 26775
		public string ToSceneName;

		// Token: 0x04006898 RID: 26776
		public string fungusName;

		// Token: 0x04006899 RID: 26777
		public SeaGrid.ToOhtherSceneNode.FangXiang fangxiang;

		// Token: 0x02001755 RID: 5973
		public enum FangXiang
		{
			// Token: 0x04007592 RID: 30098
			UP,
			// Token: 0x04007593 RID: 30099
			Down,
			// Token: 0x04007594 RID: 30100
			Left,
			// Token: 0x04007595 RID: 30101
			Right
		}
	}
}
