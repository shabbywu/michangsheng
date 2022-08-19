using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x020010D8 RID: 4312
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Is the Particle System playing?")]
	public class IsPlaying : Conditional
	{
		// Token: 0x0600742F RID: 29743 RVA: 0x002B1C30 File Offset: 0x002AFE30
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007430 RID: 29744 RVA: 0x002B1C70 File Offset: 0x002AFE70
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return 1;
			}
			if (!this.particleSystem.isPlaying)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06007431 RID: 29745 RVA: 0x002B1C9C File Offset: 0x002AFE9C
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04005FEE RID: 24558
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005FEF RID: 24559
		private ParticleSystem particleSystem;

		// Token: 0x04005FF0 RID: 24560
		private GameObject prevGameObject;
	}
}
