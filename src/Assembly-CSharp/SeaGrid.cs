using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000562 RID: 1378
[ExecuteInEditMode]
public class SeaGrid : FubenGrid
{
	// Token: 0x0600232E RID: 9006 RVA: 0x0001C953 File Offset: 0x0001AB53
	private void Awake()
	{
		WASDMove.DelMoreComponent(base.gameObject);
		base.gameObject.AddComponent<WASDMove>();
	}

	// Token: 0x0600232F RID: 9007 RVA: 0x001223C4 File Offset: 0x001205C4
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

	// Token: 0x06002330 RID: 9008 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x06002331 RID: 9009 RVA: 0x001224A4 File Offset: 0x001206A4
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

	// Token: 0x04001E4D RID: 7757
	[Tooltip("当前场景拥有的海域")]
	public List<int> HaiYuIDList;

	// Token: 0x04001E4E RID: 7758
	public GameObject SeaStaticNode;

	// Token: 0x04001E4F RID: 7759
	public GameObject ToOtherScene;

	// Token: 0x04001E50 RID: 7760
	public List<SeaGrid.ToOhtherSceneNode> ToOtherSceneList = new List<SeaGrid.ToOhtherSceneNode>();

	// Token: 0x02000563 RID: 1379
	[Serializable]
	public class ToOhtherSceneNode
	{
		// Token: 0x06002333 RID: 9011 RVA: 0x00122568 File Offset: 0x00120768
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

		// Token: 0x04001E51 RID: 7761
		public int SeaID;

		// Token: 0x04001E52 RID: 7762
		public string ToSceneName;

		// Token: 0x04001E53 RID: 7763
		public string fungusName;

		// Token: 0x04001E54 RID: 7764
		public SeaGrid.ToOhtherSceneNode.FangXiang fangxiang;

		// Token: 0x02000564 RID: 1380
		public enum FangXiang
		{
			// Token: 0x04001E56 RID: 7766
			UP,
			// Token: 0x04001E57 RID: 7767
			Down,
			// Token: 0x04001E58 RID: 7768
			Left,
			// Token: 0x04001E59 RID: 7769
			Right
		}
	}
}
