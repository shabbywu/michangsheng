using System;
using UnityEngine;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x0200094E RID: 2382
	[Serializable]
	public class MessageForPlayer
	{
		// Token: 0x06003CE0 RID: 15584 RVA: 0x0002BDEA File Offset: 0x00029FEA
		public void Toggle(bool toggle)
		{
			this.m_Root.SetActive(toggle);
		}

		// Token: 0x06003CE1 RID: 15585 RVA: 0x0002BDF8 File Offset: 0x00029FF8
		public void SetText(string message)
		{
			this.m_Text.text = message;
		}

		// Token: 0x04003724 RID: 14116
		[SerializeField]
		private GameObject m_Root;

		// Token: 0x04003725 RID: 14117
		[SerializeField]
		private Text m_Text;
	}
}
