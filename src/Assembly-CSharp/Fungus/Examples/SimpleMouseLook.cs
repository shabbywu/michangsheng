using System;
using UnityEngine;

namespace Fungus.Examples
{
	// Token: 0x02000FB1 RID: 4017
	public class SimpleMouseLook : MonoBehaviour
	{
		// Token: 0x06006FEE RID: 28654 RVA: 0x002A895C File Offset: 0x002A6B5C
		private void Update()
		{
			Vector3 eulerAngles = this.target.localRotation.eulerAngles;
			eulerAngles..ctor(this.pitch - Input.GetAxis("Mouse Y"), eulerAngles.y + Input.GetAxis("Mouse X"), 0f);
			eulerAngles.z = 0f;
			eulerAngles.x = Mathf.Clamp(eulerAngles.x, -this.maxPitch, this.maxPitch);
			this.pitch = eulerAngles.x;
			this.target.localRotation = Quaternion.Euler(eulerAngles);
		}

		// Token: 0x04005C62 RID: 23650
		public float xsen = 1f;

		// Token: 0x04005C63 RID: 23651
		public float ysen = 1f;

		// Token: 0x04005C64 RID: 23652
		public float maxPitch = 60f;

		// Token: 0x04005C65 RID: 23653
		public Transform target;

		// Token: 0x04005C66 RID: 23654
		private float pitch;
	}
}
