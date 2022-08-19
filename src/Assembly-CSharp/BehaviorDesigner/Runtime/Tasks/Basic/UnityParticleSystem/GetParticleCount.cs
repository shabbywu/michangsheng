using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x020010D3 RID: 4307
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Stores the particle count of the Particle System.")]
	public class GetParticleCount : Action
	{
		// Token: 0x0600741B RID: 29723 RVA: 0x002B1984 File Offset: 0x002AFB84
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600741C RID: 29724 RVA: 0x002B19C4 File Offset: 0x002AFBC4
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

		// Token: 0x0600741D RID: 29725 RVA: 0x002B19F8 File Offset: 0x002AFBF8
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeResult = 0f;
		}

		// Token: 0x04005FDC RID: 24540
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005FDD RID: 24541
		[Tooltip("The particle count of the ParticleSystem")]
		[RequiredField]
		public SharedFloat storeResult;

		// Token: 0x04005FDE RID: 24542
		private ParticleSystem particleSystem;

		// Token: 0x04005FDF RID: 24543
		private GameObject prevGameObject;
	}
}
