using System;
using UnityEngine;

// Token: 0x020000CD RID: 205
public class MovementScript : MonoBehaviour
{
	// Token: 0x06000AEF RID: 2799 RVA: 0x00041FE9 File Offset: 0x000401E9
	private void Awake()
	{
		this.cc = base.GetComponent<CharacterController>();
	}

	// Token: 0x06000AF0 RID: 2800 RVA: 0x00041FF8 File Offset: 0x000401F8
	private void FixedUpdate()
	{
		Vector3 zero = Vector3.zero;
		zero.x = Input.GetAxis("Horizontal") * this.speed;
		zero.z = Input.GetAxis("Vertical") * this.speed;
		this.cc.SimpleMove(zero);
	}

	// Token: 0x040006FD RID: 1789
	public float speed = 10f;

	// Token: 0x040006FE RID: 1790
	private CharacterController cc;
}
