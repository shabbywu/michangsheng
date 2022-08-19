using System;
using UnityEngine;
using UnityEngine.UI;

namespace PaiMai
{
	// Token: 0x02000712 RID: 1810
	public class CommandTips : MonoBehaviour
	{
		// Token: 0x060039F7 RID: 14839 RVA: 0x0018D528 File Offset: 0x0018B728
		private void Awake()
		{
			this._content = base.transform.Find("content").GetComponent<Text>();
		}

		// Token: 0x060039F8 RID: 14840 RVA: 0x0018D545 File Offset: 0x0018B745
		public void ShowTips(string msg)
		{
			base.gameObject.SetActive(true);
			this._content.text = msg;
		}

		// Token: 0x060039F9 RID: 14841 RVA: 0x000B5E62 File Offset: 0x000B4062
		public void Hide()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x04003203 RID: 12803
		private Text _content;
	}
}
