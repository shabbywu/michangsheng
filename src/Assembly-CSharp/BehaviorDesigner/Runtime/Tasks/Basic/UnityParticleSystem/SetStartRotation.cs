using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x0200159E RID: 5534
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Sets the start rotation of the Particle System.")]
	public class SetStartRotation : Action
	{
		// Token: 0x06008259 RID: 33369 RVA: 0x002CD788 File Offset: 0x002CB988
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600825A RID: 33370 RVA: 0x002CD7C8 File Offset: 0x002CB9C8
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

		// Token: 0x0600825B RID: 33371 RVA: 0x000594C2 File Offset: 0x000576C2
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.startRotation = 0f;
		}

		// Token: 0x04006F1A RID: 28442
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006F1B RID: 28443
		[Tooltip("The start rotation of the ParticleSystem")]
		public SharedFloat startRotation;

		// Token: 0x04006F1C RID: 28444
		private ParticleSystem particleSystem;

		// Token: 0x04006F1D RID: 28445
		private GameObject prevGameObject;
	}
}
