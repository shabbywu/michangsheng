using System;
using UnityEngine;

namespace Fungus;

[EventHandlerInfo("MonoBehaviour", "Particle", "The block will execute when the desired OnParticle message for the monobehaviour is received.")]
[AddComponentMenu("")]
public class Particle : TagFilteredEventHandler
{
	[Flags]
	public enum ParticleMessageFlags
	{
		OnParticleCollision = 1,
		OnParticleTrigger = 2
	}

	[Tooltip("Which of the Rendering messages to trigger on.")]
	[SerializeField]
	[EnumFlag]
	protected ParticleMessageFlags FireOn = ParticleMessageFlags.OnParticleCollision;

	private void OnParticleCollision(GameObject other)
	{
		if ((FireOn & ParticleMessageFlags.OnParticleCollision) != 0)
		{
			ProcessTagFilter(other.tag);
		}
	}

	private void OnParticleTrigger()
	{
		if ((FireOn & ParticleMessageFlags.OnParticleTrigger) != 0)
		{
			ExecuteBlock();
		}
	}
}
