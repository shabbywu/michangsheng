using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FubenGrid : MonoBehaviour
{
	[Serializable]
	public class StaticNodeInfo
	{
		public string Name = "";

		public int index;

		public Sprite Image;
	}

	[Tooltip("列间距")]
	public float intervalX;

	[Tooltip("行间距")]
	public float intervalY;

	[Tooltip("每行的数量")]
	public int num;

	[Tooltip("进行初始化，只有在没有任何子节点的时候才会生效")]
	public bool initGrid;

	[Tooltip("自动创建的子节点的基类")]
	public GameObject NodeTemp;

	[Tooltip("创建的子节点个数")]
	public int creatNum;

	[Tooltip("初始化固定场景")]
	public List<StaticNodeInfo> StaticNodeList = new List<StaticNodeInfo>();

	[Tooltip("需要删除的通道")]
	public List<Vector2> RemoveLine = new List<Vector2>();

	[Tooltip("完全不能走的点")]
	public List<int> DestroyNode = new List<int>();

	private void Awake()
	{
		WASDMove.DelMoreComponent(((Component)this).gameObject);
		if (Application.isPlaying)
		{
			((Component)this).gameObject.AddComponent<WASDMove>();
		}
	}

	private void Update()
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		float num = 0f;
		float num2 = 0f;
		int num3 = 0;
		foreach (Transform item in ((Component)this).transform)
		{
			num3++;
			item.localPosition = new Vector3(num, num2, 0f);
			num += intervalX;
			if (num3 >= this.num)
			{
				num = 0f;
				num2 += intervalY;
				num3 = 0;
			}
			((Component)item).GetComponent<MapInstComport>().showDebugLine();
		}
	}

	public virtual void addNodeIndex(int index, MapInstComport comp)
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
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
		if (index > 0 && index <= ((Component)this).transform.childCount)
		{
			comp.nextIndex.Add(index);
		}
	}
}
