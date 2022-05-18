using System;
using System.Collections.Generic;
using UnityEngine;

namespace GetWay
{
	// Token: 0x02000AD7 RID: 2775
	public class PriorityQueue
	{
		// Token: 0x060046C6 RID: 18118 RVA: 0x000327BF File Offset: 0x000309BF
		public void put(int index, float power)
		{
			this._dict.Add(index, power);
		}

		// Token: 0x060046C7 RID: 18119 RVA: 0x001E4820 File Offset: 0x001E2A20
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

		// Token: 0x060046C8 RID: 18120 RVA: 0x000327CE File Offset: 0x000309CE
		public bool IsEmpty()
		{
			return this._dict.Count < 1;
		}

		// Token: 0x04003EE2 RID: 16098
		private Dictionary<int, float> _dict = new Dictionary<int, float>();
	}
}
