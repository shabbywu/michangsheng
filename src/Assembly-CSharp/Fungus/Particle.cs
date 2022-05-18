using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200132B RID: 4907
	[EventHandlerInfo("MonoBehaviour", "Particle", "The block will execute when the desired OnParticle message for the monobehaviour is received.")]
	[AddComponentMenu("")]
	public class Particle : TagFilteredEventHandler
	{
		// Token: 0x06007762 RID: 30562 RVA: 0x0005160C File Offset: 0x0004F80C
		private void OnParticleCollision(GameObject other)
		{
			if ((this.FireOn & Particle.ParticleMessageFlags.OnParticleCollision) != (Particle.ParticleMessageFlags)0)
			{
				base.ProcessTagFilter(other.tag);
			}
		}

		// Token: 0x06007763 RID: 30563 RVA: 0x00051624 File Offset: 0x0004F824
		private void OnParticleTrigger()
		{
			if ((this.FireOn & Particle.ParticleMessageFlags.OnParticleTrigger) != (Particle.ParticleMessageFlags)0)
			{
				this.ExecuteBlock();
			}
		}

		// Token: 0x0400680D RID: 26637
		[Tooltip("Which of the Rendering messages to trigger on.")]
		[SerializeField]
		[EnumFlag]
		protected Particle.ParticleMessageFlags FireOn = Particle.ParticleMessageFlags.OnParticleCollision;

		// Token: 0x0200132C RID: 4908
		[Flags]
		public enum ParticleMessageFlags
		{
			// Token: 0x0400680F RID: 26639
			OnParticleCollision = 1,
			// Token: 0x04006810 RID: 26640
			OnParticleTrigger = 2
		}
	}
}
