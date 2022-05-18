using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x0200159A RID: 5530
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Sets the playback speed of the Particle System.")]
	public class SetPlaybackSpeed : Action
	{
		// Token: 0x06008249 RID: 33353 RVA: 0x002CD55C File Offset: 0x002CB75C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600824A RID: 33354 RVA: 0x002CD59C File Offset: 0x002CB79C
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

		// Token: 0x0600824B RID: 33355 RVA: 0x00059446 File Offset: 0x00057646
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.playbackSpeed = 1f;
		}

		// Token: 0x04006F0A RID: 28426
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006F0B RID: 28427
		[Tooltip("The playback speed of the ParticleSystem")]
		public SharedFloat playbackSpeed = 1f;

		// Token: 0x04006F0C RID: 28428
		private ParticleSystem particleSystem;

		// Token: 0x04006F0D RID: 28429
		private GameObject prevGameObject;
	}
}
