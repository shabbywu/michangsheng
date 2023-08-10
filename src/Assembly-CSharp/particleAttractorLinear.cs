using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class particleAttractorLinear : MonoBehaviour
{
	private ParticleSystem ps;

	private Particle[] m_Particles;

	public Transform target;

	public float speed = 5f;

	private int numParticlesAlive;

	private void Start()
	{
		ps = ((Component)this).GetComponent<ParticleSystem>();
		if (!Object.op_Implicit((Object)(object)((Component)this).GetComponent<Transform>()))
		{
			((Component)this).GetComponent<Transform>();
		}
	}

	private void Update()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		if (ps.isPlaying)
		{
			MainModule main = ps.main;
			m_Particles = (Particle[])(object)new Particle[((MainModule)(ref main)).maxParticles];
			numParticlesAlive = ps.GetParticles(m_Particles);
			float num = speed * Time.deltaTime;
			for (int i = 0; i < numParticlesAlive; i++)
			{
				((Particle)(ref m_Particles[i])).position = Vector3.LerpUnclamped(((Particle)(ref m_Particles[i])).position, target.position, num);
			}
			ps.SetParticles(m_Particles, numParticlesAlive);
		}
	}
}
