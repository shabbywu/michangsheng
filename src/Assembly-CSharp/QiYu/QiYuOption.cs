using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace QiYu
{
	// Token: 0x02000721 RID: 1825
	public class QiYuOption : MonoBehaviour
	{
		// Token: 0x06003A46 RID: 14918 RVA: 0x00190444 File Offset: 0x0018E644
		public void Init(string Name, UnityAction Action)
		{
			this.OptionName.text = Name;
			this.Btn.mouseUpEvent.AddListener(Action);
			base.gameObject.SetActive(true);
		}

		// Token: 0x04003273 RID: 12915
		[SerializeField]
		private FpBtn Btn;

		// Token: 0x04003274 RID: 12916
		[SerializeField]
		private Text OptionName;
	}
}
