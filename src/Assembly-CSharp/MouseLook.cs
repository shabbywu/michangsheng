using System;
using UnityEngine;

// Token: 0x020001F6 RID: 502
[AddComponentMenu("Camera-Control/Mouse Look")]
public class MouseLook : MonoBehaviour
{
	// Token: 0x06001497 RID: 5271 RVA: 0x00083EB4 File Offset: 0x000820B4
	private void Update()
	{
		if (Input.GetMouseButton(1))
		{
			if (this.axes == MouseLook.RotationAxes.MouseXAndY)
			{
				float num = base.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * this.sensitivityX;
				this.rotationY += Input.GetAxis("Mouse Y") * this.sensitivityY;
				this.rotationY = Mathf.Clamp(this.rotationY, this.minimumY, this.maximumY);
				base.transform.localEulerAngles = new Vector3(-this.rotationY, num, 0f);
				return;
			}
			if (this.axes == MouseLook.RotationAxes.MouseX)
			{
				base.transform.Rotate(0f, Input.GetAxis("Mouse X") * this.sensitivityX, 0f);
				return;
			}
			this.rotationY += Input.GetAxis("Mouse Y") * this.sensitivityY;
			this.rotationY = Mathf.Clamp(this.rotationY, this.minimumY, this.maximumY);
			base.transform.localEulerAngles = new Vector3(-this.rotationY, base.transform.localEulerAngles.y, 0f);
		}
	}

	// Token: 0x06001498 RID: 5272 RVA: 0x00083FE7 File Offset: 0x000821E7
	private void Start()
	{
		if (base.GetComponent<Rigidbody>())
		{
			base.GetComponent<Rigidbody>().freezeRotation = true;
		}
	}

	// Token: 0x04000F54 RID: 3924
	public MouseLook.RotationAxes axes;

	// Token: 0x04000F55 RID: 3925
	public float sensitivityX = 15f;

	// Token: 0x04000F56 RID: 3926
	public float sensitivityY = 15f;

	// Token: 0x04000F57 RID: 3927
	public float minimumX = -360f;

	// Token: 0x04000F58 RID: 3928
	public float maximumX = 360f;

	// Token: 0x04000F59 RID: 3929
	public float minimumY = -60f;

	// Token: 0x04000F5A RID: 3930
	public float maximumY = 60f;

	// Token: 0x04000F5B RID: 3931
	private float rotationY;

	// Token: 0x020012DF RID: 4831
	public enum RotationAxes
	{
		// Token: 0x040066E8 RID: 26344
		MouseXAndY,
		// Token: 0x040066E9 RID: 26345
		MouseX,
		// Token: 0x040066EA RID: 26346
		MouseY
	}
}
