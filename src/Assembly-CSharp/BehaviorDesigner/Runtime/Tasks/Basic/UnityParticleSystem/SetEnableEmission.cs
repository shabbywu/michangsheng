using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x02001597 RID: 5527
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Enables or disables the Particle System emission.")]
	public class SetEnableEmission : Action
	{
		// Token: 0x0600823D RID: 33341 RVA: 0x002CD3C4 File Offset: 0x002CB5C4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600823E RID: 33342 RVA: 0x002CD404 File Offset: 0x002CB604
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

		// Token: 0x0600823F RID: 33343 RVA: 0x00059407 File Offset: 0x00057607
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.enable = false;
		}

		// Token: 0x04006EFE RID: 28414
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006EFF RID: 28415
		[Tooltip("Enable the ParticleSystem emissions?")]
		public SharedBool enable;

		// Token: 0x04006F00 RID: 28416
		private ParticleSystem particleSystem;

		// Token: 0x04006F01 RID: 28417
		private GameObject prevGameObject;
	}
}
