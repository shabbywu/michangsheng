using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x0200158E RID: 5518
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Stores the playback speed of the Particle System.")]
	public class GetPlaybackSpeed : Action
	{
		// Token: 0x06008219 RID: 33305 RVA: 0x002CD13C File Offset: 0x002CB33C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600821A RID: 33306 RVA: 0x002CD17C File Offset: 0x002CB37C
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return 1;
			}
			ParticleSystem.MainModule main = this.particleSystem.main;
			this.storeResult.Value = main.simulationSpeed;
			return 2;
		}

		// Token: 0x0600821B RID: 33307 RVA: 0x0005922C File Offset: 0x0005742C
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeResult = 0f;
		}

		// Token: 0x04006EE0 RID: 28384
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006EE1 RID: 28385
		[Tooltip("The playback speed of the ParticleSystem")]
		[RequiredField]
		public SharedFloat storeResult;

		// Token: 0x04006EE2 RID: 28386
		private ParticleSystem particleSystem;

		// Token: 0x04006EE3 RID: 28387
		private GameObject prevGameObject;
	}
}
