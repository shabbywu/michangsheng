using System;
using UnityEngine;

// Token: 0x0200006A RID: 106
[AddComponentMenu("NGUI/Examples/Window Auto-Yaw")]
public class WindowAutoYaw : MonoBehaviour
{
	// Token: 0x060004C9 RID: 1225 RVA: 0x00008320 File Offset: 0x00006520
	private void OnDisable()
	{
		this.mTrans.localRotation = Quaternion.identity;
	}

	// Token: 0x060004CA RID: 1226 RVA: 0x00008332 File Offset: 0x00006532
	private void OnEnable()
	{
		if (this.uiCamera == null)
		{
			this.uiCamera = NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
		this.mTrans = base.transform;
	}

	// Token: 0x060004CB RID: 1227 RVA: 0x00070020 File Offset: 0x0006E220
	private void Update()
	{
		if (this.uiCamera != null)
		{
			Vector3 vector = this.uiCamera.WorldToViewportPoint(this.mTrans.position);
			this.mTrans.localRotation = Quaternion.Euler(0f, (vector.x * 2f - 1f) * this.yawAmount, 0f);
		}
	}

	// Token: 0x0400031F RID: 799
	public int updateOrder;

	// Token: 0x04000320 RID: 800
	public Camera uiCamera;

	// Token: 0x04000321 RID: 801
	public float yawAmount = 20f;

	// Token: 0x04000322 RID: 802
	private Transform mTrans;
}
