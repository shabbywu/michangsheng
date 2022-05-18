using System;
using UnityEngine;

// Token: 0x02000554 RID: 1364
public class SmoothFollow : MonoBehaviour
{
	// Token: 0x060022D6 RID: 8918 RVA: 0x0011F038 File Offset: 0x0011D238
	public void ResetView()
	{
		if (!this.target)
		{
			return;
		}
		float y = this.target.eulerAngles.y;
		float num = this.target.position.y + this.height;
		float num2 = base.transform.position.y;
		num2 = Mathf.Lerp(num2, num, this.heightDamping * Time.deltaTime);
		Quaternion quaternion = Quaternion.Euler(0f, y, 0f);
		base.transform.position = this.target.position;
		base.transform.position -= quaternion * Vector3.forward * this.distance;
		base.transform.position = new Vector3(base.transform.position.x, num2, base.transform.position.z);
		base.transform.LookAt(this.target);
	}

	// Token: 0x060022D7 RID: 8919 RVA: 0x0011F138 File Offset: 0x0011D338
	public void FollowUpdate()
	{
		if (!this.target)
		{
			return;
		}
		float y = this.target.eulerAngles.y;
		float num = this.target.position.y + this.height;
		float num2 = base.transform.eulerAngles.y;
		float num3 = base.transform.position.y;
		float num4 = y - num2;
		if (num4 > 180f)
		{
			num4 -= 360f;
		}
		else if (num4 < -180f)
		{
			num4 += 360f;
		}
		if (num4 > 90f)
		{
			num4 = 180f - num4;
		}
		if (num4 < -90f)
		{
			num4 = -180f - num4;
		}
		num2 = Mathf.LerpAngle(num2, num2 + num4, this.rotationDamping * Time.deltaTime);
		num3 = Mathf.Lerp(num3, num, this.heightDamping * Time.deltaTime);
		Quaternion quaternion = Quaternion.Euler(0f, num2, 0f);
		base.transform.position = this.target.position;
		base.transform.position -= quaternion * Vector3.forward * this.distance;
		base.transform.position = new Vector3(base.transform.position.x, num3, base.transform.position.z);
		base.transform.LookAt(this.target);
	}

	// Token: 0x060022D8 RID: 8920 RVA: 0x0011F2A4 File Offset: 0x0011D4A4
	public void rotateCamerX(float rotatex)
	{
		if (!this.target)
		{
			return;
		}
		Vector3 eulerAngles = this.target.eulerAngles;
		float num = this.target.position.y + this.height;
		float num2 = base.transform.eulerAngles.y;
		float num3 = base.transform.position.y;
		float num4 = rotatex;
		if (num4 > 180f)
		{
			num4 -= 360f;
		}
		else if (num4 < -180f)
		{
			num4 += 360f;
		}
		if (num4 > 90f)
		{
			num4 = 180f - num4;
		}
		if (num4 < -90f)
		{
			num4 = -180f - num4;
		}
		num2 = Mathf.LerpAngle(num2, num2 + num4, this.rotationDamping * Time.deltaTime * 30f);
		num3 = Mathf.Lerp(num3, num, this.heightDamping * Time.deltaTime * 10f);
		Quaternion quaternion = Quaternion.Euler(0f, num2, 0f);
		base.transform.position = this.target.position;
		base.transform.position -= quaternion * Vector3.forward * this.distance;
		base.transform.position = new Vector3(base.transform.position.x, num3, base.transform.position.z);
		base.transform.LookAt(this.target);
	}

	// Token: 0x04001DE8 RID: 7656
	public Transform target;

	// Token: 0x04001DE9 RID: 7657
	public float distance = 10f;

	// Token: 0x04001DEA RID: 7658
	public float height = 5f;

	// Token: 0x04001DEB RID: 7659
	public float heightDamping = 2f;

	// Token: 0x04001DEC RID: 7660
	public float rotationDamping = 0.3f;

	// Token: 0x04001DED RID: 7661
	public float rotate;
}
