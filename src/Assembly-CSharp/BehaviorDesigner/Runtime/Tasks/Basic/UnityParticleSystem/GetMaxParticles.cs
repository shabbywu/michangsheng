using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x020010D2 RID: 4306
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Stores the max particles of the Particle System.")]
	public class GetMaxParticles : Action
	{
		// Token: 0x06007417 RID: 29719 RVA: 0x002B18E4 File Offset: 0x002AFAE4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007418 RID: 29720 RVA: 0x002B1924 File Offset: 0x002AFB24
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

		// Token: 0x06007419 RID: 29721 RVA: 0x002B196B File Offset: 0x002AFB6B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeResult = 0f;
		}

		// Token: 0x04005FD8 RID: 24536
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005FD9 RID: 24537
		[Tooltip("The max particles of the ParticleSystem")]
		[RequiredField]
		public SharedFloat storeResult;

		// Token: 0x04005FDA RID: 24538
		private ParticleSystem particleSystem;

		// Token: 0x04005FDB RID: 24539
		private GameObject prevGameObject;
	}
}
