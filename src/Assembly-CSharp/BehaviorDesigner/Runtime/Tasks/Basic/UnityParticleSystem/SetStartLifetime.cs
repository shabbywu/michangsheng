using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x020010E3 RID: 4323
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Sets the start lifetime of the Particle System.")]
	public class SetStartLifetime : Action
	{
		// Token: 0x0600745B RID: 29787 RVA: 0x002B225C File Offset: 0x002B045C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600745C RID: 29788 RVA: 0x002B229C File Offset: 0x002B049C
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return 1;
			}
			this.particleSystem.main.startLifetime = this.startLifetime.Value;
			return 2;
		}

		// Token: 0x0600745D RID: 29789 RVA: 0x002B22E7 File Offset: 0x002B04E7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.startLifetime = 0f;
		}

		// Token: 0x04006016 RID: 24598
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006017 RID: 24599
		[Tooltip("The start lifetime of the ParticleSystem")]
		public SharedFloat startLifetime;

		// Token: 0x04006018 RID: 24600
		private ParticleSystem particleSystem;

		// Token: 0x04006019 RID: 24601
		private GameObject prevGameObject;
	}
}
