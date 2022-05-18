using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace QiYu
{
	// Token: 0x02000A76 RID: 2678
	public class QiYuOption : MonoBehaviour
	{
		// Token: 0x060044E3 RID: 17635 RVA: 0x0003143D File Offset: 0x0002F63D
		public void Init(string Name, UnityAction Action)
		{
			this.OptionName.text = Name;
			this.Btn.mouseUpEvent.AddListener(Action);
			base.gameObject.SetActive(true);
		}

		// Token: 0x04003D0B RID: 15627
		[SerializeField]
		private FpBtn Btn;

		// Token: 0x04003D0C RID: 15628
		[SerializeField]
		private Text OptionName;
	}
}
