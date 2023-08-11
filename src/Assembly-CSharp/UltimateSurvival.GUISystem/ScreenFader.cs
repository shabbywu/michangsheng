using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem;

public class ScreenFader : GUIBehaviour
{
	[SerializeField]
	private Image m_Image;

	[SerializeField]
	private float m_FadeSpeed = 0.3f;

	private void Start()
	{
		base.Player.Death.AddListener(On_Death);
		base.Player.Respawn.AddListener(On_Respawn);
	}

	private void On_Death()
	{
		((MonoBehaviour)this).StopAllCoroutines();
		((MonoBehaviour)this).StartCoroutine(C_FadeScreen(1f));
	}

	private void On_Respawn()
	{
		((MonoBehaviour)this).StopAllCoroutines();
		((MonoBehaviour)this).StartCoroutine(C_FadeScreen(0f));
	}

	private IEnumerator C_FadeScreen(float targetAlpha)
	{
		while (Mathf.Abs(((Graphic)m_Image).color.a - targetAlpha) > 0f)
		{
			((Graphic)m_Image).color = MoveTowardsAlpha(((Graphic)m_Image).color, targetAlpha, Time.deltaTime * m_FadeSpeed);
			AudioListener.volume = 1f - ((Graphic)m_Image).color.a;
			yield return null;
		}
	}

	private Color MoveTowardsAlpha(Color color, float alpha, float maxDelta)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		return new Color(color.r, color.g, color.b, Mathf.MoveTowards(color.a, alpha, maxDelta));
	}
}
