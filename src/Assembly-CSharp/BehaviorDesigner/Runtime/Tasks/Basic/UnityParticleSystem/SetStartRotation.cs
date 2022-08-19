using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x020010E4 RID: 4324
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Sets the start rotation of the Particle System.")]
	public class SetStartRotation : Action
	{
		// Token: 0x0600745F RID: 29791 RVA: 0x002B2300 File Offset: 0x002B0500
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007460 RID: 29792 RVA: 0x002B2340 File Offset: 0x002B0540
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return 1;
			}
			this.particleSystem.main.startRotation = this.startRotation.Value;
			return 2;
		}

		// Token: 0x06007461 RID: 29793 RVA: 0x002B238B File Offset: 0x002B058B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.startRotation = 0f;
		}

		// Token: 0x0400601A RID: 24602
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400601B RID: 24603
		[Tooltip("The start rotation of the ParticleSystem")]
		public SharedFloat startRotation;

		// Token: 0x0400601C RID: 24604
		private ParticleSystem particleSystem;

		// Token: 0x0400601D RID: 24605
		private GameObject prevGameObject;
	}
}
