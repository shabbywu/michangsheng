using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

// Token: 0x020003CC RID: 972
public class RandomFuBen : MonoBehaviour
{
	// Token: 0x06001FAE RID: 8110 RVA: 0x000DF586 File Offset: 0x000DD786
	private void OnDestroy()
	{
		RandomFuBen.IsInRandomFuBen = false;
	}

	// Token: 0x06001FAF RID: 8111 RVA: 0x000DF590 File Offset: 0x000DD790
	private void Awake()
	{
		RandomFuBen.IsInRandomFuBen = true;
		WASDMove.DelMoreComponent(base.gameObject);
		base.gameObject.AddComponent<WASDMove>();
		this.FuBenID = Tools.instance.getPlayer().NowRandomFuBenID;
		RandomFuBen.NowRanDomFuBenID = this.FuBenID;
		this.InitNode(this.FuBenID);
	}

	// Token: 0x06001FB0 RID: 8112 RVA: 0x000DF5E8 File Offset: 0x000DD7E8
	public void InitNode(int RandomId)
	{
		JToken jtoken = Tools.instance.getPlayer().RandomFuBenList[this.FuBenID.ToString()];
		this.High = (int)jtoken["high"];
		this.Wide = (int)jtoken["wide"];
		int num = 0;
		for (int i = 0; i < this.High; i++)
		{
			for (int j = 0; j < this.Wide; j++)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this.NodeTemp);
				gameObject.transform.SetParent(base.transform);
				gameObject.name = string.Concat(num + 1);
				num++;
			}
		}
		this.mapMag = new FuBenMap(this.High, this.Wide);
		int[,] array = new int[this.Wide, this.High];
		int num2 = 0;
		foreach (JToken jtoken2 in jtoken["Map"])
		{
			int num3 = 0;
			foreach (JToken jtoken3 in ((JArray)jtoken2))
			{
				array[num2, num3] = (int)jtoken3;
				num3++;
			}
			num2++;
		}
		this.mapMag.map = array;
		this.SetMapNodeRoad(this.mapMag);
		this.SetNodePosition();
	}

	// Token: 0x06001FB1 RID: 8113 RVA: 0x000DF784 File Offset: 0x000DD984
	public void SetNodePosition()
	{
		int num = 0;
		foreach (object obj in base.transform)
		{
			Transform transform = (Transform)obj;
			float num2 = (float)(num % this.Wide + 1) - (float)(this.Wide + 1) / 2f;
			float num3 = (float)(num / this.Wide + 1) - (float)(this.High + 1) / 2f;
			transform.localPosition = new Vector3(num2 * this.intervalX, num3 * this.intervalY, 0f);
			num++;
		}
	}

	// Token: 0x06001FB2 RID: 8114 RVA: 0x000DF838 File Offset: 0x000DDA38
	public void SetMapNodeRoad(FuBenMap map)
	{
		for (int i = 0; i < this.High; i++)
		{
			for (int j = 0; j < this.Wide; j++)
			{
				if (map.map[j, i] > 0)
				{
					List<int> xiangLingRoad = map.getXiangLingRoad(j, i);
					MapRandomCompent component = base.transform.Find(map.mapIndex[j, i].ToString()).GetComponent<MapRandomCompent>();
					foreach (int item in xiangLingRoad)
					{
						component.nextIndex.Add(item);
					}
				}
			}
		}
	}

	// Token: 0x06001FB3 RID: 8115 RVA: 0x00004095 File Offset: 0x00002295
	public void SetAwakeRoad(FuBenMap map)
	{
	}

	// Token: 0x06001FB4 RID: 8116 RVA: 0x000DF8F0 File Offset: 0x000DDAF0
	private void Update()
	{
		foreach (object obj in base.transform)
		{
			((Transform)obj).GetComponent<MapRandomCompent>().showDebugLine();
		}
	}

	// Token: 0x040019BF RID: 6591
	public static bool IsInRandomFuBen;

	// Token: 0x040019C0 RID: 6592
	public static int NowRanDomFuBenID;

	// Token: 0x040019C1 RID: 6593
	[Tooltip("列间距")]
	public float intervalX;

	// Token: 0x040019C2 RID: 6594
	[Tooltip("行间距")]
	public float intervalY;

	// Token: 0x040019C3 RID: 6595
	[Tooltip("每行的数量")]
	public int num;

	// Token: 0x040019C4 RID: 6596
	[Tooltip("自动创建的子节点的基类")]
	public GameObject NodeTemp;

	// Token: 0x040019C5 RID: 6597
	[Tooltip("创建的子节点个数")]
	public int creatNum;

	// Token: 0x040019C6 RID: 6598
	public int High;

	// Token: 0x040019C7 RID: 6599
	public int Wide;

	// Token: 0x040019C8 RID: 6600
	public int FuBenType;

	// Token: 0x040019C9 RID: 6601
	public int FuBenID;

	// Token: 0x040019CA RID: 6602
	public FuBenMap mapMag;

	// Token: 0x040019CB RID: 6603
	public GameObject dian2;
}
