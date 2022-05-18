using System;
using UnityEngine;

// Token: 0x0200006B RID: 107
[AddComponentMenu("NGUI/Examples/Window Drag Tilt")]
public class WindowDragTilt : MonoBehaviour
{
	// Token: 0x060004CD RID: 1229 RVA: 0x00008377 File Offset: 0x00006577
	private void OnEnable()
	{
		this.mTrans = base.transform;
		this.mLastPos = this.mTrans.position;
	}

	// Token: 0x060004CE RID: 1230 RVA: 0x00070088 File Offset: 0x0006E288
	private void Update()
	{
		Vector3 vector = this.mTrans.position - this.mLastPos;
		this.mLastPos = this.mTrans.position;
		this.mAngle += vector.x * this.degrees;
		this.mAngle = NGUIMath.SpringLerp(this.mAngle, 0f, 20f, Time.deltaTime);
		this.mTrans.localRotation = Quaternion.Euler(0f, 0f, -this.mAngle);
	}

	// Token: 0x04000323 RID: 803
	public int updateOrder;

	// Token: 0x04000324 RID: 804
	public float degrees = 30f;

	// Token: 0x04000325 RID: 805
	private Vector3 mLastPos;

	// Token: 0x04000326 RID: 806
	private Transform mTrans;

	// Token: 0x04000327 RID: 807
	private float mAngle;
}
