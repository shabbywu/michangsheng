using System;
using System.Collections.Generic;

namespace KBEngine;

public class MapIndexInfo
{
	public string SenceName = "";

	private FubenContrl parent;

	public int NowIndex
	{
		get
		{
			return (int)parent.entity.FuBen[SenceName]["NowIndex"].n;
		}
		set
		{
			if (!parent.entity.FuBen[SenceName].HasField("NowIndex"))
			{
				parent.entity.FuBen[SenceName].SetField("NowIndex", -1);
			}
			addExploredNode(value);
			parent.entity.FuBen[SenceName].SetField("NowIndex", value);
		}
	}

	public List<int> ExploredNode
	{
		get
		{
			List<int> TempList = new List<int>();
			parent.entity.FuBen[SenceName]["ExploredNode"].list.ForEach(delegate(JSONObject aa)
			{
				TempList.Add((int)aa.n);
			});
			return TempList;
		}
	}

	public DateTime StartTime => DateTime.Parse(parent.entity.FuBen[SenceName]["StartTime"].str);

	public int ResidueTimeDay
	{
		get
		{
			DateTime startTime = StartTime;
			DateTime nowTime = Tools.instance.getPlayer().worldTimeMag.getNowTime();
			JSONObject jSONObject = jsonData.instance.FuBenInfoJsonData[SenceName];
			return (int)jSONObject["TimeY"].n * 12 * 30 + (int)jSONObject["TimeM"].n * 30 + (int)jSONObject["TimeD"].n - (nowTime - startTime).Days;
		}
	}

	public MapIndexInfo(FubenContrl aa)
	{
		parent = aa;
	}

	public void addExploredNode(int index)
	{
		if (!parent.entity.FuBen[SenceName].HasField("ExploredNode"))
		{
			parent.entity.FuBen[SenceName].SetField("ExploredNode", new JSONObject(JSONObject.Type.ARRAY));
		}
		if (!parent.entity.FuBen[SenceName]["ExploredNode"].HasItem(index))
		{
			parent.entity.FuBen[SenceName]["ExploredNode"].Add(index);
		}
	}

	public void AddNodeRoad(int index, int ToIndex)
	{
		if (!parent.entity.FuBen[SenceName].HasField("RoadNode"))
		{
			parent.entity.FuBen[SenceName].SetField("RoadNode", new JSONObject(JSONObject.Type.OBJECT));
		}
		if (!parent.entity.FuBen[SenceName]["RoadNode"].HasField(string.Concat(index)))
		{
			parent.entity.FuBen[SenceName]["RoadNode"].SetField(string.Concat(index), new JSONObject(JSONObject.Type.ARRAY));
		}
		if (!parent.entity.FuBen[SenceName]["RoadNode"][index.ToString()].HasItem(ToIndex))
		{
			parent.entity.FuBen[SenceName]["RoadNode"][index.ToString()].Add(ToIndex);
		}
	}

	public void setStartTime()
	{
		parent.entity.FuBen[SenceName].SetField("StartTime", Tools.instance.getPlayer().worldTimeMag.nowTime);
	}

	public void setFirstIndex(int index)
	{
		NowIndex = index;
		setStartTime();
	}
}
