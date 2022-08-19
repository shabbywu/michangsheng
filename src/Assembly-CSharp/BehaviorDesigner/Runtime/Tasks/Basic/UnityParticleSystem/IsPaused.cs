using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x020010D7 RID: 4311
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Is the Particle System paused?")]
	public class IsPaused : Conditional
	{
		// Token: 0x0600742B RID: 29739 RVA: 0x002B1BB8 File Offset: 0x002AFDB8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600742C RID: 29740 RVA: 0x002B1BF8 File Offset: 0x002AFDF8
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return 1;
			}
			if (!this.particleSystem.isPaused)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x0600742D RID: 29741 RVA: 0x002B1C24 File Offset: 0x002AFE24
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04005FEB RID: 24555
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005FEC RID: 24556
		private ParticleSystem particleSystem;

		// Token: 0x04005FED RID: 24557
		private GameObject prevGameObject;
	}
}
