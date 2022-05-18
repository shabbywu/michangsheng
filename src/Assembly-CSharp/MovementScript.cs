using System;
using UnityEngine;

// Token: 0x0200013E RID: 318
public class MovementScript : MonoBehaviour
{
	// Token: 0x06000BD2 RID: 3026 RVA: 0x0000DEEF File Offset: 0x0000C0EF
	private void Awake()
	{
		this.cc = base.GetComponent<CharacterController>();
	}

	// Token: 0x06000BD3 RID: 3027 RVA: 0x00093E74 File Offset: 0x00092074
	private void FixedUpdate()
	{
		Vector3 zero = Vector3.zero;
		zero.x = Input.GetAxis("Horizontal") * this.speed;
		zero.z = Input.GetAxis("Vertical") * this.speed;
		this.cc.SimpleMove(zero);
	}

	// Token: 0x040008A8 RID: 2216
	public float speed = 10f;

	// Token: 0x040008A9 RID: 2217
	private CharacterController cc;
}
