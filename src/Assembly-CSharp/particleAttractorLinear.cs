using System;
using UnityEngine;

// Token: 0x02000162 RID: 354
[RequireComponent(typeof(ParticleSystem))]
public class particleAttractorLinear : MonoBehaviour
{
	// Token: 0x06000F63 RID: 3939 RVA: 0x0005C9F2 File Offset: 0x0005ABF2
	private void Start()
	{
		this.ps = base.GetComponent<ParticleSystem>();
		if (!base.GetComponent<Transform>())
		{
			base.GetComponent<Transform>();
		}
	}

	// Token: 0x06000F64 RID: 3940 RVA: 0x0005CA14 File Offset: 0x0005AC14
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

	// Token: 0x04000B7D RID: 2941
	private ParticleSystem ps;

	// Token: 0x04000B7E RID: 2942
	private ParticleSystem.Particle[] m_Particles;

	// Token: 0x04000B7F RID: 2943
	public Transform target;

	// Token: 0x04000B80 RID: 2944
	public float speed = 5f;

	// Token: 0x04000B81 RID: 2945
	private int numParticlesAlive;
}
