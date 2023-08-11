using System.Collections.Generic;

namespace KBEngine;

public class BuffLoopData
{
	public List<int> seid;

	public int TargetLoopTime;

	public BuffLoopData()
	{
	}

	public BuffLoopData(int TargetLoopTime, List<int> seid)
	{
		this.TargetLoopTime = TargetLoopTime;
		SetSeid(seid);
	}

	public void SetSeid(List<int> seid)
	{
		this.seid = new List<int>();
		for (int i = 0; i < seid.Count; i++)
		{
			this.seid.Add(seid[i]);
		}
	}

	public override string ToString()
	{
		string text = $"TargetLoopTime:{TargetLoopTime},seid:";
		foreach (int item in seid)
		{
			text += $"{item} ";
		}
		return text;
	}
}
