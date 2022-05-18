using System;
using UnityEngine;

// Token: 0x0200005B RID: 91
[AddComponentMenu("NGUI/Examples/Lag Position")]
public class LagPosition : MonoBehaviour
{
	// Token: 0x0600049F RID: 1183 RVA: 0x00008091 File Offset: 0x00006291
	private void OnEnable()
	{
		this.mTrans = base.transform;
		this.mAbsolute = this.mTrans.position;
		this.mRelative = this.mTrans.localPosition;
	}

	// Token: 0x060004A0 RID: 1184 RVA: 0x0006F214 File Offset: 0x0006D414
	private void Update()
	{
		Transform parent = this.mTrans.parent;
		if (parent != null)
		{
			float num = this.ignoreTimeScale ? RealTime.deltaTime : Time.deltaTime;
			Vector3 vector = parent.position + parent.rotation * this.mRelative;
			this.mAbsolute.x = Mathf.Lerp(this.mAbsolute.x, vector.x, Mathf.Clamp01(num * this.speed.x));
			this.mAbsolute.y = Mathf.Lerp(this.mAbsolute.y, vector.y, Mathf.Clamp01(num * this.speed.y));
			this.mAbsolute.z = Mathf.Lerp(this.mAbsolute.z, vector.z, Mathf.Clamp01(num * this.speed.z));
			this.mTrans.position = this.mAbsolute;
		}
	}

	// Token: 0x040002E6 RID: 742
	public int updateOrder;

	// Token: 0x040002E7 RID: 743
	public Vector3 speed = new Vector3(10f, 10f, 10f);

	// Token: 0x040002E8 RID: 744
	public bool ignoreTimeScale;

	// Token: 0x040002E9 RID: 745
	private Transform mTrans;

	// Token: 0x040002EA RID: 746
	private Vector3 mRelative;

	// Token: 0x040002EB RID: 747
	private Vector3 mAbsolute;
}
