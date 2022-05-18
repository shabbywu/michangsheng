using System;
using UnityEngine;

// Token: 0x02000664 RID: 1636
public class BabunAnimationEvents : MonoBehaviour
{
	// Token: 0x060028D4 RID: 10452 RVA: 0x0001FD2B File Offset: 0x0001DF2B
	private void Awake()
	{
		this.babun = base.transform.GetChild(0);
		this.anim = this.babun.GetComponent<Animator>();
	}

	// Token: 0x060028D5 RID: 10453 RVA: 0x0001FD50 File Offset: 0x0001DF50
	private void startPatrolRight()
	{
		this.anim.SetBool("changeSide", true);
	}

	// Token: 0x060028D6 RID: 10454 RVA: 0x0001FD63 File Offset: 0x0001DF63
	private void startPatrolLeft()
	{
		this.anim.SetBool("changeSide", false);
	}

	// Token: 0x060028D7 RID: 10455 RVA: 0x0001FD76 File Offset: 0x0001DF76
	private void landBaboon()
	{
		this.anim.SetBool("Land", true);
	}

	// Token: 0x060028D8 RID: 10456 RVA: 0x0001FD89 File Offset: 0x0001DF89
	private void startJumpBaboon()
	{
		this.anim.Play("Jump");
		this.anim.SetBool("Land", false);
	}

	// Token: 0x0400227C RID: 8828
	private Animator anim;

	// Token: 0x0400227D RID: 8829
	private Transform babun;
}
