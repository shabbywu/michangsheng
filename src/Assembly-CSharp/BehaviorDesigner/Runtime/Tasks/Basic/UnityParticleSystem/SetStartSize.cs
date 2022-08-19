using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x020010E5 RID: 4325
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Sets the start size of the Particle System.")]
	public class SetStartSize : Action
	{
		// Token: 0x06007463 RID: 29795 RVA: 0x002B23A4 File Offset: 0x002B05A4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007464 RID: 29796 RVA: 0x002B23E4 File Offset: 0x002B05E4
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return 1;
			}
			this.particleSystem.main.startSize = this.startSize.Value;
			return 2;
		}

		// Token: 0x06007465 RID: 29797 RVA: 0x002B242F File Offset: 0x002B062F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.startSize = 0f;
		}

		// Token: 0x0400601E RID: 24606
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400601F RID: 24607
		[Tooltip("The start size of the ParticleSystem")]
		public SharedFloat startSize;

		// Token: 0x04006020 RID: 24608
		private ParticleSystem particleSystem;

		// Token: 0x04006021 RID: 24609
		private GameObject prevGameObject;
	}
}
