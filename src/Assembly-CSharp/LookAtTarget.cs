using System;
using UnityEngine;

// Token: 0x02000046 RID: 70
[AddComponentMenu("NGUI/Examples/Look At Target")]
public class LookAtTarget : MonoBehaviour
{
	// Token: 0x06000459 RID: 1113 RVA: 0x00017F57 File Offset: 0x00016157
	private void Start()
	{
		this.mTrans = base.transform;
	}

	// Token: 0x0600045A RID: 1114 RVA: 0x00017F68 File Offset: 0x00016168
	private void LateUpdate()
	{
		if (this.target != null)
		{
			Vector3 vector = this.target.position - this.mTrans.position;
			if (vector.magnitude > 0.001f)
			{
				Quaternion quaternion = Quaternion.LookRotation(vector);
				this.mTrans.rotation = Quaternion.Slerp(this.mTrans.rotation, quaternion, Mathf.Clamp01(this.speed * Time.deltaTime));
			}
		}
	}

	// Token: 0x04000283 RID: 643
	public int level;

	// Token: 0x04000284 RID: 644
	public Transform target;

	// Token: 0x04000285 RID: 645
	public float speed = 8f;

	// Token: 0x04000286 RID: 646
	private Transform mTrans;
}
