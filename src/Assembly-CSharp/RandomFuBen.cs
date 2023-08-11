using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class RandomFuBen : MonoBehaviour
{
	public static bool IsInRandomFuBen;

	public static int NowRanDomFuBenID;

	[Tooltip("列间距")]
	public float intervalX;

	[Tooltip("行间距")]
	public float intervalY;

	[Tooltip("每行的数量")]
	public int num;

	[Tooltip("自动创建的子节点的基类")]
	public GameObject NodeTemp;

	[Tooltip("创建的子节点个数")]
	public int creatNum;

	public int High;

	public int Wide;

	public int FuBenType;

	public int FuBenID;

	public FuBenMap mapMag;

	public GameObject dian2;

	private void OnDestroy()
	{
		IsInRandomFuBen = false;
	}

	private void Awake()
	{
		IsInRandomFuBen = true;
		WASDMove.DelMoreComponent(((Component)this).gameObject);
		((Component)this).gameObject.AddComponent<WASDMove>();
		FuBenID = Tools.instance.getPlayer().NowRandomFuBenID;
		NowRanDomFuBenID = FuBenID;
		InitNode(FuBenID);
	}

	public void InitNode(int RandomId)
	{
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		JToken val = Tools.instance.getPlayer().RandomFuBenList[FuBenID.ToString()];
		High = (int)val[(object)"high"];
		Wide = (int)val[(object)"wide"];
		int num = 0;
		for (int i = 0; i < High; i++)
		{
			for (int j = 0; j < Wide; j++)
			{
				GameObject obj = Object.Instantiate<GameObject>(NodeTemp);
				obj.transform.SetParent(((Component)this).transform);
				((Object)obj).name = string.Concat(num + 1);
				num++;
			}
		}
		mapMag = new FuBenMap(High, Wide);
		int[,] array = new int[Wide, High];
		int num2 = 0;
		int num3 = 0;
		foreach (JToken item in (IEnumerable<JToken>)val[(object)"Map"])
		{
			num3 = 0;
			foreach (JToken item2 in (JArray)item)
			{
				array[num2, num3] = (int)item2;
				num3++;
			}
			num2++;
		}
		mapMag.map = array;
		SetMapNodeRoad(mapMag);
		SetNodePosition();
	}

	public void SetNodePosition()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		int num = 0;
		foreach (Transform item in ((Component)this).transform)
		{
			float num2 = (float)(num % Wide + 1) - (float)(Wide + 1) / 2f;
			float num3 = (float)(num / Wide + 1) - (float)(High + 1) / 2f;
			item.localPosition = new Vector3(num2 * intervalX, num3 * intervalY, 0f);
			num++;
		}
	}

	public void SetMapNodeRoad(FuBenMap map)
	{
		for (int i = 0; i < High; i++)
		{
			for (int j = 0; j < Wide; j++)
			{
				if (map.map[j, i] <= 0)
				{
					continue;
				}
				List<int> xiangLingRoad = map.getXiangLingRoad(j, i);
				MapRandomCompent component = ((Component)((Component)this).transform.Find(map.mapIndex[j, i].ToString())).GetComponent<MapRandomCompent>();
				foreach (int item in xiangLingRoad)
				{
					component.nextIndex.Add(item);
				}
			}
		}
	}

	public void SetAwakeRoad(FuBenMap map)
	{
	}

	private void Update()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		foreach (Transform item in ((Component)this).transform)
		{
			((Component)item).GetComponent<MapRandomCompent>().showDebugLine();
		}
	}
}
