using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x02001588 RID: 5512
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Stores the duration of the Particle System.")]
	public class GetDuration : Action
	{
		// Token: 0x06008201 RID: 33281 RVA: 0x002CCE9C File Offset: 0x002CB09C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008202 RID: 33282 RVA: 0x002CCEDC File Offset: 0x002CB0DC
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return 1;
			}
			this.storeResult.Value = this.particleSystem.main.duration;
			return 2;
		}

		// Token: 0x06008203 RID: 33283 RVA: 0x00059143 File Offset: 0x00057343
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeResult = 0f;
		}

		// Token: 0x04006EC8 RID: 28360
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006EC9 RID: 28361
		[Tooltip("The duration of the ParticleSystem")]
		[RequiredField]
		public SharedFloat storeResult;

		// Token: 0x04006ECA RID: 28362
		private ParticleSystem particleSystem;

		// Token: 0x04006ECB RID: 28363
		private GameObject prevGameObject;
	}
}
