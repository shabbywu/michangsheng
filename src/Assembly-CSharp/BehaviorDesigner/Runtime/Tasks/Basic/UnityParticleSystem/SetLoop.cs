using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x02001598 RID: 5528
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Sets if the Particle System should loop.")]
	public class SetLoop : Action
	{
		// Token: 0x06008241 RID: 33345 RVA: 0x002CD44C File Offset: 0x002CB64C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008242 RID: 33346 RVA: 0x002CD48C File Offset: 0x002CB68C
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return 1;
			}
			this.particleSystem.main.loop = this.loop.Value;
			return 2;
		}

		// Token: 0x06008243 RID: 33347 RVA: 0x0005941C File Offset: 0x0005761C
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.loop = false;
		}

		// Token: 0x04006F02 RID: 28418
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006F03 RID: 28419
		[Tooltip("Should the ParticleSystem loop?")]
		public SharedBool loop;

		// Token: 0x04006F04 RID: 28420
		private ParticleSystem particleSystem;

		// Token: 0x04006F05 RID: 28421
		private GameObject prevGameObject;
	}
}
