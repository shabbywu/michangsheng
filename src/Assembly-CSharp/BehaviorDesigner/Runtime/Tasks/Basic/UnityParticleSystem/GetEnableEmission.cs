using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x020010D0 RID: 4304
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Stores if the Particle System is emitting particles.")]
	public class GetEnableEmission : Action
	{
		// Token: 0x0600740F RID: 29711 RVA: 0x002B17AC File Offset: 0x002AF9AC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007410 RID: 29712 RVA: 0x002B17EC File Offset: 0x002AF9EC
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return 1;
			}
			this.storeResult.Value = this.particleSystem.emission.enabled;
			return 2;
		}

		// Token: 0x06007411 RID: 29713 RVA: 0x002B1832 File Offset: 0x002AFA32
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeResult = false;
		}

		// Token: 0x04005FD0 RID: 24528
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005FD1 RID: 24529
		[Tooltip("Is the Particle System emitting particles?")]
		[RequiredField]
		public SharedBool storeResult;

		// Token: 0x04005FD2 RID: 24530
		private ParticleSystem particleSystem;

		// Token: 0x04005FD3 RID: 24531
		private GameObject prevGameObject;
	}
}
