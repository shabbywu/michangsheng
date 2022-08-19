using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x020010E6 RID: 4326
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Sets the start speed of the Particle System.")]
	public class SetStartSpeed : Action
	{
		// Token: 0x06007467 RID: 29799 RVA: 0x002B2448 File Offset: 0x002B0648
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007468 RID: 29800 RVA: 0x002B2488 File Offset: 0x002B0688
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

		// Token: 0x06007469 RID: 29801 RVA: 0x002B24D3 File Offset: 0x002B06D3
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.startSpeed = 0f;
		}

		// Token: 0x04006022 RID: 24610
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006023 RID: 24611
		[Tooltip("The start speed of the ParticleSystem")]
		public SharedFloat startSpeed;

		// Token: 0x04006024 RID: 24612
		private ParticleSystem particleSystem;

		// Token: 0x04006025 RID: 24613
		private GameObject prevGameObject;
	}
}
