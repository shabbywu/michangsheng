using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x02001593 RID: 5523
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Is the Particle System stopped?")]
	public class IsStopped : Conditional
	{
		// Token: 0x0600822D RID: 33325 RVA: 0x002CD2C4 File Offset: 0x002CB4C4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600822E RID: 33326 RVA: 0x00059330 File Offset: 0x00057530
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return 1;
			}
			if (!this.particleSystem.isStopped)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x0600822F RID: 33327 RVA: 0x0005935C File Offset: 0x0005755C
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04006EF1 RID: 28401
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006EF2 RID: 28402
		private ParticleSystem particleSystem;

		// Token: 0x04006EF3 RID: 28403
		private GameObject prevGameObject;
	}
}
