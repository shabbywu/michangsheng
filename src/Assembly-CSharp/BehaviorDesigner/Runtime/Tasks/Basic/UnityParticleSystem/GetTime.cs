using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x0200158F RID: 5519
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Stores the time of the Particle System.")]
	public class GetTime : Action
	{
		// Token: 0x0600821D RID: 33309 RVA: 0x002CD1C4 File Offset: 0x002CB3C4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600821E RID: 33310 RVA: 0x00059245 File Offset: 0x00057445
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

		// Token: 0x0600821F RID: 33311 RVA: 0x00059278 File Offset: 0x00057478
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeResult = 0f;
		}

		// Token: 0x04006EE4 RID: 28388
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006EE5 RID: 28389
		[Tooltip("The time of the ParticleSystem")]
		[RequiredField]
		public SharedFloat storeResult;

		// Token: 0x04006EE6 RID: 28390
		private ParticleSystem particleSystem;

		// Token: 0x04006EE7 RID: 28391
		private GameObject prevGameObject;
	}
}
