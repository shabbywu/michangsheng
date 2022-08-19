using System;
using UnityEngine;

// Token: 0x0200004C RID: 76
[AddComponentMenu("NGUI/Examples/Spin")]
public class Spin : MonoBehaviour
{
	// Token: 0x06000468 RID: 1128 RVA: 0x00018542 File Offset: 0x00016742
	private void Start()
	{
		this.mTrans = base.transform;
		this.mRb = base.GetComponent<Rigidbody>();
	}

	// Token: 0x06000469 RID: 1129 RVA: 0x0001855C File Offset: 0x0001675C
	private void Update()
	{
		if (this.mRb == null)
		{
			this.ApplyDelta(this.ignoreTimeScale ? RealTime.deltaTime : Time.deltaTime);
		}
	}

	// Token: 0x0600046A RID: 1130 RVA: 0x00018586 File Offset: 0x00016786
	private void FixedUpdate()
	{
		if (this.mRb != null)
		{
			this.ApplyDelta(Time.deltaTime);
		}
	}

	// Token: 0x0600046B RID: 1131 RVA: 0x000185A4 File Offset: 0x000167A4
	public void ApplyDelta(float delta)
	{
		delta *= 360f;
		Quaternion quaternion = Quaternion.Euler(this.rotationsPerSecond * delta);
		if (this.mRb == null)
		{
			this.mTrans.rotation = this.mTrans.rotation * quaternion;
			return;
		}
		this.mRb.MoveRotation(this.mRb.rotation * quaternion);
	}

	// Token: 0x04000293 RID: 659
	public Vector3 rotationsPerSecond = new Vector3(0f, 0.1f, 0f);

	// Token: 0x04000294 RID: 660
	public bool ignoreTimeScale;

	// Token: 0x04000295 RID: 661
	private Rigidbody mRb;

	// Token: 0x04000296 RID: 662
	private Transform mTrans;
}
