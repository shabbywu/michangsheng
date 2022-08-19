using System;
using UnityEngine;

// Token: 0x020004D7 RID: 1239
public class RotationInfinity : MonoBehaviour
{
	// Token: 0x0600282B RID: 10283 RVA: 0x00130284 File Offset: 0x0012E484
	private void Update()
	{
		base.transform.GetChild(0).Rotate(0f, 500f * Time.deltaTime, 0f);
		if (Random.Range(1, 100) < 5 && this.landed)
		{
			this.jump = true;
			this.landed = false;
		}
	}

	// Token: 0x0600282C RID: 10284 RVA: 0x001302D8 File Offset: 0x0012E4D8
	private void FixedUpdate()
	{
		if (this.jump)
		{
			base.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 15000f));
			this.jump = false;
		}
	}

	// Token: 0x0600282D RID: 10285 RVA: 0x00130303 File Offset: 0x0012E503
	private void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Footer")
		{
			this.landed = true;
		}
	}

	// Token: 0x04002330 RID: 9008
	private bool jump;

	// Token: 0x04002331 RID: 9009
	private bool landed = true;
}
