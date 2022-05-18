using System;
using UnityEngine;

// Token: 0x02000797 RID: 1943
public class UbrzanjeTest : MonoBehaviour
{
	// Token: 0x0600316D RID: 12653 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x0600316E RID: 12654 RVA: 0x0002424B File Offset: 0x0002244B
	private void FixedUpdate()
	{
		base.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, this.force));
		this.force += 5f;
	}

	// Token: 0x04002DBB RID: 11707
	private float force = 100f;
}
