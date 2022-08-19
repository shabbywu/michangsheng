using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x020010E7 RID: 4327
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Sets the time of the Particle System.")]
	public class SetTime : Action
	{
		// Token: 0x0600746B RID: 29803 RVA: 0x002B24EC File Offset: 0x002B06EC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600746C RID: 29804 RVA: 0x002B252C File Offset: 0x002B072C
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return 1;
			}
			this.particleSystem.time = this.time.Value;
			return 2;
		}

		// Token: 0x0600746D RID: 29805 RVA: 0x002B255F File Offset: 0x002B075F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.time = 0f;
		}

		// Token: 0x04006026 RID: 24614
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006027 RID: 24615
		[Tooltip("The time of the ParticleSystem")]
		public SharedFloat time;

		// Token: 0x04006028 RID: 24616
		private ParticleSystem particleSystem;

		// Token: 0x04006029 RID: 24617
		private GameObject prevGameObject;
	}
}
