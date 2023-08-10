using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class QuanZhongItem
{
	public object Obj;

	public int Weight;

	public QuanZhongItem()
	{
	}

	public QuanZhongItem(object obj, int weight)
	{
		Obj = obj;
		Weight = weight;
	}

	public static QuanZhongItem Roll(List<QuanZhongItem> itemList, bool log = false)
	{
		if (itemList == null || itemList.Count == 0)
		{
			if (log)
			{
				Debug.Log((object)"QuanZhongItem.Roll:随机列表为空，返回null");
			}
			return null;
		}
		if (itemList.Count == 1)
		{
			if (log)
			{
				Debug.Log((object)"QuanZhongItem.Roll:随机列表长度为1，直接返回");
			}
			return itemList[0];
		}
		List<QuanZhongItem> list = new List<QuanZhongItem>();
		int num = 0;
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("QuanZhongItem.Roll:权重:");
		for (int i = 0; i < itemList.Count; i++)
		{
			QuanZhongItem quanZhongItem = itemList[i];
			if (quanZhongItem.Weight > 0)
			{
				list.Add(quanZhongItem);
				num += quanZhongItem.Weight;
			}
			stringBuilder.Append($"[{i}]{quanZhongItem.Weight},");
		}
		stringBuilder.Append($"总权重:{num},");
		if (list.Count == 0)
		{
			stringBuilder.Append("随机列表为空，返回null");
			Debug.Log((object)stringBuilder.ToString());
			return null;
		}
		int num2 = Random.Range(0, num);
		stringBuilder.Append($"随机落点:{num2},开始遍历随机列表:\n");
		int num3 = 0;
		int num4 = 0;
		for (int j = 0; j < list.Count; j++)
		{
			QuanZhongItem quanZhongItem2 = list[j];
			num3 += quanZhongItem2.Weight;
			num4 = j;
			if (num3 <= num2)
			{
				stringBuilder.AppendLine($"cur {num3} <= target {num2}，结果索引设置为{j}");
				continue;
			}
			stringBuilder.AppendLine($"cur {num3} > target {num2}，中断遍历");
			break;
		}
		stringBuilder.AppendLine($"最终随机结果:{num4}");
		Debug.Log((object)stringBuilder.ToString());
		return list[num4];
	}
}
