using System;
using UnityEngine;

namespace EpicToonFX
{
	// Token: 0x02000B23 RID: 2851
	public class ETFXMouseOrbit : MonoBehaviour
	{
		// Token: 0x06004F8D RID: 20365 RVA: 0x00219B08 File Offset: 0x00217D08
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

		// Token: 0x06004F8E RID: 20366 RVA: 0x00219B54 File Offset: 0x00217D54
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

		// Token: 0x06004F8F RID: 20367 RVA: 0x00003117 File Offset: 0x00001317
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

		// Token: 0x04004E81 RID: 20097
		public Transform target;

		// Token: 0x04004E82 RID: 20098
		public float distance = 5f;

		// Token: 0x04004E83 RID: 20099
		public float xSpeed = 120f;

		// Token: 0x04004E84 RID: 20100
		public float ySpeed = 120f;

		// Token: 0x04004E85 RID: 20101
		public float yMinLimit = -20f;

		// Token: 0x04004E86 RID: 20102
		public float yMaxLimit = 80f;

		// Token: 0x04004E87 RID: 20103
		public float distanceMin = 0.5f;

		// Token: 0x04004E88 RID: 20104
		public float distanceMax = 15f;

		// Token: 0x04004E89 RID: 20105
		public float smoothTime = 2f;

		// Token: 0x04004E8A RID: 20106
		private float rotationYAxis;

		// Token: 0x04004E8B RID: 20107
		private float rotationXAxis;

		// Token: 0x04004E8C RID: 20108
		private float velocityX;

		// Token: 0x04004E8D RID: 20109
		private float velocityY;
	}
}
