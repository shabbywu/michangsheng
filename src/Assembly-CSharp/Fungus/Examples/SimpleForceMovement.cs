using System;
using UnityEngine;

namespace Fungus.Examples
{
	// Token: 0x02001468 RID: 5224
	public class SimpleForceMovement : MonoBehaviour
	{
		// Token: 0x06007DE6 RID: 32230 RVA: 0x002C80A0 File Offset: 0x002C62A0
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

		// Token: 0x04006B57 RID: 27479
		public Rigidbody rb;

		// Token: 0x04006B58 RID: 27480
		public Transform getForwardFrom;

		// Token: 0x04006B59 RID: 27481
		public float forceScale;
	}
}
