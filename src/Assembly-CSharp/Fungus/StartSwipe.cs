using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E4C RID: 3660
	[CommandInfo("Camera", "Start Swipe", "Activates swipe panning mode where the player can pan the camera within the area between viewA & viewB.", 0)]
	[AddComponentMenu("")]
	public class StartSwipe : Command
	{
		// Token: 0x060066EF RID: 26351 RVA: 0x0028851F File Offset: 0x0028671F
		public virtual void Start()
		{
			if (this.targetCamera == null)
			{
				this.targetCamera = Camera.main;
			}
			if (this.targetCamera == null)
			{
				this.targetCamera = Object.FindObjectOfType<Camera>();
			}
		}

		// Token: 0x060066F0 RID: 26352 RVA: 0x00288554 File Offset: 0x00286754
		public override void OnEnter()
		{
			if (this.targetCamera == null || this.viewA == null || this.viewB == null)
			{
				this.Continue();
				return;
			}
			FungusManager.Instance.CameraManager.StartSwipePan(this.targetCamera, this.viewA, this.viewB, this.duration, this.speedMultiplier, delegate
			{
				this.Continue();
			});
		}

		// Token: 0x060066F1 RID: 26353 RVA: 0x002885CC File Offset: 0x002867CC
		public override string GetSummary()
		{
			if (this.viewA == null)
			{
				return "Error: No view selected for View A";
			}
			if (this.viewB == null)
			{
				return "Error: No view selected for View B";
			}
			return this.viewA.name + " to " + this.viewB.name;
		}

		// Token: 0x060066F2 RID: 26354 RVA: 0x0027EC04 File Offset: 0x0027CE04
		public override Color GetButtonColor()
		{
			return new Color32(216, 228, 170, byte.MaxValue);
		}

		// Token: 0x0400581E RID: 22558
		[Tooltip("Defines one extreme of the scrollable area that the player can pan around")]
		[SerializeField]
		protected View viewA;

		// Token: 0x0400581F RID: 22559
		[Tooltip("Defines one extreme of the scrollable area that the player can pan around")]
		[SerializeField]
		protected View viewB;

		// Token: 0x04005820 RID: 22560
		[Tooltip("Time to move the camera to a valid starting position between the two views")]
		[SerializeField]
		protected float duration = 0.5f;

		// Token: 0x04005821 RID: 22561
		[Tooltip("Multiplier factor for speed of swipe pan")]
		[SerializeField]
		protected float speedMultiplier = 1f;

		// Token: 0x04005822 RID: 22562
		[Tooltip("Camera to use for the pan. Will use main camera if set to none.")]
		[SerializeField]
		protected Camera targetCamera;
	}
}
