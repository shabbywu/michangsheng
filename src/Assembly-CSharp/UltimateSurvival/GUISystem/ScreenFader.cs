using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x0200095B RID: 2395
	public class ScreenFader : GUIBehaviour
	{
		// Token: 0x06003D32 RID: 15666 RVA: 0x0002C1CE File Offset: 0x0002A3CE
		private void Start()
		{
			base.Player.Death.AddListener(new Action(this.On_Death));
			base.Player.Respawn.AddListener(new Action(this.On_Respawn));
		}

		// Token: 0x06003D33 RID: 15667 RVA: 0x0002C208 File Offset: 0x0002A408
		private void On_Death()
		{
			base.StopAllCoroutines();
			base.StartCoroutine(this.C_FadeScreen(1f));
		}

		// Token: 0x06003D34 RID: 15668 RVA: 0x0002C222 File Offset: 0x0002A422
		private void On_Respawn()
		{
			base.StopAllCoroutines();
			base.StartCoroutine(this.C_FadeScreen(0f));
		}

		// Token: 0x06003D35 RID: 15669 RVA: 0x0002C23C File Offset: 0x0002A43C
		private IEnumerator C_FadeScreen(float targetAlpha)
		{
			while (Mathf.Abs(this.m_Image.color.a - targetAlpha) > 0f)
			{
				this.m_Image.color = this.MoveTowardsAlpha(this.m_Image.color, targetAlpha, Time.deltaTime * this.m_FadeSpeed);
				AudioListener.volume = 1f - this.m_Image.color.a;
				yield return null;
			}
			yield break;
		}

		// Token: 0x06003D36 RID: 15670 RVA: 0x0002C252 File Offset: 0x0002A452
		private Color MoveTowardsAlpha(Color color, float alpha, float maxDelta)
		{
			return new Color(color.r, color.g, color.b, Mathf.MoveTowards(color.a, alpha, maxDelta));
		}

		// Token: 0x0400376D RID: 14189
		[SerializeField]
		private Image m_Image;

		// Token: 0x0400376E RID: 14190
		[SerializeField]
		private float m_FadeSpeed = 0.3f;
	}
}
