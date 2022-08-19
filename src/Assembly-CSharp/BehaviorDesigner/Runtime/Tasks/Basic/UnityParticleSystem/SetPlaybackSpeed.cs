using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x020010E0 RID: 4320
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Sets the playback speed of the Particle System.")]
	public class SetPlaybackSpeed : Action
	{
		// Token: 0x0600744F RID: 29775 RVA: 0x002B205C File Offset: 0x002B025C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007450 RID: 29776 RVA: 0x002B209C File Offset: 0x002B029C
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return 1;
			}
			this.particleSystem.main.simulationSpeed = this.playbackSpeed.Value;
			return 2;
		}

		// Token: 0x06007451 RID: 29777 RVA: 0x002B20E2 File Offset: 0x002B02E2
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.playbackSpeed = 1f;
		}

		// Token: 0x0400600A RID: 24586
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400600B RID: 24587
		[Tooltip("The playback speed of the ParticleSystem")]
		public SharedFloat playbackSpeed = 1f;

		// Token: 0x0400600C RID: 24588
		private ParticleSystem particleSystem;

		// Token: 0x0400600D RID: 24589
		private GameObject prevGameObject;
	}
}
