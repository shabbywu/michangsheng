using System;
using UnityEngine;

namespace EpicToonFX
{
	// Token: 0x02000E96 RID: 3734
	public class ETFXMouseOrbit : MonoBehaviour
	{
		// Token: 0x060059AC RID: 22956 RVA: 0x00249938 File Offset: 0x00247B38
		private void Start()
		{
			Vector3 eulerAngles = base.transform.eulerAngles;
			this.rotationYAxis = eulerAngles.y;
			this.rotationXAxis = eulerAngles.x;
			if (base.GetComponent<Rigidbody>())
			{
				base.GetComponent<Rigidbody>().freezeRotation = true;
			}
		}

		// Token: 0x060059AD RID: 22957 RVA: 0x00249984 File Offset: 0x00247B84
		private void LateUpdate()
		{
			if (this.target)
			{
				if (Input.GetMouseButton(1))
				{
					this.velocityX += this.xSpeed * Input.GetAxis("Mouse X") * this.distance * 0.02f;
					this.velocityY += this.ySpeed * Input.GetAxis("Mouse Y") * 0.02f;
				}
				this.rotationYAxis += this.velocityX;
				this.rotationXAxis -= this.velocityY;
				this.rotationXAxis = ETFXMouseOrbit.ClampAngle(this.rotationXAxis, this.yMinLimit, this.yMaxLimit);
				Quaternion quaternion = Quaternion.Euler(this.rotationXAxis, this.rotationYAxis, 0f);
				this.distance = Mathf.Clamp(this.distance - Input.GetAxis("Mouse ScrollWheel") * 5f, this.distanceMin, this.distanceMax);
				RaycastHit raycastHit;
				if (Physics.Linecast(this.target.position, base.transform.position, ref raycastHit))
				{
					this.distance -= raycastHit.distance;
				}
				Vector3 vector;
				vector..ctor(0f, 0f, -this.distance);
				Vector3 position = quaternion * vector + this.target.position;
				base.transform.rotation = quaternion;
				base.transform.position = position;
				this.velocityX = Mathf.Lerp(this.velocityX, 0f, Time.deltaTime * this.smoothTime);
				this.velocityY = Mathf.Lerp(this.velocityY, 0f, Time.deltaTime * this.smoothTime);
			}
		}

		// Token: 0x060059AE RID: 22958 RVA: 0x0000411E File Offset: 0x0000231E
		public static float ClampAngle(float angle, float min, float max)
		{
			if (angle < -360f)
			{
				angle += 360f;
			}
			if (angle > 360f)
			{
				angle -= 360f;
			}
			return Mathf.Clamp(angle, min, max);
		}

		// Token: 0x040058F5 RID: 22773
		public Transform target;

		// Token: 0x040058F6 RID: 22774
		public float distance = 5f;

		// Token: 0x040058F7 RID: 22775
		public float xSpeed = 120f;

		// Token: 0x040058F8 RID: 22776
		public float ySpeed = 120f;

		// Token: 0x040058F9 RID: 22777
		public float yMinLimit = -20f;

		// Token: 0x040058FA RID: 22778
		public float yMaxLimit = 80f;

		// Token: 0x040058FB RID: 22779
		public float distanceMin = 0.5f;

		// Token: 0x040058FC RID: 22780
		public float distanceMax = 15f;

		// Token: 0x040058FD RID: 22781
		public float smoothTime = 2f;

		// Token: 0x040058FE RID: 22782
		private float rotationYAxis;

		// Token: 0x040058FF RID: 22783
		private float rotationXAxis;

		// Token: 0x04005900 RID: 22784
		private float velocityX;

		// Token: 0x04005901 RID: 22785
		private float velocityY;
	}
}
