using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x020010DD RID: 4317
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Enables or disables the Particle System emission.")]
	public class SetEnableEmission : Action
	{
		// Token: 0x06007443 RID: 29763 RVA: 0x002B1E88 File Offset: 0x002B0088
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007444 RID: 29764 RVA: 0x002B1EC8 File Offset: 0x002B00C8
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return 1;
			}
			this.particleSystem.emission.enabled = this.enable.Value;
			return 2;
		}

		// Token: 0x06007445 RID: 29765 RVA: 0x002B1F0E File Offset: 0x002B010E
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.enable = false;
		}

		// Token: 0x04005FFE RID: 24574
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005FFF RID: 24575
		[Tooltip("Enable the ParticleSystem emissions?")]
		public SharedBool enable;

		// Token: 0x04006000 RID: 24576
		private ParticleSystem particleSystem;

		// Token: 0x04006001 RID: 24577
		private GameObject prevGameObject;
	}
}
