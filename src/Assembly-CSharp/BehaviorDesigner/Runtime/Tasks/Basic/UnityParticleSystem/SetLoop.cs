using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x020010DE RID: 4318
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Sets if the Particle System should loop.")]
	public class SetLoop : Action
	{
		// Token: 0x06007447 RID: 29767 RVA: 0x002B1F24 File Offset: 0x002B0124
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007448 RID: 29768 RVA: 0x002B1F64 File Offset: 0x002B0164
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

		// Token: 0x06007449 RID: 29769 RVA: 0x002B1FAA File Offset: 0x002B01AA
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.loop = false;
		}

		// Token: 0x04006002 RID: 24578
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006003 RID: 24579
		[Tooltip("Should the ParticleSystem loop?")]
		public SharedBool loop;

		// Token: 0x04006004 RID: 24580
		private ParticleSystem particleSystem;

		// Token: 0x04006005 RID: 24581
		private GameObject prevGameObject;
	}
}
