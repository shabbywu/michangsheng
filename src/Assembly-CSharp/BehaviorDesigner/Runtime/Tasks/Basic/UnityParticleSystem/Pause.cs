using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x02001594 RID: 5524
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Pause the Particle System.")]
	public class Pause : Action
	{
		// Token: 0x06008231 RID: 33329 RVA: 0x002CD304 File Offset: 0x002CB504
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008232 RID: 33330 RVA: 0x00059365 File Offset: 0x00057565
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return 1;
			}
			this.particleSystem.Pause();
			return 2;
		}

		// Token: 0x06008233 RID: 33331 RVA: 0x0005938D File Offset: 0x0005758D
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04006EF4 RID: 28404
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006EF5 RID: 28405
		private ParticleSystem particleSystem;

		// Token: 0x04006EF6 RID: 28406
		private GameObject prevGameObject;
	}
}
