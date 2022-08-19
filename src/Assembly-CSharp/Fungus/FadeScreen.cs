using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000DCD RID: 3533
	[CommandInfo("Camera", "Fade Screen", "Draws a fullscreen texture over the scene to give a fade effect. Setting Target Alpha to 1 will obscure the screen, alpha 0 will reveal the screen. If no Fade Texture is provided then a default flat color texture is used.", 0)]
	[AddComponentMenu("")]
	public class FadeScreen : Command
	{
		// Token: 0x06006472 RID: 25714 RVA: 0x0027EB3C File Offset: 0x0027CD3C
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

		// Token: 0x06006473 RID: 25715 RVA: 0x0027EBB8 File Offset: 0x0027CDB8
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

		// Token: 0x06006474 RID: 25716 RVA: 0x0027EC04 File Offset: 0x0027CE04
		public override Color GetButtonColor()
		{
			return new Color32(216, 228, 170, byte.MaxValue);
		}

		// Token: 0x0400564E RID: 22094
		[Tooltip("Time for fade effect to complete")]
		[SerializeField]
		protected float duration = 1f;

		// Token: 0x0400564F RID: 22095
		[Tooltip("Current target alpha transparency value. The fade gradually adjusts the alpha to approach this target value.")]
		[SerializeField]
		protected float targetAlpha = 1f;

		// Token: 0x04005650 RID: 22096
		[Tooltip("Wait until the fade has finished before executing next command")]
		[SerializeField]
		protected bool waitUntilFinished = true;

		// Token: 0x04005651 RID: 22097
		[Tooltip("Color to render fullscreen fade texture with when screen is obscured.")]
		[SerializeField]
		protected Color fadeColor = Color.black;

		// Token: 0x04005652 RID: 22098
		[Tooltip("Optional texture to use when rendering the fullscreen fade effect.")]
		[SerializeField]
		protected Texture2D fadeTexture;
	}
}
