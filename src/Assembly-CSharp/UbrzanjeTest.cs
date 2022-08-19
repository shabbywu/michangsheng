using System;
using UnityEngine;

// Token: 0x02000506 RID: 1286
public class UbrzanjeTest : MonoBehaviour
{
	// Token: 0x06002968 RID: 10600 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x06002969 RID: 10601 RVA: 0x0013CEF6 File Offset: 0x0013B0F6
	private void FixedUpdate()
	{
		base.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, this.force));
		this.force += 5f;
	}

	// Token: 0x040025D3 RID: 9683
	private float force = 100f;
}
