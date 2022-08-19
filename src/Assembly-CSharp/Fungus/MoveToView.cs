using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E09 RID: 3593
	[CommandInfo("Camera", "Move To View", "Moves the camera to a location specified by a View object.", 0)]
	[AddComponentMenu("")]
	public class MoveToView : Command
	{
		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x06006575 RID: 25973 RVA: 0x0028342C File Offset: 0x0028162C
		public virtual View TargetView
		{
			get
			{
				return this.targetView;
			}
		}

		// Token: 0x06006576 RID: 25974 RVA: 0x00283434 File Offset: 0x00281634
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

		// Token: 0x06006577 RID: 25975 RVA: 0x00283469 File Offset: 0x00281669
		public virtual void Start()
		{
			this.AcquireCamera();
		}

		// Token: 0x06006578 RID: 25976 RVA: 0x00283474 File Offset: 0x00281674
		public override void OnEnter()
		{
			this.AcquireCamera();
			if (this.targetCamera == null || this.targetView == null)
			{
				this.Continue();
				return;
			}
			CameraManager cameraManager = FungusManager.Instance.CameraManager;
			Vector3 position = this.targetView.transform.position;
			Quaternion rotation = this.targetView.transform.rotation;
			float viewSize = this.targetView.ViewSize;
			cameraManager.PanToPosition(this.targetCamera, position, rotation, viewSize, this.duration, delegate
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

		// Token: 0x06006579 RID: 25977 RVA: 0x0027EF09 File Offset: 0x0027D109
		public override void OnStopExecuting()
		{
			FungusManager.Instance.CameraManager.Stop();
		}

		// Token: 0x0600657A RID: 25978 RVA: 0x00283510 File Offset: 0x00281710
		public override string GetSummary()
		{
			if (this.targetView == null)
			{
				return "Error: No view selected";
			}
			return this.targetView.name;
		}

		// Token: 0x0600657B RID: 25979 RVA: 0x0027EC04 File Offset: 0x0027CE04
		public override Color GetButtonColor()
		{
			return new Color32(216, 228, 170, byte.MaxValue);
		}

		// Token: 0x0400572C RID: 22316
		[Tooltip("Time for move effect to complete")]
		[SerializeField]
		protected float duration = 1f;

		// Token: 0x0400572D RID: 22317
		[Tooltip("View to transition to when move is complete")]
		[SerializeField]
		protected View targetView;

		// Token: 0x0400572E RID: 22318
		[Tooltip("Wait until the fade has finished before executing next command")]
		[SerializeField]
		protected bool waitUntilFinished = true;

		// Token: 0x0400572F RID: 22319
		[Tooltip("Camera to use for the pan. Will use main camera if set to none.")]
		[SerializeField]
		protected Camera targetCamera;
	}
}
