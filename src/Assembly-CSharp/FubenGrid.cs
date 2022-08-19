using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003C8 RID: 968
[ExecuteInEditMode]
public class FubenGrid : MonoBehaviour
{
	// Token: 0x06001F9E RID: 8094 RVA: 0x000DF174 File Offset: 0x000DD374
	private void Awake()
	{
		WASDMove.DelMoreComponent(base.gameObject);
		if (Application.isPlaying)
		{
			base.gameObject.AddComponent<WASDMove>();
		}
	}

	// Token: 0x06001F9F RID: 8095 RVA: 0x000DF194 File Offset: 0x000DD394
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

	// Token: 0x06001FA0 RID: 8096 RVA: 0x000DF23C File Offset: 0x000DD43C
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

	// Token: 0x040019AB RID: 6571
	[Tooltip("列间距")]
	public float intervalX;

	// Token: 0x040019AC RID: 6572
	[Tooltip("行间距")]
	public float intervalY;

	// Token: 0x040019AD RID: 6573
	[Tooltip("每行的数量")]
	public int num;

	// Token: 0x040019AE RID: 6574
	[Tooltip("进行初始化，只有在没有任何子节点的时候才会生效")]
	public bool initGrid;

	// Token: 0x040019AF RID: 6575
	[Tooltip("自动创建的子节点的基类")]
	public GameObject NodeTemp;

	// Token: 0x040019B0 RID: 6576
	[Tooltip("创建的子节点个数")]
	public int creatNum;

	// Token: 0x040019B1 RID: 6577
	[Tooltip("初始化固定场景")]
	public List<FubenGrid.StaticNodeInfo> StaticNodeList = new List<FubenGrid.StaticNodeInfo>();

	// Token: 0x040019B2 RID: 6578
	[Tooltip("需要删除的通道")]
	public List<Vector2> RemoveLine = new List<Vector2>();

	// Token: 0x040019B3 RID: 6579
	[Tooltip("完全不能走的点")]
	public List<int> DestroyNode = new List<int>();

	// Token: 0x02001378 RID: 4984
	[Serializable]
	public class StaticNodeInfo
	{
		// Token: 0x04006893 RID: 26771
		public string Name = "";

		// Token: 0x04006894 RID: 26772
		public int index;

		// Token: 0x04006895 RID: 26773
		public Sprite Image;
	}
}
