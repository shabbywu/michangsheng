using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020012F7 RID: 4855
	[RequireComponent(typeof(SpriteRenderer))]
	public class SpriteFader : MonoBehaviour
	{
		// Token: 0x06007663 RID: 30307 RVA: 0x00050942 File Offset: 0x0004EB42
		protected virtual void Start()
		{
			this.spriteRenderer = (base.GetComponent<Renderer>() as SpriteRenderer);
		}

		// Token: 0x06007664 RID: 30308 RVA: 0x002B3368 File Offset: 0x002B1568
		protected virtual void Update()
		{
			this.fadeTimer += Time.deltaTime;
			if (this.fadeTimer > this.fadeDuration)
			{
				this.spriteRenderer.color = this.endColor;
				if (this.slideOffset.magnitude > 0f)
				{
					base.transform.position = this.endPosition;
				}
				Object.Destroy(this);
				if (this.onFadeComplete != null)
				{
					this.onFadeComplete();
					return;
				}
			}
			else
			{
				float num = Mathf.SmoothStep(0f, 1f, this.fadeTimer / this.fadeDuration);
				this.spriteRenderer.color = Color.Lerp(this.startColor, this.endColor, num);
				if (this.slideOffset.magnitude > 0f)
				{
					Vector3 vector = this.endPosition;
					vector.x += this.slideOffset.x;
					vector.y += this.slideOffset.y;
					base.transform.position = Vector3.Lerp(vector, this.endPosition, num);
				}
			}
		}

		// Token: 0x06007665 RID: 30309 RVA: 0x002B3480 File Offset: 0x002B1680
		public static void FadeSprite(SpriteRenderer spriteRenderer, Color targetColor, float duration, Vector2 slideOffset, Action onComplete = null)
		{
			if (spriteRenderer == null)
			{
				Debug.LogError("Sprite renderer must not be null");
				return;
			}
			foreach (SpriteRenderer spriteRenderer2 in spriteRenderer.gameObject.GetComponentsInChildren<SpriteRenderer>())
			{
				if (!(spriteRenderer2 == spriteRenderer))
				{
					SpriteFader.FadeSprite(spriteRenderer2, targetColor, duration, slideOffset, null);
				}
			}
			SpriteFader component = spriteRenderer.GetComponent<SpriteFader>();
			if (component != null)
			{
				Object.Destroy(component);
			}
			if (Mathf.Approximately(duration, 0f))
			{
				spriteRenderer.color = targetColor;
				if (onComplete != null)
				{
					onComplete();
				}
				return;
			}
			SpriteFader spriteFader = spriteRenderer.gameObject.AddComponent<SpriteFader>();
			spriteFader.fadeDuration = duration;
			spriteFader.startColor = spriteRenderer.color;
			spriteFader.endColor = targetColor;
			spriteFader.endPosition = spriteRenderer.transform.position;
			spriteFader.slideOffset = slideOffset;
			spriteFader.onFadeComplete = onComplete;
		}

		// Token: 0x04006739 RID: 26425
		protected float fadeDuration;

		// Token: 0x0400673A RID: 26426
		protected float fadeTimer;

		// Token: 0x0400673B RID: 26427
		protected Color startColor;

		// Token: 0x0400673C RID: 26428
		protected Color endColor;

		// Token: 0x0400673D RID: 26429
		protected Vector2 slideOffset;

		// Token: 0x0400673E RID: 26430
		protected Vector3 endPosition;

		// Token: 0x0400673F RID: 26431
		protected SpriteRenderer spriteRenderer;

		// Token: 0x04006740 RID: 26432
		protected Action onFadeComplete;
	}
}
