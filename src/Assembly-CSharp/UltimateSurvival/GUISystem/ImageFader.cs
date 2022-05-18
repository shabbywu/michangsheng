using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x02000950 RID: 2384
	[Serializable]
	public class ImageFader
	{
		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06003CEB RID: 15595 RVA: 0x0002BE73 File Offset: 0x0002A073
		// (set) Token: 0x06003CEC RID: 15596 RVA: 0x0002BE7B File Offset: 0x0002A07B
		public bool Fading { get; private set; }

		// Token: 0x06003CED RID: 15597 RVA: 0x001B2740 File Offset: 0x001B0940
		public void DoFadeCycle(MonoBehaviour parent, float targetAlpha)
		{
			if (this.m_Image == null)
			{
				Debug.LogError("[ImageFader] - The image to fade is not assigned!");
				return;
			}
			targetAlpha = Mathf.Clamp01(Mathf.Max(Mathf.Abs(targetAlpha), this.m_MinAlpha));
			if (this.m_FadeHandler != null)
			{
				parent.StopCoroutine(this.m_FadeHandler);
			}
			this.m_FadeHandler = parent.StartCoroutine(this.C_DoFadeCycle(targetAlpha));
		}

		// Token: 0x06003CEE RID: 15598 RVA: 0x0002BE84 File Offset: 0x0002A084
		private IEnumerator C_DoFadeCycle(float targetAlpha)
		{
			this.Fading = true;
			while (Mathf.Abs(this.m_Image.color.a - targetAlpha) > 0.01f)
			{
				this.m_Image.color = Color.Lerp(this.m_Image.color, new Color(this.m_Image.color.r, this.m_Image.color.g, this.m_Image.color.b, targetAlpha), this.m_FadeInSpeed * Time.deltaTime);
				yield return null;
			}
			this.m_Image.color = new Color(this.m_Image.color.r, this.m_Image.color.g, this.m_Image.color.b, targetAlpha);
			if (this.m_FadeOutPause > 0f)
			{
				yield return new WaitForSeconds(this.m_FadeOutPause);
			}
			while (this.m_Image.color.a > 0.01f)
			{
				this.m_Image.color = Color.Lerp(this.m_Image.color, new Color(this.m_Image.color.r, this.m_Image.color.g, this.m_Image.color.b, 0f), this.m_FadeOutSpeed * Time.deltaTime);
				yield return null;
			}
			this.m_Image.color = new Color(this.m_Image.color.r, this.m_Image.color.g, this.m_Image.color.b, 0f);
			this.Fading = false;
			yield break;
		}

		// Token: 0x0400372D RID: 14125
		[SerializeField]
		private Image m_Image;

		// Token: 0x0400372E RID: 14126
		[SerializeField]
		[Range(0f, 1f)]
		private float m_MinAlpha = 0.4f;

		// Token: 0x0400372F RID: 14127
		[SerializeField]
		[Range(0f, 100f)]
		private float m_FadeInSpeed = 25f;

		// Token: 0x04003730 RID: 14128
		[SerializeField]
		[Range(0f, 100f)]
		private float m_FadeOutSpeed = 0.3f;

		// Token: 0x04003731 RID: 14129
		[SerializeField]
		[Range(0f, 10f)]
		private float m_FadeOutPause = 0.5f;

		// Token: 0x04003732 RID: 14130
		private Coroutine m_FadeHandler;
	}
}
