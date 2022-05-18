using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody2D
{
	// Token: 0x0200153A RID: 5434
	[TaskCategory("Basic/Rigidbody2D")]
	[TaskDescription("Returns Success if the Rigidbody2D is sleeping, otherwise Failure.")]
	public class IsSleeping : Conditional
	{
		// Token: 0x060080E6 RID: 32998 RVA: 0x002CB964 File Offset: 0x002C9B64
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060080E7 RID: 32999 RVA: 0x00057DAB File Offset: 0x00055FAB
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return 1;
			}
			if (!this.rigidbody2D.IsSleeping())
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x060080E8 RID: 33000 RVA: 0x00057DD7 File Offset: 0x00055FD7
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04006D9B RID: 28059
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006D9C RID: 28060
		private Rigidbody2D rigidbody2D;

		// Token: 0x04006D9D RID: 28061
		private GameObject prevGameObject;
	}
}
