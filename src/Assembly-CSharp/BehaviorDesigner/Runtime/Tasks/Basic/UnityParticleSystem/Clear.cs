using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x020010CD RID: 4301
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Clear the Particle System.")]
	public class Clear : Action
	{
		// Token: 0x06007403 RID: 29699 RVA: 0x002B1618 File Offset: 0x002AF818
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007404 RID: 29700 RVA: 0x002B1658 File Offset: 0x002AF858
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

		// Token: 0x06007405 RID: 29701 RVA: 0x002B1680 File Offset: 0x002AF880
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04005FC5 RID: 24517
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005FC6 RID: 24518
		private ParticleSystem particleSystem;

		// Token: 0x04005FC7 RID: 24519
		private GameObject prevGameObject;
	}
}
