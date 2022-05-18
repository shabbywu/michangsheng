using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x02001587 RID: 5511
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Clear the Particle System.")]
	public class Clear : Action
	{
		// Token: 0x060081FD RID: 33277 RVA: 0x002CCE5C File Offset: 0x002CB05C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060081FE RID: 33278 RVA: 0x00059112 File Offset: 0x00057312
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return 1;
			}
			this.particleSystem.Clear();
			return 2;
		}

		// Token: 0x060081FF RID: 33279 RVA: 0x0005913A File Offset: 0x0005733A
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04006EC5 RID: 28357
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006EC6 RID: 28358
		private ParticleSystem particleSystem;

		// Token: 0x04006EC7 RID: 28359
		private GameObject prevGameObject;
	}
}
