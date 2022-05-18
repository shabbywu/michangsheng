using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x02001599 RID: 5529
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Sets the max particles of the Particle System.")]
	public class SetMaxParticles : Action
	{
		// Token: 0x06008245 RID: 33349 RVA: 0x002CD4D4 File Offset: 0x002CB6D4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008246 RID: 33350 RVA: 0x002CD514 File Offset: 0x002CB714
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return 1;
			}
			this.particleSystem.main.maxParticles = this.maxParticles.Value;
			return 2;
		}

		// Token: 0x06008247 RID: 33351 RVA: 0x00059431 File Offset: 0x00057631
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.maxParticles = 0;
		}

		// Token: 0x04006F06 RID: 28422
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006F07 RID: 28423
		[Tooltip("The max particles of the ParticleSystem")]
		public SharedInt maxParticles;

		// Token: 0x04006F08 RID: 28424
		private ParticleSystem particleSystem;

		// Token: 0x04006F09 RID: 28425
		private GameObject prevGameObject;
	}
}
