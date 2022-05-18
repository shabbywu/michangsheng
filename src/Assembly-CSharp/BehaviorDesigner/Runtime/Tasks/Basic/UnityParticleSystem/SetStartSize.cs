using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x0200159F RID: 5535
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Sets the start size of the Particle System.")]
	public class SetStartSize : Action
	{
		// Token: 0x0600825D RID: 33373 RVA: 0x002CD814 File Offset: 0x002CBA14
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600825E RID: 33374 RVA: 0x002CD854 File Offset: 0x002CBA54
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

		// Token: 0x0600825F RID: 33375 RVA: 0x000594DB File Offset: 0x000576DB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.startSize = 0f;
		}

		// Token: 0x04006F1E RID: 28446
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006F1F RID: 28447
		[Tooltip("The start size of the ParticleSystem")]
		public SharedFloat startSize;

		// Token: 0x04006F20 RID: 28448
		private ParticleSystem particleSystem;

		// Token: 0x04006F21 RID: 28449
		private GameObject prevGameObject;
	}
}
