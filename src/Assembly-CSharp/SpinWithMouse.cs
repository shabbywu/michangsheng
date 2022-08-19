using System;
using UnityEngine;

// Token: 0x0200004D RID: 77
[AddComponentMenu("NGUI/Examples/Spin With Mouse")]
public class SpinWithMouse : MonoBehaviour
{
	// Token: 0x0600046D RID: 1133 RVA: 0x00018635 File Offset: 0x00016835
	private void Start()
	{
		this.mTrans = base.transform;
	}

	// Token: 0x0600046E RID: 1134 RVA: 0x00018644 File Offset: 0x00016844
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

	// Token: 0x04000297 RID: 663
	public Transform target;

	// Token: 0x04000298 RID: 664
	public float speed = 1f;

	// Token: 0x04000299 RID: 665
	private Transform mTrans;
}
