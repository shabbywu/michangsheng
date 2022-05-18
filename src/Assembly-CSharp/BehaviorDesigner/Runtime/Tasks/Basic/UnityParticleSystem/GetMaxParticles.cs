using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x0200158C RID: 5516
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Stores the max particles of the Particle System.")]
	public class GetMaxParticles : Action
	{
		// Token: 0x06008211 RID: 33297 RVA: 0x002CD074 File Offset: 0x002CB274
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008212 RID: 33298 RVA: 0x002CD0B4 File Offset: 0x002CB2B4
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return 1;
			}
			this.storeResult.Value = (float)this.particleSystem.main.maxParticles;
			return 2;
		}

		// Token: 0x06008213 RID: 33299 RVA: 0x000591C6 File Offset: 0x000573C6
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeResult = 0f;
		}

		// Token: 0x04006ED8 RID: 28376
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006ED9 RID: 28377
		[Tooltip("The max particles of the ParticleSystem")]
		[RequiredField]
		public SharedFloat storeResult;

		// Token: 0x04006EDA RID: 28378
		private ParticleSystem particleSystem;

		// Token: 0x04006EDB RID: 28379
		private GameObject prevGameObject;
	}
}
