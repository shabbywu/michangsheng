using System;
using UnityEngine;

// Token: 0x0200005C RID: 92
[AddComponentMenu("NGUI/Examples/Lag Rotation")]
public class LagRotation : MonoBehaviour
{
	// Token: 0x060004A2 RID: 1186 RVA: 0x000080E3 File Offset: 0x000062E3
	private void OnEnable()
	{
		this.mTrans = base.transform;
		this.mRelative = this.mTrans.localRotation;
		this.mAbsolute = this.mTrans.rotation;
	}

	// Token: 0x060004A3 RID: 1187 RVA: 0x0006F318 File Offset: 0x0006D518
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

	// Token: 0x040002EC RID: 748
	public int updateOrder;

	// Token: 0x040002ED RID: 749
	public float speed = 10f;

	// Token: 0x040002EE RID: 750
	public bool ignoreTimeScale;

	// Token: 0x040002EF RID: 751
	private Transform mTrans;

	// Token: 0x040002F0 RID: 752
	private Quaternion mRelative;

	// Token: 0x040002F1 RID: 753
	private Quaternion mAbsolute;
}
