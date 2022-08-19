using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EA8 RID: 3752
	[EventHandlerInfo("MonoBehaviour", "Particle", "The block will execute when the desired OnParticle message for the monobehaviour is received.")]
	[AddComponentMenu("")]
	public class Particle : TagFilteredEventHandler
	{
		// Token: 0x06006A2C RID: 27180 RVA: 0x00292B34 File Offset: 0x00290D34
		private void OnParticleCollision(GameObject other)
		{
			if ((this.FireOn & Particle.ParticleMessageFlags.OnParticleCollision) != (Particle.ParticleMessageFlags)0)
			{
				base.ProcessTagFilter(other.tag);
			}
		}

		// Token: 0x06006A2D RID: 27181 RVA: 0x00292B4C File Offset: 0x00290D4C
		private void OnParticleTrigger()
		{
			if ((this.FireOn & Particle.ParticleMessageFlags.OnParticleTrigger) != (Particle.ParticleMessageFlags)0)
			{
				this.ExecuteBlock();
			}
		}

		// Token: 0x040059DE RID: 23006
		[Tooltip("Which of the Rendering messages to trigger on.")]
		[SerializeField]
		[EnumFlag]
		protected Particle.ParticleMessageFlags FireOn = Particle.ParticleMessageFlags.OnParticleCollision;

		// Token: 0x020016F7 RID: 5879
		[Flags]
		public enum ParticleMessageFlags
		{
			// Token: 0x0400748B RID: 29835
			OnParticleCollision = 1,
			// Token: 0x0400748C RID: 29836
			OnParticleTrigger = 2
		}
	}
}
