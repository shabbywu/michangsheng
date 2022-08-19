using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x020010DF RID: 4319
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Sets the max particles of the Particle System.")]
	public class SetMaxParticles : Action
	{
		// Token: 0x0600744B RID: 29771 RVA: 0x002B1FC0 File Offset: 0x002B01C0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600744C RID: 29772 RVA: 0x002B2000 File Offset: 0x002B0200
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

		// Token: 0x0600744D RID: 29773 RVA: 0x002B2046 File Offset: 0x002B0246
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.maxParticles = 0;
		}

		// Token: 0x04006006 RID: 24582
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006007 RID: 24583
		[Tooltip("The max particles of the ParticleSystem")]
		public SharedInt maxParticles;

		// Token: 0x04006008 RID: 24584
		private ParticleSystem particleSystem;

		// Token: 0x04006009 RID: 24585
		private GameObject prevGameObject;
	}
}
