using System;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Bag
{
	// Token: 0x02000999 RID: 2457
	public class BaseFilterTopChild : FpBtn
	{
		// Token: 0x0600448D RID: 17549 RVA: 0x001D3375 File Offset: 0x001D1575
		public void Init(string title, UnityAction action)
		{
			this.Text.SetText(title);
			this.mouseUpEvent.AddListener(action);
			base.gameObject.SetActive(true);
		}

		// Token: 0x04004646 RID: 17990
		public Text Text;
	}
}
