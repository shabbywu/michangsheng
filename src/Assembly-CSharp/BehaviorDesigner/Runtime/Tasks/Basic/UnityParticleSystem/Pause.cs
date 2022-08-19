using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x020010DA RID: 4314
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Pause the Particle System.")]
	public class Pause : Action
	{
		// Token: 0x06007437 RID: 29751 RVA: 0x002B1D20 File Offset: 0x002AFF20
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007438 RID: 29752 RVA: 0x002B1D60 File Offset: 0x002AFF60
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

		// Token: 0x06007439 RID: 29753 RVA: 0x002B1D88 File Offset: 0x002AFF88
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04005FF4 RID: 24564
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005FF5 RID: 24565
		private ParticleSystem particleSystem;

		// Token: 0x04005FF6 RID: 24566
		private GameObject prevGameObject;
	}
}
