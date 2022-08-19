using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x020010CE RID: 4302
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Stores the duration of the Particle System.")]
	public class GetDuration : Action
	{
		// Token: 0x06007407 RID: 29703 RVA: 0x002B168C File Offset: 0x002AF88C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007408 RID: 29704 RVA: 0x002B16CC File Offset: 0x002AF8CC
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return 1;
			}
			this.storeResult.Value = this.particleSystem.main.duration;
			return 2;
		}

		// Token: 0x06007409 RID: 29705 RVA: 0x002B1712 File Offset: 0x002AF912
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeResult = 0f;
		}

		// Token: 0x04005FC8 RID: 24520
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005FC9 RID: 24521
		[Tooltip("The duration of the ParticleSystem")]
		[RequiredField]
		public SharedFloat storeResult;

		// Token: 0x04005FCA RID: 24522
		private ParticleSystem particleSystem;

		// Token: 0x04005FCB RID: 24523
		private GameObject prevGameObject;
	}
}
