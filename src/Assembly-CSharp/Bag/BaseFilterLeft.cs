using System;
using UnityEngine;
using UnityEngine.Events;

namespace Bag
{
	// Token: 0x02000998 RID: 2456
	public class BaseFilterLeft : MonoBehaviour
	{
		// Token: 0x0600448A RID: 17546 RVA: 0x001D3320 File Offset: 0x001D1520
		public void Add(UnityAction action)
		{
			this.Select.mouseUpEvent.AddListener(action);
			this.UnSelect.mouseUpEvent.AddListener(action);
		}

		// Token: 0x0600448B RID: 17547 RVA: 0x001D3344 File Offset: 0x001D1544
		public void UpdateState()
		{
			this.Select.gameObject.SetActive(this.IsSelect);
			this.UnSelect.gameObject.SetActive(!this.IsSelect);
		}

		// Token: 0x04004642 RID: 17986
		public ItemType ItemType;

		// Token: 0x04004643 RID: 17987
		public FpBtn Select;

		// Token: 0x04004644 RID: 17988
		public FpBtn UnSelect;

		// Token: 0x04004645 RID: 17989
		public bool IsSelect;
	}
}
