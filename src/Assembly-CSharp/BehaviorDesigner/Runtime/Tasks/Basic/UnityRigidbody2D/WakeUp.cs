using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody2D
{
	// Token: 0x02001545 RID: 5445
	[TaskCategory("Basic/Rigidbody2D")]
	[TaskDescription("Forces the Rigidbody2D to wake up. Returns Success.")]
	public class WakeUp : Conditional
	{
		// Token: 0x06008112 RID: 33042 RVA: 0x002CBC24 File Offset: 0x002C9E24
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008113 RID: 33043 RVA: 0x000580B9 File Offset: 0x000562B9
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return 1;
			}
			this.rigidbody2D.WakeUp();
			return 2;
		}

		// Token: 0x06008114 RID: 33044 RVA: 0x000580E1 File Offset: 0x000562E1
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04006DC5 RID: 28101
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006DC6 RID: 28102
		private Rigidbody2D rigidbody2D;

		// Token: 0x04006DC7 RID: 28103
		private GameObject prevGameObject;
	}
}
