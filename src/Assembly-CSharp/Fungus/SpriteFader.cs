using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E86 RID: 3718
	[RequireComponent(typeof(SpriteRenderer))]
	public class SpriteFader : MonoBehaviour
	{
		// Token: 0x06006964 RID: 26980 RVA: 0x00290EAB File Offset: 0x0028F0AB
		protected virtual void Start()
		{
			this.spriteRenderer = (base.GetComponent<Renderer>() as SpriteRenderer);
		}

		// Token: 0x06006965 RID: 26981 RVA: 0x00290EC0 File Offset: 0x0028F0C0
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

		// Token: 0x06006966 RID: 26982 RVA: 0x00290FD8 File Offset: 0x0028F1D8
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

		// Token: 0x04005958 RID: 22872
		protected float fadeDuration;

		// Token: 0x04005959 RID: 22873
		protected float fadeTimer;

		// Token: 0x0400595A RID: 22874
		protected Color startColor;

		// Token: 0x0400595B RID: 22875
		protected Color endColor;

		// Token: 0x0400595C RID: 22876
		protected Vector2 slideOffset;

		// Token: 0x0400595D RID: 22877
		protected Vector3 endPosition;

		// Token: 0x0400595E RID: 22878
		protected SpriteRenderer spriteRenderer;

		// Token: 0x0400595F RID: 22879
		protected Action onFadeComplete;
	}
}
