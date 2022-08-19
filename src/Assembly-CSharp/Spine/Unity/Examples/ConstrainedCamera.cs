using System;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000ADB RID: 2779
	public class ConstrainedCamera : MonoBehaviour
	{
		// Token: 0x06004DDA RID: 19930 RVA: 0x002147BC File Offset: 0x002129BC
		private void LateUpdate()
		{
			Vector3 vector = this.target.position + this.offset;
			vector.x = Mathf.Clamp(vector.x, this.min.x, this.max.x);
			vector.y = Mathf.Clamp(vector.y, this.min.y, this.max.y);
			vector.z = Mathf.Clamp(vector.z, this.min.z, this.max.z);
			base.transform.position = Vector3.Lerp(base.transform.position, vector, this.smoothing * Time.deltaTime);
		}

		// Token: 0x04004D21 RID: 19745
		public Transform target;

		// Token: 0x04004D22 RID: 19746
		public Vector3 offset;

		// Token: 0x04004D23 RID: 19747
		public Vector3 min;

		// Token: 0x04004D24 RID: 19748
		public Vector3 max;

		// Token: 0x04004D25 RID: 19749
		public float smoothing = 5f;
	}
}
