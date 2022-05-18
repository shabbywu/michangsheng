using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x02001590 RID: 5520
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Is the Particle System alive?")]
	public class IsAlive : Conditional
	{
		// Token: 0x06008221 RID: 33313 RVA: 0x002CD204 File Offset: 0x002CB404
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008222 RID: 33314 RVA: 0x00059291 File Offset: 0x00057491
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return 1;
			}
			if (!this.particleSystem.IsAlive())
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06008223 RID: 33315 RVA: 0x000592BD File Offset: 0x000574BD
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04006EE8 RID: 28392
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006EE9 RID: 28393
		private ParticleSystem particleSystem;

		// Token: 0x04006EEA RID: 28394
		private GameObject prevGameObject;
	}
}
