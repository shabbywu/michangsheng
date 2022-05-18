using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x02001589 RID: 5513
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Stores the emission rate of the Particle System.")]
	public class GetEmissionRate : Action
	{
		// Token: 0x06008205 RID: 33285 RVA: 0x002CCF24 File Offset: 0x002CB124
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008206 RID: 33286 RVA: 0x0005915C File Offset: 0x0005735C
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

		// Token: 0x06008207 RID: 33287 RVA: 0x00059183 File Offset: 0x00057383
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeResult = 0f;
		}

		// Token: 0x04006ECC RID: 28364
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006ECD RID: 28365
		[Tooltip("The emission rate of the ParticleSystem")]
		[RequiredField]
		public SharedFloat storeResult;

		// Token: 0x04006ECE RID: 28366
		private ParticleSystem particleSystem;

		// Token: 0x04006ECF RID: 28367
		private GameObject prevGameObject;
	}
}
