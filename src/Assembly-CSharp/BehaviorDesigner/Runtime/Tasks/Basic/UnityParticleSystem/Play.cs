using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x020010DB RID: 4315
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Play the Particle System.")]
	public class Play : Action
	{
		// Token: 0x0600743B RID: 29755 RVA: 0x002B1D94 File Offset: 0x002AFF94
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600743C RID: 29756 RVA: 0x002B1DD4 File Offset: 0x002AFFD4
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

		// Token: 0x0600743D RID: 29757 RVA: 0x002B1DFC File Offset: 0x002AFFFC
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04005FF7 RID: 24567
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005FF8 RID: 24568
		private ParticleSystem particleSystem;

		// Token: 0x04005FF9 RID: 24569
		private GameObject prevGameObject;
	}
}
