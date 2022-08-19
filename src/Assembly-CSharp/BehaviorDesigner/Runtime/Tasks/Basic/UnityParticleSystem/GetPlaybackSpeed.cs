using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x020010D4 RID: 4308
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Stores the playback speed of the Particle System.")]
	public class GetPlaybackSpeed : Action
	{
		// Token: 0x0600741F RID: 29727 RVA: 0x002B1A14 File Offset: 0x002AFC14
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007420 RID: 29728 RVA: 0x002B1A54 File Offset: 0x002AFC54
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

		// Token: 0x06007421 RID: 29729 RVA: 0x002B1A9A File Offset: 0x002AFC9A
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeResult = 0f;
		}

		// Token: 0x04005FE0 RID: 24544
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005FE1 RID: 24545
		[Tooltip("The playback speed of the ParticleSystem")]
		[RequiredField]
		public SharedFloat storeResult;

		// Token: 0x04005FE2 RID: 24546
		private ParticleSystem particleSystem;

		// Token: 0x04005FE3 RID: 24547
		private GameObject prevGameObject;
	}
}
