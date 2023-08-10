using System.Collections.Generic;
using UnityEngine;

namespace GetWay;

public class PriorityQueue
{
	private Dictionary<int, float> _dict = new Dictionary<int, float>();

	public void put(int index, float power)
	{
		_dict.Add(index, power);
	}

	public int Get()
	{
		if (IsEmpty())
		{
			Debug.LogError((object)"队列为空");
			return -1;
		}
		int num = -1;
		foreach (int key in _dict.Keys)
		{
			if (num == -1)
			{
				num = key;
			}
			else if (_dict[num] > _dict[key])
			{
				num = key;
			}
		}
		_dict.Remove(num);
		return num;
	}

	public bool IsEmpty()
	{
		if (_dict.Count < 1)
		{
			return true;
		}
		return false;
	}
}
