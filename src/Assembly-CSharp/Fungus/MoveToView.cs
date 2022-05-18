using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001256 RID: 4694
	[CommandInfo("Camera", "Move To View", "Moves the camera to a location specified by a View object.", 0)]
	[AddComponentMenu("")]
	public class MoveToView : Command
	{
		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x06007203 RID: 29187 RVA: 0x0004D8D5 File Offset: 0x0004BAD5
		public virtual View TargetView
		{
			get
			{
				return this.targetView;
			}
		}

		// Token: 0x06007204 RID: 29188 RVA: 0x0004D8DD File Offset: 0x0004BADD
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

		// Token: 0x06007205 RID: 29189 RVA: 0x0004D912 File Offset: 0x0004BB12
		public virtual void Start()
		{
			this.AcquireCamera();
		}

		// Token: 0x06007206 RID: 29190 RVA: 0x002A7670 File Offset: 0x002A5870
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

		// Token: 0x06007207 RID: 29191 RVA: 0x0004CBF3 File Offset: 0x0004ADF3
		public override void OnStopExecuting()
		{
			FungusManager.Instance.CameraManager.Stop();
		}

		// Token: 0x06007208 RID: 29192 RVA: 0x0004D91A File Offset: 0x0004BB1A
		public override string GetSummary()
		{
			if (this.targetView == null)
			{
				return "Error: No view selected";
			}
			return this.targetView.name;
		}

		// Token: 0x06007209 RID: 29193 RVA: 0x0004CAB8 File Offset: 0x0004ACB8
		public override Color GetButtonColor()
		{
			return new Color32(216, 228, 170, byte.MaxValue);
		}

		// Token: 0x04006461 RID: 25697
		[Tooltip("Time for move effect to complete")]
		[SerializeField]
		protected float duration = 1f;

		// Token: 0x04006462 RID: 25698
		[Tooltip("View to transition to when move is complete")]
		[SerializeField]
		protected View targetView;

		// Token: 0x04006463 RID: 25699
		[Tooltip("Wait until the fade has finished before executing next command")]
		[SerializeField]
		protected bool waitUntilFinished = true;

		// Token: 0x04006464 RID: 25700
		[Tooltip("Camera to use for the pan. Will use main camera if set to none.")]
		[SerializeField]
		protected Camera targetCamera;
	}
}
