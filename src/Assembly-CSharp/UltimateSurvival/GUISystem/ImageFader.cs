using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x02000651 RID: 1617
	[Serializable]
	public class ImageFader
	{
		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06003375 RID: 13173 RVA: 0x00169823 File Offset: 0x00167A23
		// (set) Token: 0x06003376 RID: 13174 RVA: 0x0016982B File Offset: 0x00167A2B
		public bool Fading { get; private set; }

		// Token: 0x06003377 RID: 13175 RVA: 0x00169834 File Offset: 0x00167A34
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

		// Token: 0x06003378 RID: 13176 RVA: 0x00169899 File Offset: 0x00167A99
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

		// Token: 0x04002DB6 RID: 11702
		[SerializeField]
		private Image m_Image;

		// Token: 0x04002DB7 RID: 11703
		[SerializeField]
		[Range(0f, 1f)]
		private float m_MinAlpha = 0.4f;

		// Token: 0x04002DB8 RID: 11704
		[SerializeField]
		[Range(0f, 100f)]
		private float m_FadeInSpeed = 25f;

		// Token: 0x04002DB9 RID: 11705
		[SerializeField]
		[Range(0f, 100f)]
		private float m_FadeOutSpeed = 0.3f;

		// Token: 0x04002DBA RID: 11706
		[SerializeField]
		[Range(0f, 10f)]
		private float m_FadeOutPause = 0.5f;

		// Token: 0x04002DBB RID: 11707
		private Coroutine m_FadeHandler;
	}
}
