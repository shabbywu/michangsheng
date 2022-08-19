using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x020001CB RID: 459
public class QuanZhongItem
{
	// Token: 0x06001332 RID: 4914 RVA: 0x000027FC File Offset: 0x000009FC
	public QuanZhongItem()
	{
	}

	// Token: 0x06001333 RID: 4915 RVA: 0x00078A7E File Offset: 0x00076C7E
	public QuanZhongItem(object obj, int weight)
	{
		this.Obj = obj;
		this.Weight = weight;
	}

	// Token: 0x06001334 RID: 4916 RVA: 0x00078A94 File Offset: 0x00076C94
	public static QuanZhongItem Roll(List<QuanZhongItem> itemList, bool log = false)
	{
		if (itemList == null || itemList.Count == 0)
		{
			if (log)
			{
				Debug.Log("QuanZhongItem.Roll:随机列表为空，返回null");
			}
			return null;
		}
		if (itemList.Count == 1)
		{
			if (log)
			{
				Debug.Log("QuanZhongItem.Roll:随机列表长度为1，直接返回");
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
			stringBuilder.Append(string.Format("[{0}]{1},", i, quanZhongItem.Weight));
		}
		stringBuilder.Append(string.Format("总权重:{0},", num));
		if (list.Count == 0)
		{
			stringBuilder.Append("随机列表为空，返回null");
			Debug.Log(stringBuilder.ToString());
			return null;
		}
		int num2 = Random.Range(0, num);
		stringBuilder.Append(string.Format("随机落点:{0},开始遍历随机列表:\n", num2));
		int num3 = 0;
		int num4 = 0;
		for (int j = 0; j < list.Count; j++)
		{
			QuanZhongItem quanZhongItem2 = list[j];
			num3 += quanZhongItem2.Weight;
			num4 = j;
			if (num3 > num2)
			{
				stringBuilder.AppendLine(string.Format("cur {0} > target {1}，中断遍历", num3, num2));
				break;
			}
			stringBuilder.AppendLine(string.Format("cur {0} <= target {1}，结果索引设置为{2}", num3, num2, j));
		}
		stringBuilder.AppendLine(string.Format("最终随机结果:{0}", num4));
		Debug.Log(stringBuilder.ToString());
		return list[num4];
	}

	// Token: 0x04000E84 RID: 3716
	public object Obj;

	// Token: 0x04000E85 RID: 3717
	public int Weight;
}
