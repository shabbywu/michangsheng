using System;
using UnityEngine;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x02000657 RID: 1623
	public class PlayerStatsGUI : GUIBehaviour
	{
		// Token: 0x060033A1 RID: 13217 RVA: 0x0016A650 File Offset: 0x00168850
		private void Start()
		{
			base.Player.Defense.AddChangeListener(new Action(this.OnChanged_Defense));
		}

		// Token: 0x060033A2 RID: 13218 RVA: 0x0016A66E File Offset: 0x0016886E
		private void OnChanged_Defense()
		{
			this.m_DefenseText.text = base.Player.Defense.Get() + "%";
		}

		// Token: 0x04002DE7 RID: 11751
		[SerializeField]
		private Text m_DefenseText;
	}
}
