using System;
using UnityEngine;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x02000959 RID: 2393
	public class PlayerStatsGUI : GUIBehaviour
	{
		// Token: 0x06003D29 RID: 15657 RVA: 0x0002C0F8 File Offset: 0x0002A2F8
		private void Start()
		{
			base.Player.Defense.AddChangeListener(new Action(this.OnChanged_Defense));
		}

		// Token: 0x06003D2A RID: 15658 RVA: 0x0002C116 File Offset: 0x0002A316
		private void OnChanged_Defense()
		{
			this.m_DefenseText.text = base.Player.Defense.Get() + "%";
		}

		// Token: 0x04003768 RID: 14184
		[SerializeField]
		private Text m_DefenseText;
	}
}
