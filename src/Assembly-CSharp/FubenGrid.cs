using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200055D RID: 1373
[ExecuteInEditMode]
public class FubenGrid : MonoBehaviour
{
	// Token: 0x06002317 RID: 8983 RVA: 0x0001C87C File Offset: 0x0001AA7C
	private void Awake()
	{
		WASDMove.DelMoreComponent(base.gameObject);
		if (Application.isPlaying)
		{
			base.gameObject.AddComponent<WASDMove>();
		}
	}

	// Token: 0x06002318 RID: 8984 RVA: 0x00121D68 File Offset: 0x0011FF68
	private void Update()
	{
		float num = 0f;
		float num2 = 0f;
		int num3 = 0;
		foreach (object obj in base.transform)
		{
			Transform transform = (Transform)obj;
			num3++;
			transform.localPosition = new Vector3(num, num2, 0f);
			num += this.intervalX;
			if (num3 >= this.num)
			{
				num = 0f;
				num2 += this.intervalY;
				num3 = 0;
			}
			transform.GetComponent<MapInstComport>().showDebugLine();
		}
	}

	// Token: 0x06002319 RID: 8985 RVA: 0x00121E10 File Offset: 0x00120010
	public virtual void addNodeIndex(int index, MapInstComport comp)
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
		if (index > 0 && index <= base.transform.childCount)
		{
			comp.nextIndex.Add(index);
		}
	}

	// Token: 0x04001E2C RID: 7724
	[Tooltip("列间距")]
	public float intervalX;

	// Token: 0x04001E2D RID: 7725
	[Tooltip("行间距")]
	public float intervalY;

	// Token: 0x04001E2E RID: 7726
	[Tooltip("每行的数量")]
	public int num;

	// Token: 0x04001E2F RID: 7727
	[Tooltip("进行初始化，只有在没有任何子节点的时候才会生效")]
	public bool initGrid;

	// Token: 0x04001E30 RID: 7728
	[Tooltip("自动创建的子节点的基类")]
	public GameObject NodeTemp;

	// Token: 0x04001E31 RID: 7729
	[Tooltip("创建的子节点个数")]
	public int creatNum;

	// Token: 0x04001E32 RID: 7730
	[Tooltip("初始化固定场景")]
	public List<FubenGrid.StaticNodeInfo> StaticNodeList = new List<FubenGrid.StaticNodeInfo>();

	// Token: 0x04001E33 RID: 7731
	[Tooltip("需要删除的通道")]
	public List<Vector2> RemoveLine = new List<Vector2>();

	// Token: 0x04001E34 RID: 7732
	[Tooltip("完全不能走的点")]
	public List<int> DestroyNode = new List<int>();

	// Token: 0x0200055E RID: 1374
	[Serializable]
	public class StaticNodeInfo
	{
		// Token: 0x04001E35 RID: 7733
		public string Name = "";

		// Token: 0x04001E36 RID: 7734
		public int index;

		// Token: 0x04001E37 RID: 7735
		public Sprite Image;
	}
}
