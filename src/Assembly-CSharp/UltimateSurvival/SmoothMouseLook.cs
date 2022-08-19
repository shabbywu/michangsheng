using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005B5 RID: 1461
	[AddComponentMenu("Camera-Control/Smooth Mouse Look")]
	public class SmoothMouseLook : MonoBehaviour
	{
		// Token: 0x06002F7E RID: 12158 RVA: 0x00157970 File Offset: 0x00155B70
		private void Update()
		{
			if (this.axes == SmoothMouseLook.RotationAxes.MouseXAndY)
			{
				this.rotAverageY = 0f;
				this.rotAverageX = 0f;
				this.rotationY += Input.GetAxis("Mouse Y") * this.sensitivityY;
				this.rotationX += Input.GetAxis("Mouse X") * this.sensitivityX;
				this.rotArrayY.Add(this.rotationY);
				this.rotArrayX.Add(this.rotationX);
				if ((float)this.rotArrayY.Count >= this.frameCounter)
				{
					this.rotArrayY.RemoveAt(0);
				}
				if ((float)this.rotArrayX.Count >= this.frameCounter)
				{
					this.rotArrayX.RemoveAt(0);
				}
				for (int i = 0; i < this.rotArrayY.Count; i++)
				{
					this.rotAverageY += this.rotArrayY[i];
				}
				for (int j = 0; j < this.rotArrayX.Count; j++)
				{
					this.rotAverageX += this.rotArrayX[j];
				}
				this.rotAverageY /= (float)this.rotArrayY.Count;
				this.rotAverageX /= (float)this.rotArrayX.Count;
				this.rotAverageY = SmoothMouseLook.ClampAngle(this.rotAverageY, this.minimumY, this.maximumY);
				this.rotAverageX = SmoothMouseLook.ClampAngle(this.rotAverageX, this.minimumX, this.maximumX);
				Quaternion quaternion = Quaternion.AngleAxis(this.rotAverageY, Vector3.left);
				Quaternion quaternion2 = Quaternion.AngleAxis(this.rotAverageX, Vector3.up);
				base.transform.localRotation = this.originalRotation * quaternion2 * quaternion;
				return;
			}
			if (this.axes == SmoothMouseLook.RotationAxes.MouseX)
			{
				this.rotAverageX = 0f;
				this.rotationX += Input.GetAxis("Mouse X") * this.sensitivityX;
				this.rotArrayX.Add(this.rotationX);
				if ((float)this.rotArrayX.Count >= this.frameCounter)
				{
					this.rotArrayX.RemoveAt(0);
				}
				for (int k = 0; k < this.rotArrayX.Count; k++)
				{
					this.rotAverageX += this.rotArrayX[k];
				}
				this.rotAverageX /= (float)this.rotArrayX.Count;
				this.rotAverageX = SmoothMouseLook.ClampAngle(this.rotAverageX, this.minimumX, this.maximumX);
				Quaternion quaternion3 = Quaternion.AngleAxis(this.rotAverageX, Vector3.up);
				base.transform.localRotation = this.originalRotation * quaternion3;
				return;
			}
			this.rotAverageY = 0f;
			this.rotationY += Input.GetAxis("Mouse Y") * this.sensitivityY;
			this.rotArrayY.Add(this.rotationY);
			if ((float)this.rotArrayY.Count >= this.frameCounter)
			{
				this.rotArrayY.RemoveAt(0);
			}
			for (int l = 0; l < this.rotArrayY.Count; l++)
			{
				this.rotAverageY += this.rotArrayY[l];
			}
			this.rotAverageY /= (float)this.rotArrayY.Count;
			this.rotAverageY = SmoothMouseLook.ClampAngle(this.rotAverageY, this.minimumY, this.maximumY);
			Quaternion quaternion4 = Quaternion.AngleAxis(this.rotAverageY, Vector3.left);
			base.transform.localRotation = this.originalRotation * quaternion4;
		}

		// Token: 0x06002F7F RID: 12159 RVA: 0x00157D30 File Offset: 0x00155F30
		private void Start()
		{
			Rigidbody component = base.GetComponent<Rigidbody>();
			if (component)
			{
				component.freezeRotation = true;
			}
			this.originalRotation = base.transform.localRotation;
		}

		// Token: 0x06002F80 RID: 12160 RVA: 0x00157D64 File Offset: 0x00155F64
		public static float ClampAngle(float angle, float min, float max)
		{
			angle %= 360f;
			if (angle >= -360f && angle <= 360f)
			{
				if (angle < -360f)
				{
					angle += 360f;
				}
				if (angle > 360f)
				{
					angle -= 360f;
				}
			}
			return Mathf.Clamp(angle, min, max);
		}

		// Token: 0x040029C4 RID: 10692
		public SmoothMouseLook.RotationAxes axes;

		// Token: 0x040029C5 RID: 10693
		public float sensitivityX = 15f;

		// Token: 0x040029C6 RID: 10694
		public float sensitivityY = 15f;

		// Token: 0x040029C7 RID: 10695
		public float minimumX = -360f;

		// Token: 0x040029C8 RID: 10696
		public float maximumX = 360f;

		// Token: 0x040029C9 RID: 10697
		public float minimumY = -60f;

		// Token: 0x040029CA RID: 10698
		public float maximumY = 60f;

		// Token: 0x040029CB RID: 10699
		private float rotationX;

		// Token: 0x040029CC RID: 10700
		private float rotationY;

		// Token: 0x040029CD RID: 10701
		private List<float> rotArrayX = new List<float>();

		// Token: 0x040029CE RID: 10702
		private float rotAverageX;

		// Token: 0x040029CF RID: 10703
		private List<float> rotArrayY = new List<float>();

		// Token: 0x040029D0 RID: 10704
		private float rotAverageY;

		// Token: 0x040029D1 RID: 10705
		public float frameCounter = 20f;

		// Token: 0x040029D2 RID: 10706
		private Quaternion originalRotation;

		// Token: 0x020014A7 RID: 5287
		public enum RotationAxes
		{
			// Token: 0x04006CC9 RID: 27849
			MouseXAndY,
			// Token: 0x04006CCA RID: 27850
			MouseX,
			// Token: 0x04006CCB RID: 27851
			MouseY
		}
	}
}
