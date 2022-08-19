using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x02000659 RID: 1625
	public class ScreenFader : GUIBehaviour
	{
		// Token: 0x060033AA RID: 13226 RVA: 0x0016A7A5 File Offset: 0x001689A5
		private void Start()
		{
			base.Player.Death.AddListener(new Action(this.On_Death));
			base.Player.Respawn.AddListener(new Action(this.On_Respawn));
		}

		// Token: 0x060033AB RID: 13227 RVA: 0x0016A7DF File Offset: 0x001689DF
		private void On_Death()
		{
			base.StopAllCoroutines();
			base.StartCoroutine(this.C_FadeScreen(1f));
		}

		// Token: 0x060033AC RID: 13228 RVA: 0x0016A7F9 File Offset: 0x001689F9
		private void On_Respawn()
		{
			base.StopAllCoroutines();
			base.StartCoroutine(this.C_FadeScreen(0f));
		}

		// Token: 0x060033AD RID: 13229 RVA: 0x0016A813 File Offset: 0x00168A13
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

		// Token: 0x060033AE RID: 13230 RVA: 0x0016A829 File Offset: 0x00168A29
		private Color MoveTowardsAlpha(Color color, float alpha, float maxDelta)
		{
			return new Color(color.r, color.g, color.b, Mathf.MoveTowards(color.a, alpha, maxDelta));
		}

		// Token: 0x04002DEC RID: 11756
		[SerializeField]
		private Image m_Image;

		// Token: 0x04002DED RID: 11757
		[SerializeField]
		private float m_FadeSpeed = 0.3f;
	}
}
