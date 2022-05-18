using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem
{
	// Token: 0x0200159B RID: 5531
	[TaskCategory("Basic/ParticleSystem")]
	[TaskDescription("Sets the start color of the Particle System.")]
	public class SetStartColor : Action
	{
		// Token: 0x0600824D RID: 33357 RVA: 0x002CD5E4 File Offset: 0x002CB7E4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600824E RID: 33358 RVA: 0x002CD624 File Offset: 0x002CB824
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return 1;
			}
			this.particleSystem.main.startColor = this.startColor.Value;
			return 2;
		}

		// Token: 0x0600824F RID: 33359 RVA: 0x00059477 File Offset: 0x00057677
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.startColor = Color.white;
		}

		// Token: 0x04006F0E RID: 28430
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006F0F RID: 28431
		[Tooltip("The start color of the ParticleSystem")]
		public SharedColor startColor;

		// Token: 0x04006F10 RID: 28432
		private ParticleSystem particleSystem;

		// Token: 0x04006F11 RID: 28433
		private GameObject prevGameObject;
	}
}
