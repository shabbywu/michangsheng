using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x020010E2 RID: 4322
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Sets the start delay of the Particle System.")]
	public class SetStartDelay : Action
	{
		// Token: 0x06007457 RID: 29783 RVA: 0x002B21B8 File Offset: 0x002B03B8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007458 RID: 29784 RVA: 0x002B21F8 File Offset: 0x002B03F8
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return 1;
			}
			this.particleSystem.main.startDelay = this.startDelay.Value;
			return 2;
		}

		// Token: 0x06007459 RID: 29785 RVA: 0x002B2243 File Offset: 0x002B0443
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.startDelay = 0f;
		}

		// Token: 0x04006012 RID: 24594
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006013 RID: 24595
		[Tooltip("The start delay of the ParticleSystem")]
		public SharedFloat startDelay;

		// Token: 0x04006014 RID: 24596
		private ParticleSystem particleSystem;

		// Token: 0x04006015 RID: 24597
		private GameObject prevGameObject;
	}
}
