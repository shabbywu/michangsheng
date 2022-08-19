using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x020010D1 RID: 4305
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Stores if the Particle System should loop.")]
	public class GetLoop : Action
	{
		// Token: 0x06007413 RID: 29715 RVA: 0x002B1848 File Offset: 0x002AFA48
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007414 RID: 29716 RVA: 0x002B1888 File Offset: 0x002AFA88
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return 1;
			}
			this.storeResult.Value = this.particleSystem.main.loop;
			return 2;
		}

		// Token: 0x06007415 RID: 29717 RVA: 0x002B18CE File Offset: 0x002AFACE
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeResult = false;
		}

		// Token: 0x04005FD4 RID: 24532
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005FD5 RID: 24533
		[Tooltip("Should the ParticleSystem loop?")]
		[RequiredField]
		public SharedBool storeResult;

		// Token: 0x04005FD6 RID: 24534
		private ParticleSystem particleSystem;

		// Token: 0x04005FD7 RID: 24535
		private GameObject prevGameObject;
	}
}
