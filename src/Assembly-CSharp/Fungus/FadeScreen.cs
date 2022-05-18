using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001207 RID: 4615
	[CommandInfo("Camera", "Fade Screen", "Draws a fullscreen texture over the scene to give a fade effect. Setting Target Alpha to 1 will obscure the screen, alpha 0 will reveal the screen. If no Fade Texture is provided then a default flat color texture is used.", 0)]
	[AddComponentMenu("")]
	public class FadeScreen : Command
	{
		// Token: 0x060070F1 RID: 28913 RVA: 0x002A3A74 File Offset: 0x002A1C74
		public override void OnEnter()
		{
			CameraManager cameraManager = FungusManager.Instance.CameraManager;
			if (this.fadeTexture)
			{
				cameraManager.ScreenFadeTexture = this.fadeTexture;
			}
			else
			{
				cameraManager.ScreenFadeTexture = CameraManager.CreateColorTexture(this.fadeColor, 32, 32);
			}
			Tools.canClickFlag = false;
			cameraManager.Fade(this.targetAlpha, this.duration, delegate
			{
				if (this.waitUntilFinished)
				{
					Tools.canClickFlag = true;
					this.Continue();
				}
			});
			if (!this.waitUntilFinished)
			{
				this.Continue();
			}
		}

		// Token: 0x060070F2 RID: 28914 RVA: 0x002A3AF0 File Offset: 0x002A1CF0
		public override string GetSummary()
		{
			return string.Concat(new object[]
			{
				"Fade to ",
				this.targetAlpha,
				" over ",
				this.duration,
				" seconds"
			});
		}

		// Token: 0x060070F3 RID: 28915 RVA: 0x0004CAB8 File Offset: 0x0004ACB8
		public override Color GetButtonColor()
		{
			return new Color32(216, 228, 170, byte.MaxValue);
		}

		// Token: 0x0400634D RID: 25421
		[Tooltip("Time for fade effect to complete")]
		[SerializeField]
		protected float duration = 1f;

		// Token: 0x0400634E RID: 25422
		[Tooltip("Current target alpha transparency value. The fade gradually adjusts the alpha to approach this target value.")]
		[SerializeField]
		protected float targetAlpha = 1f;

		// Token: 0x0400634F RID: 25423
		[Tooltip("Wait until the fade has finished before executing next command")]
		[SerializeField]
		protected bool waitUntilFinished = true;

		// Token: 0x04006350 RID: 25424
		[Tooltip("Color to render fullscreen fade texture with when screen is obscured.")]
		[SerializeField]
		protected Color fadeColor = Color.black;

		// Token: 0x04006351 RID: 25425
		[Tooltip("Optional texture to use when rendering the fullscreen fade effect.")]
		[SerializeField]
		protected Texture2D fadeTexture;
	}
}
