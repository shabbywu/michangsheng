using System;
using UnityEngine;

// Token: 0x0200023D RID: 573
[RequireComponent(typeof(ParticleSystem))]
public class particleAttractorLinear : MonoBehaviour
{
	// Token: 0x060011BD RID: 4541 RVA: 0x000111C0 File Offset: 0x0000F3C0
	private void Start()
	{
		this.ps = base.GetComponent<ParticleSystem>();
		if (!base.GetComponent<Transform>())
		{
			base.GetComponent<Transform>();
		}
	}

	// Token: 0x060011BE RID: 4542 RVA: 0x000AC698 File Offset: 0x000AA898
	private void Update()
	{
		if (this.ps.isPlaying)
		{
			this.m_Particles = new ParticleSystem.Particle[this.ps.main.maxParticles];
			this.numParticlesAlive = this.ps.GetParticles(this.m_Particles);
			float num = this.speed * Time.deltaTime;
			for (int i = 0; i < this.numParticlesAlive; i++)
			{
				this.m_Particles[i].position = Vector3.LerpUnclamped(this.m_Particles[i].position, this.target.position, num);
			}
			this.ps.SetParticles(this.m_Particles, this.numParticlesAlive);
		}
	}

	// Token: 0x04000E4A RID: 3658
	private ParticleSystem ps;

	// Token: 0x04000E4B RID: 3659
	private ParticleSystem.Particle[] m_Particles;

	// Token: 0x04000E4C RID: 3660
	public Transform target;

	// Token: 0x04000E4D RID: 3661
	public float speed = 5f;

	// Token: 0x04000E4E RID: 3662
	private int numParticlesAlive;
}
