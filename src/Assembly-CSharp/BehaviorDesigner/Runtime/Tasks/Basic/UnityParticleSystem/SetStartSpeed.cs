using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x020015A0 RID: 5536
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Sets the start speed of the Particle System.")]
	public class SetStartSpeed : Action
	{
		// Token: 0x06008261 RID: 33377 RVA: 0x002CD8A0 File Offset: 0x002CBAA0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008262 RID: 33378 RVA: 0x002CD8E0 File Offset: 0x002CBAE0
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return 1;
			}
			this.particleSystem.main.startSpeed = this.startSpeed.Value;
			return 2;
		}

		// Token: 0x06008263 RID: 33379 RVA: 0x000594F4 File Offset: 0x000576F4
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.startSpeed = 0f;
		}

		// Token: 0x04006F22 RID: 28450
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006F23 RID: 28451
		[Tooltip("The start speed of the ParticleSystem")]
		public SharedFloat startSpeed;

		// Token: 0x04006F24 RID: 28452
		private ParticleSystem particleSystem;

		// Token: 0x04006F25 RID: 28453
		private GameObject prevGameObject;
	}
}
