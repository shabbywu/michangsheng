using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000DCF RID: 3535
	[CommandInfo("Camera", "Fade To View", "Fades the camera out and in again at a position specified by a View object.", 0)]
	[AddComponentMenu("")]
	public class FadeToView : Command
	{
		// Token: 0x0600647E RID: 25726 RVA: 0x0027EE18 File Offset: 0x0027D018
		protected virtual void Start()
		{
			this.AcquireCamera();
		}

		// Token: 0x0600647F RID: 25727 RVA: 0x0027EE20 File Offset: 0x0027D020
		protected virtual void AcquireCamera()
		{
			if (this.targetCamera != null)
			{
				return;
			}
			this.targetCamera = Camera.main;
			if (this.targetCamera == null)
			{
				this.targetCamera = Object.FindObjectOfType<Camera>();
			}
		}

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x06006480 RID: 25728 RVA: 0x0027EE55 File Offset: 0x0027D055
		public virtual View TargetView
		{
			get
			{
				return this.targetView;
			}
		}

		// Token: 0x06006481 RID: 25729 RVA: 0x0027EE60 File Offset: 0x0027D060
		public override void OnEnter()
		{
			this.AcquireCamera();
			if (this.targetCamera == null || this.targetView == null)
			{
				this.Continue();
				return;
			}
			CameraManager cameraManager = FungusManager.Instance.CameraManager;
			if (this.fadeTexture)
			{
				cameraManager.ScreenFadeTexture = this.fadeTexture;
			}
			else
			{
				cameraManager.ScreenFadeTexture = CameraManager.CreateColorTexture(this.fadeColor, 32, 32);
			}
			cameraManager.FadeToView(this.targetCamera, this.targetView, this.duration, this.fadeOut, delegate
			{
				if (this.waitUntilFinished)
				{
					this.Continue();
				}
			});
			if (!this.waitUntilFinished)
			{
				this.Continue();
			}
		}

		// Token: 0x06006482 RID: 25730 RVA: 0x0027EF09 File Offset: 0x0027D109
		public override void OnStopExecuting()
		{
			FungusManager.Instance.CameraManager.Stop();
		}

		// Token: 0x06006483 RID: 25731 RVA: 0x0027EF1A File Offset: 0x0027D11A
		public override string GetSummary()
		{
			if (this.targetView == null)
			{
				return "Error: No view selected";
			}
			return this.targetView.name;
		}

		// Token: 0x06006484 RID: 25732 RVA: 0x0027EC04 File Offset: 0x0027CE04
		public override Color GetButtonColor()
		{
			return new Color32(216, 228, 170, byte.MaxValue);
		}

		// Token: 0x04005659 RID: 22105
		[Tooltip("Time for fade effect to complete")]
		[SerializeField]
		protected float duration = 1f;

		// Token: 0x0400565A RID: 22106
		[Tooltip("Fade from fully visible to opaque at start of fade")]
		[SerializeField]
		protected bool fadeOut = true;

		// Token: 0x0400565B RID: 22107
		[Tooltip("View to transition to when Fade is complete")]
		[SerializeField]
		protected View targetView;

		// Token: 0x0400565C RID: 22108
		[Tooltip("Wait until the fade has finished before executing next command")]
		[SerializeField]
		protected bool waitUntilFinished = true;

		// Token: 0x0400565D RID: 22109
		[Tooltip("Color to render fullscreen fade texture with when screen is obscured.")]
		[SerializeField]
		protected Color fadeColor = Color.black;

		// Token: 0x0400565E RID: 22110
		[Tooltip("Optional texture to use when rendering the fullscreen fade effect.")]
		[SerializeField]
		protected Texture2D fadeTexture;

		// Token: 0x0400565F RID: 22111
		[Tooltip("Camera to use for the fade. Will use main camera if set to none.")]
		[SerializeField]
		protected Camera targetCamera;
	}
}
