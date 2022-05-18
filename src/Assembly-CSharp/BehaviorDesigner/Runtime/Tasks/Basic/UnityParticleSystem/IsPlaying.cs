using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x02001592 RID: 5522
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Is the Particle System playing?")]
	public class IsPlaying : Conditional
	{
		// Token: 0x06008229 RID: 33321 RVA: 0x002CD284 File Offset: 0x002CB484
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600822A RID: 33322 RVA: 0x000592FB File Offset: 0x000574FB
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return 1;
			}
			if (!this.particleSystem.isPlaying)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x0600822B RID: 33323 RVA: 0x00059327 File Offset: 0x00057527
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04006EEE RID: 28398
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006EEF RID: 28399
		private ParticleSystem particleSystem;

		// Token: 0x04006EF0 RID: 28400
		private GameObject prevGameObject;
	}
}
