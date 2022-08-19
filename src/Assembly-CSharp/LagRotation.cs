using System;
using UnityEngine;

// Token: 0x02000044 RID: 68
[AddComponentMenu("NGUI/Examples/Lag Rotation")]
public class LagRotation : MonoBehaviour
{
	// Token: 0x06000454 RID: 1108 RVA: 0x00017E87 File Offset: 0x00016087
	private void OnEnable()
	{
		this.mTrans = base.transform;
		this.mRelative = this.mTrans.localRotation;
		this.mAbsolute = this.mTrans.rotation;
	}

	// Token: 0x06000455 RID: 1109 RVA: 0x00017EB8 File Offset: 0x000160B8
	private void Update()
	{
		Transform parent = this.mTrans.parent;
		if (parent != null)
		{
			float num = this.ignoreTimeScale ? RealTime.deltaTime : Time.deltaTime;
			this.mAbsolute = Quaternion.Slerp(this.mAbsolute, parent.rotation * this.mRelative, num * this.speed);
			this.mTrans.rotation = this.mAbsolute;
		}
	}

	// Token: 0x0400027C RID: 636
	public int updateOrder;

	// Token: 0x0400027D RID: 637
	public float speed = 10f;

	// Token: 0x0400027E RID: 638
	public bool ignoreTimeScale;

	// Token: 0x0400027F RID: 639
	private Transform mTrans;

	// Token: 0x04000280 RID: 640
	private Quaternion mRelative;

	// Token: 0x04000281 RID: 641
	private Quaternion mAbsolute;
}
