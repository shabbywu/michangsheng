using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x02001595 RID: 5525
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Play the Particle System.")]
	public class Play : Action
	{
		// Token: 0x06008235 RID: 33333 RVA: 0x002CD344 File Offset: 0x002CB544
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008236 RID: 33334 RVA: 0x00059396 File Offset: 0x00057596
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return 1;
			}
			this.particleSystem.Play();
			return 2;
		}

		// Token: 0x06008237 RID: 33335 RVA: 0x000593BE File Offset: 0x000575BE
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04006EF7 RID: 28407
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006EF8 RID: 28408
		private ParticleSystem particleSystem;

		// Token: 0x04006EF9 RID: 28409
		private GameObject prevGameObject;
	}
}
