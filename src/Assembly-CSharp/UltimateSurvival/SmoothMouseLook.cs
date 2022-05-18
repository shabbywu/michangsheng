using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000872 RID: 2162
	[AddComponentMenu("Camera-Control/Smooth Mouse Look")]
	public class SmoothMouseLook : MonoBehaviour
	{
		// Token: 0x06003802 RID: 14338 RVA: 0x001A1CB0 File Offset: 0x0019FEB0
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

		// Token: 0x06003803 RID: 14339 RVA: 0x001A2070 File Offset: 0x001A0270
		private void Start()
		{
			Rigidbody component = base.GetComponent<Rigidbody>();
			if (component)
			{
				component.freezeRotation = true;
			}
			this.originalRotation = base.transform.localRotation;
		}

		// Token: 0x06003804 RID: 14340 RVA: 0x001A20A4 File Offset: 0x001A02A4
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

		// Token: 0x0400324A RID: 12874
		public SmoothMouseLook.RotationAxes axes;

		// Token: 0x0400324B RID: 12875
		public float sensitivityX = 15f;

		// Token: 0x0400324C RID: 12876
		public float sensitivityY = 15f;

		// Token: 0x0400324D RID: 12877
		public float minimumX = -360f;

		// Token: 0x0400324E RID: 12878
		public float maximumX = 360f;

		// Token: 0x0400324F RID: 12879
		public float minimumY = -60f;

		// Token: 0x04003250 RID: 12880
		public float maximumY = 60f;

		// Token: 0x04003251 RID: 12881
		private float rotationX;

		// Token: 0x04003252 RID: 12882
		private float rotationY;

		// Token: 0x04003253 RID: 12883
		private List<float> rotArrayX = new List<float>();

		// Token: 0x04003254 RID: 12884
		private float rotAverageX;

		// Token: 0x04003255 RID: 12885
		private List<float> rotArrayY = new List<float>();

		// Token: 0x04003256 RID: 12886
		private float rotAverageY;

		// Token: 0x04003257 RID: 12887
		public float frameCounter = 20f;

		// Token: 0x04003258 RID: 12888
		private Quaternion originalRotation;

		// Token: 0x02000873 RID: 2163
		public enum RotationAxes
		{
			// Token: 0x0400325A RID: 12890
			MouseXAndY,
			// Token: 0x0400325B RID: 12891
			MouseX,
			// Token: 0x0400325C RID: 12892
			MouseY
		}
	}
}
