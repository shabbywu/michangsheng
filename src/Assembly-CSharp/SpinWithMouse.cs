using System;
using UnityEngine;

// Token: 0x02000065 RID: 101
[AddComponentMenu("NGUI/Examples/Spin With Mouse")]
public class SpinWithMouse : MonoBehaviour
{
	// Token: 0x060004BB RID: 1211 RVA: 0x0000825A File Offset: 0x0000645A
	private void Start()
	{
		this.mTrans = base.transform;
	}

	// Token: 0x060004BC RID: 1212 RVA: 0x0006F94C File Offset: 0x0006DB4C
	private void OnDrag(Vector2 delta)
	{
		UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
		if (this.target != null)
		{
			this.target.localRotation = Quaternion.Euler(0f, -0.5f * delta.x * this.speed, 0f) * this.target.localRotation;
			return;
		}
		this.mTrans.localRotation = Quaternion.Euler(0f, -0.5f * delta.x * this.speed, 0f) * this.mTrans.localRotation;
	}

	// Token: 0x04000307 RID: 775
	public Transform target;

	// Token: 0x04000308 RID: 776
	public float speed = 1f;

	// Token: 0x04000309 RID: 777
	private Transform mTrans;
}
