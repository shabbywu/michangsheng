using System;
using UnityEngine;
using UnityEngine.Events;

namespace Bag
{
	// Token: 0x02000D1D RID: 3357
	public class BaseFilterLeft : MonoBehaviour
	{
		// Token: 0x06004FEA RID: 20458 RVA: 0x00039853 File Offset: 0x00037A53
		public void Add(UnityAction action)
		{
			this.Select.mouseUpEvent.AddListener(action);
			this.UnSelect.mouseUpEvent.AddListener(action);
		}

		// Token: 0x06004FEB RID: 20459 RVA: 0x00039877 File Offset: 0x00037A77
		public void UpdateState()
		{
			this.Select.gameObject.SetActive(this.IsSelect);
			this.UnSelect.gameObject.SetActive(!this.IsSelect);
		}

		// Token: 0x04005140 RID: 20800
		public ItemType ItemType;

		// Token: 0x04005141 RID: 20801
		public FpBtn Select;

		// Token: 0x04005142 RID: 20802
		public FpBtn UnSelect;

		// Token: 0x04005143 RID: 20803
		public bool IsSelect;
	}
}
