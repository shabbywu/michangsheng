using System;
using System.Collections.Generic;
using UnityEngine;

namespace GetWay
{
	// Token: 0x0200073F RID: 1855
	public class PriorityQueue
	{
		// Token: 0x06003B10 RID: 15120 RVA: 0x001962E6 File Offset: 0x001944E6
		public void put(int index, float power)
		{
			this._dict.Add(index, power);
		}

		// Token: 0x06003B11 RID: 15121 RVA: 0x001962F8 File Offset: 0x001944F8
		public int Get()
		{
			if (this.IsEmpty())
			{
				Debug.LogError("队列为空");
				return -1;
			}
			int num = -1;
			foreach (int num2 in this._dict.Keys)
			{
				if (num == -1)
				{
					num = num2;
				}
				else if (this._dict[num] > this._dict[num2])
				{
					num = num2;
				}
			}
			this._dict.Remove(num);
			return num;
		}

		// Token: 0x06003B12 RID: 15122 RVA: 0x00196394 File Offset: 0x00194594
		public bool IsEmpty()
		{
			return this._dict.Count < 1;
		}

		// Token: 0x04003346 RID: 13126
		private Dictionary<int, float> _dict = new Dictionary<int, float>();
	}
}
