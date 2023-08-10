using System;
using UnityEngine;

namespace Fungus;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteFader : MonoBehaviour
{
	protected float fadeDuration;

	protected float fadeTimer;

	protected Color startColor;

	protected Color endColor;

	protected Vector2 slideOffset;

	protected Vector3 endPosition;

	protected SpriteRenderer spriteRenderer;

	protected Action onFadeComplete;

	protected virtual void Start()
	{
		ref SpriteRenderer reference = ref spriteRenderer;
		Renderer component = ((Component)this).GetComponent<Renderer>();
		reference = (SpriteRenderer)(object)((component is SpriteRenderer) ? component : null);
	}

	protected virtual void Update()
	{
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		fadeTimer += Time.deltaTime;
		if (fadeTimer > fadeDuration)
		{
			spriteRenderer.color = endColor;
			if (((Vector2)(ref slideOffset)).magnitude > 0f)
			{
				((Component)this).transform.position = endPosition;
			}
			Object.Destroy((Object)(object)this);
			if (onFadeComplete != null)
			{
				onFadeComplete();
			}
		}
		else
		{
			float num = Mathf.SmoothStep(0f, 1f, fadeTimer / fadeDuration);
			spriteRenderer.color = Color.Lerp(startColor, endColor, num);
			if (((Vector2)(ref slideOffset)).magnitude > 0f)
			{
				Vector3 val = endPosition;
				val.x += slideOffset.x;
				val.y += slideOffset.y;
				((Component)this).transform.position = Vector3.Lerp(val, endPosition, num);
			}
		}
	}

	public static void FadeSprite(SpriteRenderer spriteRenderer, Color targetColor, float duration, Vector2 slideOffset, Action onComplete = null)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)spriteRenderer == (Object)null)
		{
			Debug.LogError((object)"Sprite renderer must not be null");
			return;
		}
		SpriteRenderer[] componentsInChildren = ((Component)spriteRenderer).gameObject.GetComponentsInChildren<SpriteRenderer>();
		foreach (SpriteRenderer val in componentsInChildren)
		{
			if (!((Object)(object)val == (Object)(object)spriteRenderer))
			{
				FadeSprite(val, targetColor, duration, slideOffset);
			}
		}
		SpriteFader component = ((Component)spriteRenderer).GetComponent<SpriteFader>();
		if ((Object)(object)component != (Object)null)
		{
			Object.Destroy((Object)(object)component);
		}
		if (Mathf.Approximately(duration, 0f))
		{
			spriteRenderer.color = targetColor;
			onComplete?.Invoke();
			return;
		}
		SpriteFader spriteFader = ((Component)spriteRenderer).gameObject.AddComponent<SpriteFader>();
		spriteFader.fadeDuration = duration;
		spriteFader.startColor = spriteRenderer.color;
		spriteFader.endColor = targetColor;
		spriteFader.endPosition = ((Component)spriteRenderer).transform.position;
		spriteFader.slideOffset = slideOffset;
		spriteFader.onFadeComplete = onComplete;
	}
}
