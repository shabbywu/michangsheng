using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x020015A1 RID: 5537
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Sets the time of the Particle System.")]
	public class SetTime : Action
	{
		// Token: 0x06008265 RID: 33381 RVA: 0x002CD92C File Offset: 0x002CBB2C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008266 RID: 33382 RVA: 0x0005950D File Offset: 0x0005770D
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return 1;
			}
			this.particleSystem.time = this.time.Value;
			return 2;
		}

		// Token: 0x06008267 RID: 33383 RVA: 0x00059540 File Offset: 0x00057740
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.time = 0f;
		}

		// Token: 0x04006F26 RID: 28454
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006F27 RID: 28455
		[Tooltip("The time of the ParticleSystem")]
		public SharedFloat time;

		// Token: 0x04006F28 RID: 28456
		private ParticleSystem particleSystem;

		// Token: 0x04006F29 RID: 28457
		private GameObject prevGameObject;
	}
}
