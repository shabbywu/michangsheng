using System;
using UnityEngine;

// Token: 0x020000AB RID: 171
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("NGUI/UI/Orthographic Camera")]
public class UIOrthoCamera : MonoBehaviour
{
	// Token: 0x060009DF RID: 2527 RVA: 0x0003B3DC File Offset: 0x000395DC
	private void Start()
	{
		this.mCam = base.GetComponent<Camera>();
		this.mTrans = base.transform;
		this.mCam.orthographic = true;
	}

	// Token: 0x060009E0 RID: 2528 RVA: 0x0003B404 File Offset: 0x00039604
	private void Update()
	{
		float num = this.mCam.rect.yMin * (float)Screen.height;
		float num2 = (this.mCam.rect.yMax * (float)Screen.height - num) * 0.5f * this.mTrans.lossyScale.y;
		if (!Mathf.Approximately(this.mCam.orthographicSize, num2))
		{
			this.mCam.orthographicSize = num2;
		}
	}

	// Token: 0x0400060D RID: 1549
	private Camera mCam;

	// Token: 0x0400060E RID: 1550
	private Transform mTrans;
}
