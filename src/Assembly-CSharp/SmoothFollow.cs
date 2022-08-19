using System;
using UnityEngine;

// Token: 0x020003C2 RID: 962
public class SmoothFollow : MonoBehaviour
{
	// Token: 0x06001F53 RID: 8019 RVA: 0x000DC2FC File Offset: 0x000DA4FC
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

	// Token: 0x06001F54 RID: 8020 RVA: 0x000DC3FC File Offset: 0x000DA5FC
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

	// Token: 0x06001F55 RID: 8021 RVA: 0x000DC568 File Offset: 0x000DA768
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

	// Token: 0x04001967 RID: 6503
	public Transform target;

	// Token: 0x04001968 RID: 6504
	public float distance = 10f;

	// Token: 0x04001969 RID: 6505
	public float height = 5f;

	// Token: 0x0400196A RID: 6506
	public float heightDamping = 2f;

	// Token: 0x0400196B RID: 6507
	public float rotationDamping = 0.3f;

	// Token: 0x0400196C RID: 6508
	public float rotate;
}
