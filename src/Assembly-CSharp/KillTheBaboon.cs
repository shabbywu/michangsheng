using System;
using UnityEngine;

// Token: 0x020006A8 RID: 1704
public class KillTheBaboon : MonoBehaviour
{
	// Token: 0x06002A97 RID: 10903 RVA: 0x00021099 File Offset: 0x0001F299
	private void Awake()
	{
		this.babun = base.transform.parent.Find("_MajmunceNadrlja");
		this.boxColliders = base.GetComponents<BoxCollider2D>();
		this.babunScript = this.babun.GetComponent<BabunDogadjaji_new>();
	}

	// Token: 0x06002A98 RID: 10904 RVA: 0x000210D3 File Offset: 0x0001F2D3
	private void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Monkey")
		{
			this.turnOffColliders();
			this.babunScript.killBaboonStuff();
		}
	}

	// Token: 0x06002A99 RID: 10905 RVA: 0x000210FD File Offset: 0x0001F2FD
	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Monkey")
		{
			this.turnOffColliders();
			this.babunScript.killBaboonStuff();
		}
	}

	// Token: 0x06002A9A RID: 10906 RVA: 0x00021127 File Offset: 0x0001F327
	public void turnOffColliders()
	{
		this.boxColliders[0].enabled = false;
		this.collidersTurnedOff = true;
	}

	// Token: 0x06002A9B RID: 10907 RVA: 0x0002113E File Offset: 0x0001F33E
	public void turnOnColliders()
	{
		if (this.collidersTurnedOff)
		{
			this.boxColliders[0].enabled = true;
			this.collidersTurnedOff = false;
		}
	}

	// Token: 0x06002A9C RID: 10908 RVA: 0x0002115D File Offset: 0x0001F35D
	public void DestoyEnemy()
	{
		this.turnOffColliders();
		this.babunScript.killBaboonStuff();
	}

	// Token: 0x04002445 RID: 9285
	private Transform babun;

	// Token: 0x04002446 RID: 9286
	private BoxCollider2D[] boxColliders;

	// Token: 0x04002447 RID: 9287
	private BabunDogadjaji_new babunScript;

	// Token: 0x04002448 RID: 9288
	private bool collidersTurnedOff;
}
