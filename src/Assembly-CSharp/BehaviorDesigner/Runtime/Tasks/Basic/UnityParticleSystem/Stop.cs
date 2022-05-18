using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x020015A3 RID: 5539
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Stop the Particle System.")]
	public class Stop : Action
	{
		// Token: 0x0600826D RID: 33389 RVA: 0x002CD9AC File Offset: 0x002CBBAC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600826E RID: 33390 RVA: 0x000595A5 File Offset: 0x000577A5
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return 1;
			}
			this.particleSystem.Stop();
			return 2;
		}

		// Token: 0x0600826F RID: 33391 RVA: 0x000595CD File Offset: 0x000577CD
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04006F2E RID: 28462
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006F2F RID: 28463
		private ParticleSystem particleSystem;

		// Token: 0x04006F30 RID: 28464
		private GameObject prevGameObject;
	}
}
