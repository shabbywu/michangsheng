using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200129D RID: 4765
	[CommandInfo("Camera", "Start Swipe", "Activates swipe panning mode where the player can pan the camera within the area between viewA & viewB.", 0)]
	[AddComponentMenu("")]
	public class StartSwipe : Command
	{
		// Token: 0x0600737D RID: 29565 RVA: 0x0004ECB5 File Offset: 0x0004CEB5
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

		// Token: 0x0600737E RID: 29566 RVA: 0x002AB370 File Offset: 0x002A9570
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

		// Token: 0x0600737F RID: 29567 RVA: 0x002AB3E8 File Offset: 0x002A95E8
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

		// Token: 0x06007380 RID: 29568 RVA: 0x0004CAB8 File Offset: 0x0004ACB8
		public override Color GetButtonColor()
		{
			return new Color32(216, 228, 170, byte.MaxValue);
		}

		// Token: 0x04006562 RID: 25954
		[Tooltip("Defines one extreme of the scrollable area that the player can pan around")]
		[SerializeField]
		protected View viewA;

		// Token: 0x04006563 RID: 25955
		[Tooltip("Defines one extreme of the scrollable area that the player can pan around")]
		[SerializeField]
		protected View viewB;

		// Token: 0x04006564 RID: 25956
		[Tooltip("Time to move the camera to a valid starting position between the two views")]
		[SerializeField]
		protected float duration = 0.5f;

		// Token: 0x04006565 RID: 25957
		[Tooltip("Multiplier factor for speed of swipe pan")]
		[SerializeField]
		protected float speedMultiplier = 1f;

		// Token: 0x04006566 RID: 25958
		[Tooltip("Camera to use for the pan. Will use main camera if set to none.")]
		[SerializeField]
		protected Camera targetCamera;
	}
}
