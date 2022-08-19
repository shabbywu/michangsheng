using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x020010E8 RID: 4328
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Simulate the Particle System.")]
	public class Simulate : Action
	{
		// Token: 0x0600746F RID: 29807 RVA: 0x002B2578 File Offset: 0x002B0778
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007470 RID: 29808 RVA: 0x002B25B8 File Offset: 0x002B07B8
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return 1;
			}
			this.particleSystem.Simulate(this.time.Value);
			return 2;
		}

		// Token: 0x06007471 RID: 29809 RVA: 0x002B25EB File Offset: 0x002B07EB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.time = 0f;
		}

		// Token: 0x0400602A RID: 24618
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400602B RID: 24619
		[Tooltip("Time to fastfoward the Particle System to")]
		public SharedFloat time;

		// Token: 0x0400602C RID: 24620
		private ParticleSystem particleSystem;

		// Token: 0x0400602D RID: 24621
		private GameObject prevGameObject;
	}
}
