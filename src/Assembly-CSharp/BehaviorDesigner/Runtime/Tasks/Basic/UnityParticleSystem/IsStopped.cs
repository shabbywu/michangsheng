using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x020010D9 RID: 4313
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Is the Particle System stopped?")]
	public class IsStopped : Conditional
	{
		// Token: 0x06007433 RID: 29747 RVA: 0x002B1CA8 File Offset: 0x002AFEA8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007434 RID: 29748 RVA: 0x002B1CE8 File Offset: 0x002AFEE8
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return 1;
			}
			if (!this.particleSystem.isStopped)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06007435 RID: 29749 RVA: 0x002B1D14 File Offset: 0x002AFF14
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04005FF1 RID: 24561
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005FF2 RID: 24562
		private ParticleSystem particleSystem;

		// Token: 0x04005FF3 RID: 24563
		private GameObject prevGameObject;
	}
}
