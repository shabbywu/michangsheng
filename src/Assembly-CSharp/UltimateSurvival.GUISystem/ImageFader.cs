using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem;

[Serializable]
public class ImageFader
{
	[SerializeField]
	private Image m_Image;

	[SerializeField]
	[Range(0f, 1f)]
	private float m_MinAlpha = 0.4f;

	[SerializeField]
	[Range(0f, 100f)]
	private float m_FadeInSpeed = 25f;

	[SerializeField]
	[Range(0f, 100f)]
	private float m_FadeOutSpeed = 0.3f;

	[SerializeField]
	[Range(0f, 10f)]
	private float m_FadeOutPause = 0.5f;

	private Coroutine m_FadeHandler;

	public bool Fading { get; private set; }

	public void DoFadeCycle(MonoBehaviour parent, float targetAlpha)
	{
		if ((Object)(object)m_Image == (Object)null)
		{
			Debug.LogError((object)"[ImageFader] - The image to fade is not assigned!");
			return;
		}
		targetAlpha = Mathf.Clamp01(Mathf.Max(Mathf.Abs(targetAlpha), m_MinAlpha));
		if (m_FadeHandler != null)
		{
			parent.StopCoroutine(m_FadeHandler);
		}
		m_FadeHandler = parent.StartCoroutine(C_DoFadeCycle(targetAlpha));
	}

	private IEnumerator C_DoFadeCycle(float targetAlpha)
	{
		Fading = true;
		while (Mathf.Abs(((Graphic)m_Image).color.a - targetAlpha) > 0.01f)
		{
			((Graphic)m_Image).color = Color.Lerp(((Graphic)m_Image).color, new Color(((Graphic)m_Image).color.r, ((Graphic)m_Image).color.g, ((Graphic)m_Image).color.b, targetAlpha), m_FadeInSpeed * Time.deltaTime);
			yield return null;
		}
		((Graphic)m_Image).color = new Color(((Graphic)m_Image).color.r, ((Graphic)m_Image).color.g, ((Graphic)m_Image).color.b, targetAlpha);
		if (m_FadeOutPause > 0f)
		{
			yield return (object)new WaitForSeconds(m_FadeOutPause);
		}
		while (((Graphic)m_Image).color.a > 0.01f)
		{
			((Graphic)m_Image).color = Color.Lerp(((Graphic)m_Image).color, new Color(((Graphic)m_Image).color.r, ((Graphic)m_Image).color.g, ((Graphic)m_Image).color.b, 0f), m_FadeOutSpeed * Time.deltaTime);
			yield return null;
		}
		((Graphic)m_Image).color = new Color(((Graphic)m_Image).color.r, ((Graphic)m_Image).color.g, ((Graphic)m_Image).color.b, 0f);
		Fading = false;
	}
}
