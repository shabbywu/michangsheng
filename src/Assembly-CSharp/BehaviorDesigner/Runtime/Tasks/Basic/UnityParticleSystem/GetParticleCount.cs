using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x0200158D RID: 5517
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Stores the particle count of the Particle System.")]
	public class GetParticleCount : Action
	{
		// Token: 0x06008215 RID: 33301 RVA: 0x002CD0FC File Offset: 0x002CB2FC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008216 RID: 33302 RVA: 0x000591DF File Offset: 0x000573DF
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return 1;
			}
			this.storeResult.Value = (float)this.particleSystem.particleCount;
			return 2;
		}

		// Token: 0x06008217 RID: 33303 RVA: 0x00059213 File Offset: 0x00057413
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeResult = 0f;
		}

		// Token: 0x04006EDC RID: 28380
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006EDD RID: 28381
		[Tooltip("The particle count of the ParticleSystem")]
		[RequiredField]
		public SharedFloat storeResult;

		// Token: 0x04006EDE RID: 28382
		private ParticleSystem particleSystem;

		// Token: 0x04006EDF RID: 28383
		private GameObject prevGameObject;
	}
}
