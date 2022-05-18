using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x0200158B RID: 5515
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Stores if the Particle System should loop.")]
	public class GetLoop : Action
	{
		// Token: 0x0600820D RID: 33293 RVA: 0x002CCFEC File Offset: 0x002CB1EC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600820E RID: 33294 RVA: 0x002CD02C File Offset: 0x002CB22C
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return 1;
			}
			this.storeResult.Value = this.particleSystem.main.loop;
			return 2;
		}

		// Token: 0x0600820F RID: 33295 RVA: 0x000591B1 File Offset: 0x000573B1
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeResult = false;
		}

		// Token: 0x04006ED4 RID: 28372
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006ED5 RID: 28373
		[Tooltip("Should the ParticleSystem loop?")]
		[RequiredField]
		public SharedBool storeResult;

		// Token: 0x04006ED6 RID: 28374
		private ParticleSystem particleSystem;

		// Token: 0x04006ED7 RID: 28375
		private GameObject prevGameObject;
	}
}
