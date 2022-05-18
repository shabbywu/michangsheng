using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x020015A2 RID: 5538
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Simulate the Particle System.")]
	public class Simulate : Action
	{
		// Token: 0x06008269 RID: 33385 RVA: 0x002CD96C File Offset: 0x002CBB6C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600826A RID: 33386 RVA: 0x00059559 File Offset: 0x00057759
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

		// Token: 0x0600826B RID: 33387 RVA: 0x0005958C File Offset: 0x0005778C
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.time = 0f;
		}

		// Token: 0x04006F2A RID: 28458
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006F2B RID: 28459
		[Tooltip("Time to fastfoward the Particle System to")]
		public SharedFloat time;

		// Token: 0x04006F2C RID: 28460
		private ParticleSystem particleSystem;

		// Token: 0x04006F2D RID: 28461
		private GameObject prevGameObject;
	}
}
