using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x020010D5 RID: 4309
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Stores the time of the Particle System.")]
	public class GetTime : Action
	{
		// Token: 0x06007423 RID: 29731 RVA: 0x002B1AB4 File Offset: 0x002AFCB4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007424 RID: 29732 RVA: 0x002B1AF4 File Offset: 0x002AFCF4
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return 1;
			}
			this.storeResult.Value = this.particleSystem.time;
			return 2;
		}

		// Token: 0x06007425 RID: 29733 RVA: 0x002B1B27 File Offset: 0x002AFD27
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeResult = 0f;
		}

		// Token: 0x04005FE4 RID: 24548
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005FE5 RID: 24549
		[Tooltip("The time of the ParticleSystem")]
		[RequiredField]
		public SharedFloat storeResult;

		// Token: 0x04005FE6 RID: 24550
		private ParticleSystem particleSystem;

		// Token: 0x04005FE7 RID: 24551
		private GameObject prevGameObject;
	}
}
