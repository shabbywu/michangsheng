using System;
using UnityEngine;

// Token: 0x02000124 RID: 292
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("NGUI/UI/Viewport Camera")]
public class UIViewport : MonoBehaviour
{
	// Token: 0x06000B6E RID: 2926 RVA: 0x0000D7B4 File Offset: 0x0000B9B4
	private void Start()
	{
		this.mCam = base.GetComponent<Camera>();
		if (this.sourceCamera == null)
		{
			this.sourceCamera = Camera.main;
		}
	}

	// Token: 0x06000B6F RID: 2927 RVA: 0x0009282C File Offset: 0x00090A2C
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

	// Token: 0x0400082A RID: 2090
	public Camera sourceCamera;

	// Token: 0x0400082B RID: 2091
	public Transform topLeft;

	// Token: 0x0400082C RID: 2092
	public Transform bottomRight;

	// Token: 0x0400082D RID: 2093
	public float fullSize = 1f;

	// Token: 0x0400082E RID: 2094
	private Camera mCam;
}
