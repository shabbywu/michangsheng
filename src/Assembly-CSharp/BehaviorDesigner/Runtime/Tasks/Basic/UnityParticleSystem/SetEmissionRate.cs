using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x02001596 RID: 5526
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Sets the emission rate of the Particle System.")]
	public class SetEmissionRate : Action
	{
		// Token: 0x06008239 RID: 33337 RVA: 0x002CD384 File Offset: 0x002CB584
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600823A RID: 33338 RVA: 0x000593C7 File Offset: 0x000575C7
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

		// Token: 0x0600823B RID: 33339 RVA: 0x000593EE File Offset: 0x000575EE
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.emissionRate = 0f;
		}

		// Token: 0x04006EFA RID: 28410
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006EFB RID: 28411
		[Tooltip("The emission rate of the ParticleSystem")]
		public SharedFloat emissionRate;

		// Token: 0x04006EFC RID: 28412
		private ParticleSystem particleSystem;

		// Token: 0x04006EFD RID: 28413
		private GameObject prevGameObject;
	}
}
