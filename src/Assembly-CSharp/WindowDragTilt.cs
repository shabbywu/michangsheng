using System;
using UnityEngine;

// Token: 0x02000052 RID: 82
[AddComponentMenu("NGUI/Examples/Window Drag Tilt")]
public class WindowDragTilt : MonoBehaviour
{
	// Token: 0x0600047F RID: 1151 RVA: 0x00018E88 File Offset: 0x00017088
	private void OnEnable()
	{
		this.mTrans = base.transform;
		this.mLastPos = this.mTrans.position;
	}

	// Token: 0x06000480 RID: 1152 RVA: 0x00018EA8 File Offset: 0x000170A8
	private void Update()
	{
		Vector3 vector = this.mTrans.position - this.mLastPos;
		this.mLastPos = this.mTrans.position;
		this.mAngle += vector.x * this.degrees;
		this.mAngle = NGUIMath.SpringLerp(this.mAngle, 0f, 20f, Time.deltaTime);
		this.mTrans.localRotation = Quaternion.Euler(0f, 0f, -this.mAngle);
	}

	// Token: 0x040002B0 RID: 688
	public int updateOrder;

	// Token: 0x040002B1 RID: 689
	public float degrees = 30f;

	// Token: 0x040002B2 RID: 690
	private Vector3 mLastPos;

	// Token: 0x040002B3 RID: 691
	private Transform mTrans;

	// Token: 0x040002B4 RID: 692
	private float mAngle;
}
