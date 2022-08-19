using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x020010E1 RID: 4321
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Sets the start color of the Particle System.")]
	public class SetStartColor : Action
	{
		// Token: 0x06007453 RID: 29779 RVA: 0x002B2114 File Offset: 0x002B0314
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007454 RID: 29780 RVA: 0x002B2154 File Offset: 0x002B0354
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return 1;
			}
			this.particleSystem.main.startColor = this.startColor.Value;
			return 2;
		}

		// Token: 0x06007455 RID: 29781 RVA: 0x002B219F File Offset: 0x002B039F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.startColor = Color.white;
		}

		// Token: 0x0400600E RID: 24590
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400600F RID: 24591
		[Tooltip("The start color of the ParticleSystem")]
		public SharedColor startColor;

		// Token: 0x04006010 RID: 24592
		private ParticleSystem particleSystem;

		// Token: 0x04006011 RID: 24593
		private GameObject prevGameObject;
	}
}
