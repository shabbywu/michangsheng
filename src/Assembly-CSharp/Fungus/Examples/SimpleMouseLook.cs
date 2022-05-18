using System;
using UnityEngine;

namespace Fungus.Examples
{
	// Token: 0x02001469 RID: 5225
	public class SimpleMouseLook : MonoBehaviour
	{
		// Token: 0x06007DE8 RID: 32232 RVA: 0x002C8144 File Offset: 0x002C6344
		private void Update()
		{
			Vector3 eulerAngles = this.target.localRotation.eulerAngles;
			eulerAngles..ctor(this.pitch - Input.GetAxis("Mouse Y"), eulerAngles.y + Input.GetAxis("Mouse X"), 0f);
			eulerAngles.z = 0f;
			eulerAngles.x = Mathf.Clamp(eulerAngles.x, -this.maxPitch, this.maxPitch);
			this.pitch = eulerAngles.x;
			this.target.localRotation = Quaternion.Euler(eulerAngles);
		}

		// Token: 0x04006B5A RID: 27482
		public float xsen = 1f;

		// Token: 0x04006B5B RID: 27483
		public float ysen = 1f;

		// Token: 0x04006B5C RID: 27484
		public float maxPitch = 60f;

		// Token: 0x04006B5D RID: 27485
		public Transform target;

		// Token: 0x04006B5E RID: 27486
		private float pitch;
	}
}
