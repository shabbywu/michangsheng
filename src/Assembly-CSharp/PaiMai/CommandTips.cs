using System;
using UnityEngine;
using UnityEngine.UI;

namespace PaiMai
{
	// Token: 0x02000A62 RID: 2658
	public class CommandTips : MonoBehaviour
	{
		// Token: 0x06004483 RID: 17539 RVA: 0x00031020 File Offset: 0x0002F220
		private void Awake()
		{
			this._content = base.transform.Find("content").GetComponent<Text>();
		}

		// Token: 0x06004484 RID: 17540 RVA: 0x0003103D File Offset: 0x0002F23D
		public void ShowTips(string msg)
		{
			base.gameObject.SetActive(true);
			this._content.text = msg;
		}

		// Token: 0x06004485 RID: 17541 RVA: 0x00017C2D File Offset: 0x00015E2D
		public void Hide()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x04003C8A RID: 15498
		private Text _content;
	}
}
