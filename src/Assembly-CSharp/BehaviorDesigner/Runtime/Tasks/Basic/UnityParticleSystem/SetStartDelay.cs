using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x0200159C RID: 5532
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Sets the start delay of the Particle System.")]
	public class SetStartDelay : Action
	{
		// Token: 0x06008251 RID: 33361 RVA: 0x002CD670 File Offset: 0x002CB870
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008252 RID: 33362 RVA: 0x002CD6B0 File Offset: 0x002CB8B0
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return 1;
			}
			this.particleSystem.main.startDelay = this.startDelay.Value;
			return 2;
		}

		// Token: 0x06008253 RID: 33363 RVA: 0x00059490 File Offset: 0x00057690
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.startDelay = 0f;
		}

		// Token: 0x04006F12 RID: 28434
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006F13 RID: 28435
		[Tooltip("The start delay of the ParticleSystem")]
		public SharedFloat startDelay;

		// Token: 0x04006F14 RID: 28436
		private ParticleSystem particleSystem;

		// Token: 0x04006F15 RID: 28437
		private GameObject prevGameObject;
	}
}
