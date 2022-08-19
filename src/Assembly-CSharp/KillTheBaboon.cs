using System;
using UnityEngine;

// Token: 0x020004B4 RID: 1204
public class KillTheBaboon : MonoBehaviour
{
	// Token: 0x06002619 RID: 9753 RVA: 0x00107E26 File Offset: 0x00106026
	private void Awake()
	{
		this.babun = base.transform.parent.Find("_MajmunceNadrlja");
		this.boxColliders = base.GetComponents<BoxCollider2D>();
		this.babunScript = this.babun.GetComponent<BabunDogadjaji_new>();
	}

	// Token: 0x0600261A RID: 9754 RVA: 0x00107E60 File Offset: 0x00106060
	private void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Monkey")
		{
			this.turnOffColliders();
			this.babunScript.killBaboonStuff();
		}
	}

	// Token: 0x0600261B RID: 9755 RVA: 0x00107E8A File Offset: 0x0010608A
	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Monkey")
		{
			this.turnOffColliders();
			this.babunScript.killBaboonStuff();
		}
	}

	// Token: 0x0600261C RID: 9756 RVA: 0x00107EB4 File Offset: 0x001060B4
	public void turnOffColliders()
	{
		this.boxColliders[0].enabled = false;
		this.collidersTurnedOff = true;
	}

	// Token: 0x0600261D RID: 9757 RVA: 0x00107ECB File Offset: 0x001060CB
	public void turnOnColliders()
	{
		if (this.collidersTurnedOff)
		{
			this.boxColliders[0].enabled = true;
			this.collidersTurnedOff = false;
		}
	}

	// Token: 0x0600261E RID: 9758 RVA: 0x00107EEA File Offset: 0x001060EA
	public void DestoyEnemy()
	{
		this.turnOffColliders();
		this.babunScript.killBaboonStuff();
	}

	// Token: 0x04001ED2 RID: 7890
	private Transform babun;

	// Token: 0x04001ED3 RID: 7891
	private BoxCollider2D[] boxColliders;

	// Token: 0x04001ED4 RID: 7892
	private BabunDogadjaji_new babunScript;

	// Token: 0x04001ED5 RID: 7893
	private bool collidersTurnedOff;
}
