using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x020010D6 RID: 4310
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Is the Particle System alive?")]
	public class IsAlive : Conditional
	{
		// Token: 0x06007427 RID: 29735 RVA: 0x002B1B40 File Offset: 0x002AFD40
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007428 RID: 29736 RVA: 0x002B1B80 File Offset: 0x002AFD80
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return 1;
			}
			if (!this.particleSystem.IsAlive())
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06007429 RID: 29737 RVA: 0x002B1BAC File Offset: 0x002AFDAC
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04005FE8 RID: 24552
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005FE9 RID: 24553
		private ParticleSystem particleSystem;

		// Token: 0x04005FEA RID: 24554
		private GameObject prevGameObject;
	}
}
