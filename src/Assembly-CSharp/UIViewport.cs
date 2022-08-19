using System;
using UnityEngine;

// Token: 0x020000B5 RID: 181
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("NGUI/UI/Viewport Camera")]
public class UIViewport : MonoBehaviour
{
	// Token: 0x06000A91 RID: 2705 RVA: 0x0004040B File Offset: 0x0003E60B
	private void Start()
	{
		this.mCam = base.GetComponent<Camera>();
		if (this.sourceCamera == null)
		{
			this.sourceCamera = Camera.main;
		}
	}

	// Token: 0x06000A92 RID: 2706 RVA: 0x00040434 File Offset: 0x0003E634
	private void LateUpdate()
	{
		if (this.topLeft != null && this.bottomRight != null)
		{
			Vector3 vector = this.sourceCamera.WorldToScreenPoint(this.topLeft.position);
			Vector3 vector2 = this.sourceCamera.WorldToScreenPoint(this.bottomRight.position);
			Rect rect;
			rect..ctor(vector.x / (float)Screen.width, vector2.y / (float)Screen.height, (vector2.x - vector.x) / (float)Screen.width, (vector.y - vector2.y) / (float)Screen.height);
			float num = this.fullSize * rect.height;
			if (rect != this.mCam.rect)
			{
				this.mCam.rect = rect;
			}
			if (this.mCam.orthographicSize != num)
			{
				this.mCam.orthographicSize = num;
			}
		}
	}

	// Token: 0x04000686 RID: 1670
	public Camera sourceCamera;

	// Token: 0x04000687 RID: 1671
	public Transform topLeft;

	// Token: 0x04000688 RID: 1672
	public Transform bottomRight;

	// Token: 0x04000689 RID: 1673
	public float fullSize = 1f;

	// Token: 0x0400068A RID: 1674
	private Camera mCam;
}
