using System;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000E24 RID: 3620
	public class ConstrainedCamera : MonoBehaviour
	{
		// Token: 0x0600573E RID: 22334 RVA: 0x00244730 File Offset: 0x00242930
		private void LateUpdate()
		{
			Vector3 vector = this.target.position + this.offset;
			vector.x = Mathf.Clamp(vector.x, this.min.x, this.max.x);
			vector.y = Mathf.Clamp(vector.y, this.min.y, this.max.y);
			vector.z = Mathf.Clamp(vector.z, this.min.z, this.max.z);
			base.transform.position = Vector3.Lerp(base.transform.position, vector, this.smoothing * Time.deltaTime);
		}

		// Token: 0x0400571B RID: 22299
		public Transform target;

		// Token: 0x0400571C RID: 22300
		public Vector3 offset;

		// Token: 0x0400571D RID: 22301
		public Vector3 min;

		// Token: 0x0400571E RID: 22302
		public Vector3 max;

		// Token: 0x0400571F RID: 22303
		public float smoothing = 5f;
	}
}
