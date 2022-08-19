using System;
using UnityEngine;

// Token: 0x020004C9 RID: 1225
public class MonkeyParticles : MonoBehaviour
{
	// Token: 0x06002795 RID: 10133 RVA: 0x00128A63 File Offset: 0x00126C63
	private void particleDoubleJump()
	{
		this.doubleJump.Emit(25);
	}

	// Token: 0x06002796 RID: 10134 RVA: 0x00128A72 File Offset: 0x00126C72
	private void particleDoubleJumpEffect()
	{
		this.doubleJumpEffect.Emit(1);
	}

	// Token: 0x06002797 RID: 10135 RVA: 0x00128A80 File Offset: 0x00126C80
	private void particleBlast()
	{
		this.blast.Emit(1);
	}

	// Token: 0x06002798 RID: 10136 RVA: 0x00128A8E File Offset: 0x00126C8E
	private void particleDeath()
	{
		this.death.Emit(100);
	}

	// Token: 0x06002799 RID: 10137 RVA: 0x00128A9D File Offset: 0x00126C9D
	private void particleDeathDrag()
	{
		this.deathDrag.Play();
	}

	// Token: 0x0600279A RID: 10138 RVA: 0x00128AAA File Offset: 0x00126CAA
	private void particleHitBlast()
	{
		this.hitBlast.Emit(100);
	}

	// Token: 0x0600279B RID: 10139 RVA: 0x00128AB9 File Offset: 0x00126CB9
	private void particleHitSmoke()
	{
		this.hitSmoke.Play();
	}

	// Token: 0x0600279C RID: 10140 RVA: 0x00128AC6 File Offset: 0x00126CC6
	private void particleGrabDust()
	{
		this.grabDust.Play();
	}

	// Token: 0x0600279D RID: 10141 RVA: 0x00128AD3 File Offset: 0x00126CD3
	private void StartClimbing()
	{
		GameObject.FindGameObjectWithTag("Monkey").SendMessage("climb");
	}

	// Token: 0x0400227A RID: 8826
	public ParticleSystem doubleJump;

	// Token: 0x0400227B RID: 8827
	public ParticleSystem doubleJumpEffect;

	// Token: 0x0400227C RID: 8828
	public ParticleSystem blast;

	// Token: 0x0400227D RID: 8829
	public ParticleSystem death;

	// Token: 0x0400227E RID: 8830
	public ParticleSystem deathDrag;

	// Token: 0x0400227F RID: 8831
	public ParticleSystem hitBlast;

	// Token: 0x04002280 RID: 8832
	public ParticleSystem hitSmoke;

	// Token: 0x04002281 RID: 8833
	public ParticleSystem grabDust;
}
