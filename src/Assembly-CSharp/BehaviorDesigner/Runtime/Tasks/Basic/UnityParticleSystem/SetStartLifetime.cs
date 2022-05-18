using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x0200159D RID: 5533
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Sets the start lifetime of the Particle System.")]
	public class SetStartLifetime : Action
	{
		// Token: 0x06008255 RID: 33365 RVA: 0x002CD6FC File Offset: 0x002CB8FC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008256 RID: 33366 RVA: 0x002CD73C File Offset: 0x002CB93C
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return 1;
			}
			this.particleSystem.main.startLifetime = this.startLifetime.Value;
			return 2;
		}

		// Token: 0x06008257 RID: 33367 RVA: 0x000594A9 File Offset: 0x000576A9
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.startLifetime = 0f;
		}

		// Token: 0x04006F16 RID: 28438
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006F17 RID: 28439
		[Tooltip("The start lifetime of the ParticleSystem")]
		public SharedFloat startLifetime;

		// Token: 0x04006F18 RID: 28440
		private ParticleSystem particleSystem;

		// Token: 0x04006F19 RID: 28441
		private GameObject prevGameObject;
	}
}
