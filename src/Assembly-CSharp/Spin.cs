using System;
using UnityEngine;

// Token: 0x02000064 RID: 100
[AddComponentMenu("NGUI/Examples/Spin")]
public class Spin : MonoBehaviour
{
	// Token: 0x060004B6 RID: 1206 RVA: 0x000081D9 File Offset: 0x000063D9
	private void Start()
	{
		this.mTrans = base.transform;
		this.mRb = base.GetComponent<Rigidbody>();
	}

	// Token: 0x060004B7 RID: 1207 RVA: 0x000081F3 File Offset: 0x000063F3
	private void Update()
	{
		if (this.mRb == null)
		{
			this.ApplyDelta(this.ignoreTimeScale ? RealTime.deltaTime : Time.deltaTime);
		}
	}

	// Token: 0x060004B8 RID: 1208 RVA: 0x0000821D File Offset: 0x0000641D
	private void FixedUpdate()
	{
		if (this.mRb != null)
		{
			this.ApplyDelta(Time.deltaTime);
		}
	}

	// Token: 0x060004B9 RID: 1209 RVA: 0x0006F8DC File Offset: 0x0006DADC
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

	// Token: 0x04000303 RID: 771
	public Vector3 rotationsPerSecond = new Vector3(0f, 0.1f, 0f);

	// Token: 0x04000304 RID: 772
	public bool ignoreTimeScale;

	// Token: 0x04000305 RID: 773
	private Rigidbody mRb;

	// Token: 0x04000306 RID: 774
	private Transform mTrans;
}
