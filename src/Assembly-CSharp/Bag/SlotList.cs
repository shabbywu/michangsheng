using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bag
{
	// Token: 0x020009B0 RID: 2480
	public class SlotList : MonoBehaviour
	{
		// Token: 0x060044F9 RID: 17657 RVA: 0x001D5248 File Offset: 0x001D3448
		public void Init()
		{
			this.mItemList = new List<ISlot>();
			for (int i = 0; i < base.transform.childCount; i++)
			{
				this.mItemList.Add(base.transform.GetChild(i).GetComponent<ISlot>());
			}
		}

		// Token: 0x040046BC RID: 18108
		public List<ISlot> mItemList;
	}
}
