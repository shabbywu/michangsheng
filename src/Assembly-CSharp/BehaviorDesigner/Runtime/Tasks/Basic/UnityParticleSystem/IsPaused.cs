using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x02001591 RID: 5521
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Is the Particle System paused?")]
	public class IsPaused : Conditional
	{
		// Token: 0x06008225 RID: 33317 RVA: 0x002CD244 File Offset: 0x002CB444
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008226 RID: 33318 RVA: 0x000592C6 File Offset: 0x000574C6
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return 1;
			}
			if (!this.particleSystem.isPaused)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06008227 RID: 33319 RVA: 0x000592F2 File Offset: 0x000574F2
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04006EEB RID: 28395
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006EEC RID: 28396
		private ParticleSystem particleSystem;

		// Token: 0x04006EED RID: 28397
		private GameObject prevGameObject;
	}
}
