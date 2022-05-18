using System;
using UnityEngine;

// Token: 0x0200030A RID: 778
[AddComponentMenu("Camera-Control/Mouse Look")]
public class MouseLook : MonoBehaviour
{
	// Token: 0x06001741 RID: 5953 RVA: 0x000CCB70 File Offset: 0x000CAD70
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

	// Token: 0x06001742 RID: 5954 RVA: 0x000148C3 File Offset: 0x00012AC3
	private void Start()
	{
		if (base.GetComponent<Rigidbody>())
		{
			base.GetComponent<Rigidbody>().freezeRotation = true;
		}
	}

	// Token: 0x04001296 RID: 4758
	public MouseLook.RotationAxes axes;

	// Token: 0x04001297 RID: 4759
	public float sensitivityX = 15f;

	// Token: 0x04001298 RID: 4760
	public float sensitivityY = 15f;

	// Token: 0x04001299 RID: 4761
	public float minimumX = -360f;

	// Token: 0x0400129A RID: 4762
	public float maximumX = 360f;

	// Token: 0x0400129B RID: 4763
	public float minimumY = -60f;

	// Token: 0x0400129C RID: 4764
	public float maximumY = 60f;

	// Token: 0x0400129D RID: 4765
	private float rotationY;

	// Token: 0x0200030B RID: 779
	public enum RotationAxes
	{
		// Token: 0x0400129F RID: 4767
		MouseXAndY,
		// Token: 0x040012A0 RID: 4768
		MouseX,
		// Token: 0x040012A1 RID: 4769
		MouseY
	}
}
