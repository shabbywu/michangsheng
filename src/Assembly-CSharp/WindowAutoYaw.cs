using System;
using UnityEngine;

// Token: 0x02000051 RID: 81
[AddComponentMenu("NGUI/Examples/Window Auto-Yaw")]
public class WindowAutoYaw : MonoBehaviour
{
	// Token: 0x0600047B RID: 1147 RVA: 0x00018DCB File Offset: 0x00016FCB
	private void OnDisable()
	{
		this.mTrans.localRotation = Quaternion.identity;
	}

	// Token: 0x0600047C RID: 1148 RVA: 0x00018DDD File Offset: 0x00016FDD
	private void OnEnable()
	{
		if (this.uiCamera == null)
		{
			this.uiCamera = NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
		this.mTrans = base.transform;
	}

	// Token: 0x0600047D RID: 1149 RVA: 0x00018E10 File Offset: 0x00017010
	private void Update()
	{
		if (this.uiCamera != null)
		{
			Vector3 vector = this.uiCamera.WorldToViewportPoint(this.mTrans.position);
			this.mTrans.localRotation = Quaternion.Euler(0f, (vector.x * 2f - 1f) * this.yawAmount, 0f);
		}
	}

	// Token: 0x040002AC RID: 684
	public int updateOrder;

	// Token: 0x040002AD RID: 685
	public Camera uiCamera;

	// Token: 0x040002AE RID: 686
	public float yawAmount = 20f;

	// Token: 0x040002AF RID: 687
	private Transform mTrans;
}
