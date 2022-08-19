﻿using System;
using UnityEngine;

// Token: 0x02000048 RID: 72
[AddComponentMenu("NGUI/Examples/Pan With Mouse")]
public class PanWithMouse : MonoBehaviour
{
	// Token: 0x0600045E RID: 1118 RVA: 0x0001802B File Offset: 0x0001622B
	private void Start()
	{
		this.mTrans = base.transform;
		this.mStart = this.mTrans.localRotation;
	}

	// Token: 0x0600045F RID: 1119 RVA: 0x0001804C File Offset: 0x0001624C
	private void Update()
	{
		float deltaTime = RealTime.deltaTime;
		Vector3 mousePosition = Input.mousePosition;
		float num = (float)Screen.width * 0.5f;
		float num2 = (float)Screen.height * 0.5f;
		if (this.range < 0.1f)
		{
			this.range = 0.1f;
		}
		float num3 = Mathf.Clamp((mousePosition.x - num) / num / this.range, -1f, 1f);
		float num4 = Mathf.Clamp((mousePosition.y - num2) / num2 / this.range, -1f, 1f);
		this.mRot = Vector2.Lerp(this.mRot, new Vector2(num3, num4), deltaTime * 5f);
		this.mTrans.localRotation = this.mStart * Quaternion.Euler(-this.mRot.y * this.degrees.y, this.mRot.x * this.degrees.x, 0f);
	}

	// Token: 0x04000287 RID: 647
	public Vector2 degrees = new Vector2(5f, 3f);

	// Token: 0x04000288 RID: 648
	public float range = 1f;

	// Token: 0x04000289 RID: 649
	private Transform mTrans;

	// Token: 0x0400028A RID: 650
	private Quaternion mStart;

	// Token: 0x0400028B RID: 651
	private Vector2 mRot = Vector2.zero;
}
