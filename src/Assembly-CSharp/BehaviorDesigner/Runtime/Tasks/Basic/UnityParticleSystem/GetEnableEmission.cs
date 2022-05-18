using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x0200158A RID: 5514
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Stores if the Particle System is emitting particles.")]
	public class GetEnableEmission : Action
	{
		// Token: 0x06008209 RID: 33289 RVA: 0x002CCF64 File Offset: 0x002CB164
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600820A RID: 33290 RVA: 0x002CCFA4 File Offset: 0x002CB1A4
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

		// Token: 0x0600820B RID: 33291 RVA: 0x0005919C File Offset: 0x0005739C
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeResult = false;
		}

		// Token: 0x04006ED0 RID: 28368
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006ED1 RID: 28369
		[Tooltip("Is the Particle System emitting particles?")]
		[RequiredField]
		public SharedBool storeResult;

		// Token: 0x04006ED2 RID: 28370
		private ParticleSystem particleSystem;

		// Token: 0x04006ED3 RID: 28371
		private GameObject prevGameObject;
	}
}
