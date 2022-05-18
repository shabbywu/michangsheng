using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

// Token: 0x02000561 RID: 1377
public class RandomFuBen : MonoBehaviour
{
	// Token: 0x06002325 RID: 8997 RVA: 0x0001C94B File Offset: 0x0001AB4B
	private void OnDestroy()
	{
		RandomFuBen.IsInRandomFuBen = false;
	}

	// Token: 0x06002326 RID: 8998 RVA: 0x00122008 File Offset: 0x00120208
	private void Awake()
	{
		RandomFuBen.IsInRandomFuBen = true;
		WASDMove.DelMoreComponent(base.gameObject);
		base.gameObject.AddComponent<WASDMove>();
		this.FuBenID = Tools.instance.getPlayer().NowRandomFuBenID;
		RandomFuBen.NowRanDomFuBenID = this.FuBenID;
		this.InitNode(this.FuBenID);
	}

	// Token: 0x06002327 RID: 8999 RVA: 0x00122060 File Offset: 0x00120260
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

	// Token: 0x06002328 RID: 9000 RVA: 0x001221FC File Offset: 0x001203FC
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

	// Token: 0x06002329 RID: 9001 RVA: 0x001222B0 File Offset: 0x001204B0
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

	// Token: 0x0600232A RID: 9002 RVA: 0x000042DD File Offset: 0x000024DD
	public void SetAwakeRoad(FuBenMap map)
	{
	}

	// Token: 0x0600232B RID: 9003 RVA: 0x00122368 File Offset: 0x00120568
	private void Update()
	{
		foreach (object obj in base.transform)
		{
			((Transform)obj).GetComponent<MapRandomCompent>().showDebugLine();
		}
	}

	// Token: 0x04001E40 RID: 7744
	public static bool IsInRandomFuBen;

	// Token: 0x04001E41 RID: 7745
	public static int NowRanDomFuBenID;

	// Token: 0x04001E42 RID: 7746
	[Tooltip("列间距")]
	public float intervalX;

	// Token: 0x04001E43 RID: 7747
	[Tooltip("行间距")]
	public float intervalY;

	// Token: 0x04001E44 RID: 7748
	[Tooltip("每行的数量")]
	public int num;

	// Token: 0x04001E45 RID: 7749
	[Tooltip("自动创建的子节点的基类")]
	public GameObject NodeTemp;

	// Token: 0x04001E46 RID: 7750
	[Tooltip("创建的子节点个数")]
	public int creatNum;

	// Token: 0x04001E47 RID: 7751
	public int High;

	// Token: 0x04001E48 RID: 7752
	public int Wide;

	// Token: 0x04001E49 RID: 7753
	public int FuBenType;

	// Token: 0x04001E4A RID: 7754
	public int FuBenID;

	// Token: 0x04001E4B RID: 7755
	public FuBenMap mapMag;

	// Token: 0x04001E4C RID: 7756
	public GameObject dian2;
}
