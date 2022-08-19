using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x020010E9 RID: 4329
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Stop the Particle System.")]
	public class Stop : Action
	{
		// Token: 0x06007473 RID: 29811 RVA: 0x002B2604 File Offset: 0x002B0804
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007474 RID: 29812 RVA: 0x002B2644 File Offset: 0x002B0844
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return 1;
			}
			this.particleSystem.Stop();
			return 2;
		}

		// Token: 0x06007475 RID: 29813 RVA: 0x002B266C File Offset: 0x002B086C
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x0400602E RID: 24622
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400602F RID: 24623
		private ParticleSystem particleSystem;

		// Token: 0x04006030 RID: 24624
		private GameObject prevGameObject;
	}
}
