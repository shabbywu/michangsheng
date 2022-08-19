using System;
using UnityEngine;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x0200064F RID: 1615
	[Serializable]
	public class MessageForPlayer
	{
		// Token: 0x0600336A RID: 13162 RVA: 0x00169398 File Offset: 0x00167598
		public void Toggle(bool toggle)
		{
			this.m_Root.SetActive(toggle);
		}

		// Token: 0x0600336B RID: 13163 RVA: 0x001693A6 File Offset: 0x001675A6
		public void SetText(string message)
		{
			this.m_Text.text = message;
		}

		// Token: 0x04002DAD RID: 11693
		[SerializeField]
		private GameObject m_Root;

		// Token: 0x04002DAE RID: 11694
		[SerializeField]
		private Text m_Text;
	}
}
