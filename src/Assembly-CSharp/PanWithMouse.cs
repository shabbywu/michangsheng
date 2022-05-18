using System;
using UnityEngine;

// Token: 0x02000060 RID: 96
[AddComponentMenu("NGUI/Examples/Pan With Mouse")]
public class PanWithMouse : MonoBehaviour
{
	// Token: 0x060004AC RID: 1196 RVA: 0x00008161 File Offset: 0x00006361
	private void Start()
	{
		this.mTrans = base.transform;
		this.mStart = this.mTrans.localRotation;
	}

	// Token: 0x060004AD RID: 1197 RVA: 0x0006F440 File Offset: 0x0006D640
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

	// Token: 0x040002F7 RID: 759
	public Vector2 degrees = new Vector2(5f, 3f);

	// Token: 0x040002F8 RID: 760
	public float range = 1f;

	// Token: 0x040002F9 RID: 761
	private Transform mTrans;

	// Token: 0x040002FA RID: 762
	private Quaternion mStart;

	// Token: 0x040002FB RID: 763
	private Vector2 mRot = Vector2.zero;
}
