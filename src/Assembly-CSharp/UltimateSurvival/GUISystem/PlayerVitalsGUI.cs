using System;
using UnityEngine;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x0200095A RID: 2394
	public class PlayerVitalsGUI : GUIBehaviour
	{
		// Token: 0x06003D2C RID: 15660 RVA: 0x001B36F0 File Offset: 0x001B18F0
		private void Start()
		{
			base.Player.Health.AddChangeListener(new Action(this.OnChanged_Health));
			base.Player.Stamina.AddChangeListener(new Action(this.OnChanged_Stamina));
			base.Player.Thirst.AddChangeListener(new Action(this.OnChanged_Thirst));
			base.Player.Hunger.AddChangeListener(new Action(this.OnChanged_Hunger));
		}

		// Token: 0x06003D2D RID: 15661 RVA: 0x0002C142 File Offset: 0x0002A342
		private void OnChanged_Health()
		{
			this.m_HealthBar.fillAmount = base.Player.Health.Get() / 100f;
		}

		// Token: 0x06003D2E RID: 15662 RVA: 0x0002C165 File Offset: 0x0002A365
		private void OnChanged_Stamina()
		{
			this.m_StaminaBar.fillAmount = base.Player.Stamina.Get() / 100f;
		}

		// Token: 0x06003D2F RID: 15663 RVA: 0x0002C188 File Offset: 0x0002A388
		private void OnChanged_Thirst()
		{
			this.m_ThirstBar.fillAmount = base.Player.Thirst.Get() / 100f;
		}

		// Token: 0x06003D30 RID: 15664 RVA: 0x0002C1AB File Offset: 0x0002A3AB
		private void OnChanged_Hunger()
		{
			this.m_HungerBar.fillAmount = base.Player.Hunger.Get() / 100f;
		}

		// Token: 0x04003769 RID: 14185
		[SerializeField]
		private Image m_HealthBar;

		// Token: 0x0400376A RID: 14186
		[SerializeField]
		private Image m_StaminaBar;

		// Token: 0x0400376B RID: 14187
		[SerializeField]
		private Image m_ThirstBar;

		// Token: 0x0400376C RID: 14188
		[SerializeField]
		private Image m_HungerBar;
	}
}
