using System;
using UnityEngine;

// Token: 0x02000490 RID: 1168
public class BabunAnimationEvents : MonoBehaviour
{
	// Token: 0x060024DC RID: 9436 RVA: 0x000FFBEA File Offset: 0x000FDDEA
	private void Awake()
	{
		this.babun = base.transform.GetChild(0);
		this.anim = this.babun.GetComponent<Animator>();
	}

	// Token: 0x060024DD RID: 9437 RVA: 0x000FFC0F File Offset: 0x000FDE0F
	private void startPatrolRight()
	{
		this.anim.SetBool("changeSide", true);
	}

	// Token: 0x060024DE RID: 9438 RVA: 0x000FFC22 File Offset: 0x000FDE22
	private void startPatrolLeft()
	{
		this.anim.SetBool("changeSide", false);
	}

	// Token: 0x060024DF RID: 9439 RVA: 0x000FFC35 File Offset: 0x000FDE35
	private void landBaboon()
	{
		this.anim.SetBool("Land", true);
	}

	// Token: 0x060024E0 RID: 9440 RVA: 0x000FFC48 File Offset: 0x000FDE48
	private void startJumpBaboon()
	{
		this.anim.Play("Jump");
		this.anim.SetBool("Land", false);
	}

	// Token: 0x04001D7B RID: 7547
	private Animator anim;

	// Token: 0x04001D7C RID: 7548
	private Transform babun;
}
