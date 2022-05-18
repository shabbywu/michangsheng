using System;
using UnityEngine;

// Token: 0x0200074A RID: 1866
public class RotationInfinity : MonoBehaviour
{
	// Token: 0x06002F82 RID: 12162 RVA: 0x0017CA78 File Offset: 0x0017AC78
	private void Update()
	{
		base.transform.GetChild(0).Rotate(0f, 500f * Time.deltaTime, 0f);
		if (Random.Range(1, 100) < 5 && this.landed)
		{
			this.jump = true;
			this.landed = false;
		}
	}

	// Token: 0x06002F83 RID: 12163 RVA: 0x0002336D File Offset: 0x0002156D
	private void FixedUpdate()
	{
		if (this.jump)
		{
			base.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 15000f));
			this.jump = false;
		}
	}

	// Token: 0x06002F84 RID: 12164 RVA: 0x00023398 File Offset: 0x00021598
	private void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Footer")
		{
			this.landed = true;
		}
	}

	// Token: 0x04002AB0 RID: 10928
	private bool jump;

	// Token: 0x04002AB1 RID: 10929
	private bool landed = true;
}
