using UnityEngine;

public class MonkeyParticles : MonoBehaviour
{
	public ParticleSystem doubleJump;

	public ParticleSystem doubleJumpEffect;

	public ParticleSystem blast;

	public ParticleSystem death;

	public ParticleSystem deathDrag;

	public ParticleSystem hitBlast;

	public ParticleSystem hitSmoke;

	public ParticleSystem grabDust;

	private void particleDoubleJump()
	{
		doubleJump.Emit(25);
	}

	private void particleDoubleJumpEffect()
	{
		doubleJumpEffect.Emit(1);
	}

	private void particleBlast()
	{
		blast.Emit(1);
	}

	private void particleDeath()
	{
		death.Emit(100);
	}

	private void particleDeathDrag()
	{
		deathDrag.Play();
	}

	private void particleHitBlast()
	{
		hitBlast.Emit(100);
	}

	private void particleHitSmoke()
	{
		hitSmoke.Play();
	}

	private void particleGrabDust()
	{
		grabDust.Play();
	}

	private void StartClimbing()
	{
		GameObject.FindGameObjectWithTag("Monkey").SendMessage("climb");
	}
}
