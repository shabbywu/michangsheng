using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody2D
{
	// Token: 0x0200153E RID: 5438
	[TaskCategory("Basic/Rigidbody2D")]
	[TaskDescription("Sets the angular velocity of the Rigidbody2D. Returns Success.")]
	public class SetAngularVelocity : Action
	{
		// Token: 0x060080F6 RID: 33014 RVA: 0x002CBA64 File Offset: 0x002C9C64
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060080F7 RID: 33015 RVA: 0x00057EC4 File Offset: 0x000560C4
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return 1;
			}
			this.rigidbody2D.angularVelocity = this.angularVelocity.Value;
			return 2;
		}

		// Token: 0x060080F8 RID: 33016 RVA: 0x00057EF7 File Offset: 0x000560F7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.angularVelocity = 0f;
		}

		// Token: 0x04006DAA RID: 28074
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006DAB RID: 28075
		[Tooltip("The angular velocity of the Rigidbody2D")]
		public SharedFloat angularVelocity;

		// Token: 0x04006DAC RID: 28076
		private Rigidbody2D rigidbody2D;

		// Token: 0x04006DAD RID: 28077
		private GameObject prevGameObject;
	}
}
