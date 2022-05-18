using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001209 RID: 4617
	[CommandInfo("Camera", "Fade To View", "Fades the camera out and in again at a position specified by a View object.", 0)]
	[AddComponentMenu("")]
	public class FadeToView : Command
	{
		// Token: 0x060070FD RID: 28925 RVA: 0x0004CBAE File Offset: 0x0004ADAE
		protected virtual void Start()
		{
			this.AcquireCamera();
		}

		// Token: 0x060070FE RID: 28926 RVA: 0x0004CBB6 File Offset: 0x0004ADB6
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

		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x060070FF RID: 28927 RVA: 0x0004CBEB File Offset: 0x0004ADEB
		public virtual View TargetView
		{
			get
			{
				return this.targetView;
			}
		}

		// Token: 0x06007100 RID: 28928 RVA: 0x002A3C5C File Offset: 0x002A1E5C
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

		// Token: 0x06007101 RID: 28929 RVA: 0x0004CBF3 File Offset: 0x0004ADF3
		public override void OnStopExecuting()
		{
			FungusManager.Instance.CameraManager.Stop();
		}

		// Token: 0x06007102 RID: 28930 RVA: 0x0004CC04 File Offset: 0x0004AE04
		public override string GetSummary()
		{
			if (this.targetView == null)
			{
				return "Error: No view selected";
			}
			return this.targetView.name;
		}

		// Token: 0x06007103 RID: 28931 RVA: 0x0004CAB8 File Offset: 0x0004ACB8
		public override Color GetButtonColor()
		{
			return new Color32(216, 228, 170, byte.MaxValue);
		}

		// Token: 0x04006358 RID: 25432
		[Tooltip("Time for fade effect to complete")]
		[SerializeField]
		protected float duration = 1f;

		// Token: 0x04006359 RID: 25433
		[Tooltip("Fade from fully visible to opaque at start of fade")]
		[SerializeField]
		protected bool fadeOut = true;

		// Token: 0x0400635A RID: 25434
		[Tooltip("View to transition to when Fade is complete")]
		[SerializeField]
		protected View targetView;

		// Token: 0x0400635B RID: 25435
		[Tooltip("Wait until the fade has finished before executing next command")]
		[SerializeField]
		protected bool waitUntilFinished = true;

		// Token: 0x0400635C RID: 25436
		[Tooltip("Color to render fullscreen fade texture with when screen is obscured.")]
		[SerializeField]
		protected Color fadeColor = Color.black;

		// Token: 0x0400635D RID: 25437
		[Tooltip("Optional texture to use when rendering the fullscreen fade effect.")]
		[SerializeField]
		protected Texture2D fadeTexture;

		// Token: 0x0400635E RID: 25438
		[Tooltip("Camera to use for the fade. Will use main camera if set to none.")]
		[SerializeField]
		protected Camera targetCamera;
	}
}
