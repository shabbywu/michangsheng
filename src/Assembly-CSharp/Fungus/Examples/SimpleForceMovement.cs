using System;
using UnityEngine;

namespace Fungus.Examples
{
	// Token: 0x02000FB0 RID: 4016
	public class SimpleForceMovement : MonoBehaviour
	{
		// Token: 0x06006FEC RID: 28652 RVA: 0x002A88B8 File Offset: 0x002A6AB8
		private void FixedUpdate()
		{
			Vector3 vector = this.getForwardFrom.forward;
			vector.y = 0f;
			vector.Normalize();
			Vector3 vector2 = this.getForwardFrom.right;
			vector2.y = 0f;
			vector2.Normalize();
			vector *= Input.GetAxis("Vertical");
			vector2 *= Input.GetAxis("Horizontal");
			Vector3 vector3 = vector + vector2;
			if (vector3.magnitude > 1f)
			{
				vector3 = vector3.normalized;
			}
			this.rb.AddForce(vector3 * this.forceScale);
		}

		// Token: 0x04005C5F RID: 23647
		public Rigidbody rb;

		// Token: 0x04005C60 RID: 23648
		public Transform getForwardFrom;

		// Token: 0x04005C61 RID: 23649
		public float forceScale;
	}
}
