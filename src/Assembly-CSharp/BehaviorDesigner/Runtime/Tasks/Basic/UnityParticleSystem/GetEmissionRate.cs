using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x020010CF RID: 4303
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Stores the emission rate of the Particle System.")]
	public class GetEmissionRate : Action
	{
		// Token: 0x0600740B RID: 29707 RVA: 0x002B172C File Offset: 0x002AF92C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600740C RID: 29708 RVA: 0x002B176C File Offset: 0x002AF96C
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return 1;
			}
			Debug.Log("Warning: GetEmissionRate is not used in Unity 5.3 or later.");
			return 2;
		}

		// Token: 0x0600740D RID: 29709 RVA: 0x002B1793 File Offset: 0x002AF993
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeResult = 0f;
		}

		// Token: 0x04005FCC RID: 24524
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005FCD RID: 24525
		[Tooltip("The emission rate of the ParticleSystem")]
		[RequiredField]
		public SharedFloat storeResult;

		// Token: 0x04005FCE RID: 24526
		private ParticleSystem particleSystem;

		// Token: 0x04005FCF RID: 24527
		private GameObject prevGameObject;
	}
}
