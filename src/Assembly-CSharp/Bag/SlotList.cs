using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bag
{
	// Token: 0x02000D38 RID: 3384
	public class SlotList : MonoBehaviour
	{
		// Token: 0x0600505C RID: 20572 RVA: 0x00219800 File Offset: 0x00217A00
		public void Init()
		{
			this.mItemList = new List<ISlot>();
			for (int i = 0; i < base.transform.childCount; i++)
			{
				this.mItemList.Add(base.transform.GetChild(i).GetComponent<ISlot>());
			}
		}

		// Token: 0x040051BE RID: 20926
		public List<ISlot> mItemList;
	}
}
