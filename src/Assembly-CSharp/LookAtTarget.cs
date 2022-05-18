using System;
using UnityEngine;

// Token: 0x0200005E RID: 94
[AddComponentMenu("NGUI/Examples/Look At Target")]
public class LookAtTarget : MonoBehaviour
{
	// Token: 0x060004A7 RID: 1191 RVA: 0x00008140 File Offset: 0x00006340
	private void Start()
	{
		this.mTrans = base.transform;
	}

	// Token: 0x060004A8 RID: 1192 RVA: 0x0006F38C File Offset: 0x0006D58C
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

	// Token: 0x040002F3 RID: 755
	public int level;

	// Token: 0x040002F4 RID: 756
	public Transform target;

	// Token: 0x040002F5 RID: 757
	public float speed = 8f;

	// Token: 0x040002F6 RID: 758
	private Transform mTrans;
}
