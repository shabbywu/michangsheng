using System;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Bag
{
	// Token: 0x02000D1E RID: 3358
	public class BaseFilterTopChild : FpBtn
	{
		// Token: 0x06004FED RID: 20461 RVA: 0x000398A8 File Offset: 0x00037AA8
		public void Init(string title, UnityAction action)
		{
			this.Text.SetText(title);
			this.mouseUpEvent.AddListener(action);
			base.gameObject.SetActive(true);
		}

		// Token: 0x04005144 RID: 20804
		public Text Text;
	}
}
