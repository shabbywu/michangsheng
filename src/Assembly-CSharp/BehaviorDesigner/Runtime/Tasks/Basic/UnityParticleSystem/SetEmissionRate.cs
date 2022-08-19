using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x020010DC RID: 4316
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Sets the emission rate of the Particle System.")]
	public class SetEmissionRate : Action
	{
		// Token: 0x0600743F RID: 29759 RVA: 0x002B1E08 File Offset: 0x002B0008
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007440 RID: 29760 RVA: 0x002B1E48 File Offset: 0x002B0048
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return 1;
			}
			Debug.Log("Warning: SetEmissionRate is not used in Unity 5.3 or later.");
			return 2;
		}

		// Token: 0x06007441 RID: 29761 RVA: 0x002B1E6F File Offset: 0x002B006F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.emissionRate = 0f;
		}

		// Token: 0x04005FFA RID: 24570
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005FFB RID: 24571
		[Tooltip("The emission rate of the ParticleSystem")]
		public SharedFloat emissionRate;

		// Token: 0x04005FFC RID: 24572
		private ParticleSystem particleSystem;

		// Token: 0x04005FFD RID: 24573
		private GameObject prevGameObject;
	}
}
