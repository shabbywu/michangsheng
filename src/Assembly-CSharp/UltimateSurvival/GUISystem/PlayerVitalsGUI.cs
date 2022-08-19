using System;
using UnityEngine;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x02000658 RID: 1624
	public class PlayerVitalsGUI : GUIBehaviour
	{
		// Token: 0x060033A4 RID: 13220 RVA: 0x0016A69C File Offset: 0x0016889C
		private void Start()
		{
			base.Player.Health.AddChangeListener(new Action(this.OnChanged_Health));
			base.Player.Stamina.AddChangeListener(new Action(this.OnChanged_Stamina));
			base.Player.Thirst.AddChangeListener(new Action(this.OnChanged_Thirst));
			base.Player.Hunger.AddChangeListener(new Action(this.OnChanged_Hunger));
		}

		// Token: 0x060033A5 RID: 13221 RVA: 0x0016A719 File Offset: 0x00168919
		private void OnChanged_Health()
		{
			this.m_HealthBar.fillAmount = base.Player.Health.Get() / 100f;
		}

		// Token: 0x060033A6 RID: 13222 RVA: 0x0016A73C File Offset: 0x0016893C
		private void OnChanged_Stamina()
		{
			this.m_StaminaBar.fillAmount = base.Player.Stamina.Get() / 100f;
		}

		// Token: 0x060033A7 RID: 13223 RVA: 0x0016A75F File Offset: 0x0016895F
		private void OnChanged_Thirst()
		{
			this.m_ThirstBar.fillAmount = base.Player.Thirst.Get() / 100f;
		}

		// Token: 0x060033A8 RID: 13224 RVA: 0x0016A782 File Offset: 0x00168982
		private void OnChanged_Hunger()
		{
			this.m_HungerBar.fillAmount = base.Player.Hunger.Get() / 100f;
		}

		// Token: 0x04002DE8 RID: 11752
		[SerializeField]
		private Image m_HealthBar;

		// Token: 0x04002DE9 RID: 11753
		[SerializeField]
		private Image m_StaminaBar;

		// Token: 0x04002DEA RID: 11754
		[SerializeField]
		private Image m_ThirstBar;

		// Token: 0x04002DEB RID: 11755
		[SerializeField]
		private Image m_HungerBar;
	}
}
