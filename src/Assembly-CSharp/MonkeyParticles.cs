using System;
using UnityEngine;

// Token: 0x0200072D RID: 1837
public class MonkeyParticles : MonoBehaviour
{
	// Token: 0x06002E9D RID: 11933 RVA: 0x000229E9 File Offset: 0x00020BE9
	private void particleDoubleJump()
	{
		this.doubleJump.Emit(25);
	}

	// Token: 0x06002E9E RID: 11934 RVA: 0x000229F8 File Offset: 0x00020BF8
	private void particleDoubleJumpEffect()
	{
		this.doubleJumpEffect.Emit(1);
	}

	// Token: 0x06002E9F RID: 11935 RVA: 0x00022A06 File Offset: 0x00020C06
	private void particleBlast()
	{
		this.blast.Emit(1);
	}

	// Token: 0x06002EA0 RID: 11936 RVA: 0x00022A14 File Offset: 0x00020C14
	private void particleDeath()
	{
		this.death.Emit(100);
	}

	// Token: 0x06002EA1 RID: 11937 RVA: 0x00022A23 File Offset: 0x00020C23
	private void particleDeathDrag()
	{
		this.deathDrag.Play();
	}

	// Token: 0x06002EA2 RID: 11938 RVA: 0x00022A30 File Offset: 0x00020C30
	private void particleHitBlast()
	{
		this.hitBlast.Emit(100);
	}

	// Token: 0x06002EA3 RID: 11939 RVA: 0x00022A3F File Offset: 0x00020C3F
	private void particleHitSmoke()
	{
		this.hitSmoke.Play();
	}

	// Token: 0x06002EA4 RID: 11940 RVA: 0x00022A4C File Offset: 0x00020C4C
	private void particleGrabDust()
	{
		this.grabDust.Play();
	}

	// Token: 0x06002EA5 RID: 11941 RVA: 0x00022A59 File Offset: 0x00020C59
	private void StartClimbing()
	{
		GameObject.FindGameObjectWithTag("Monkey").SendMessage("climb");
	}

	// Token: 0x040029C1 RID: 10689
	public ParticleSystem doubleJump;

	// Token: 0x040029C2 RID: 10690
	public ParticleSystem doubleJumpEffect;

	// Token: 0x040029C3 RID: 10691
	public ParticleSystem blast;

	// Token: 0x040029C4 RID: 10692
	public ParticleSystem death;

	// Token: 0x040029C5 RID: 10693
	public ParticleSystem deathDrag;

	// Token: 0x040029C6 RID: 10694
	public ParticleSystem hitBlast;

	// Token: 0x040029C7 RID: 10695
	public ParticleSystem hitSmoke;

	// Token: 0x040029C8 RID: 10696
	public ParticleSystem grabDust;
}
