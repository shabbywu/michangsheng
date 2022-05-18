using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody2D
{
	// Token: 0x02001539 RID: 5433
	[TaskCategory("Basic/Rigidbody2D")]
	[TaskDescription("Returns Success if the Rigidbody2D is kinematic, otherwise Failure.")]
	public class IsKinematic : Conditional
	{
		// Token: 0x060080E2 RID: 32994 RVA: 0x002CB924 File Offset: 0x002C9B24
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060080E3 RID: 32995 RVA: 0x00057D76 File Offset: 0x00055F76
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return 1;
			}
			if (!this.rigidbody2D.isKinematic)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x060080E4 RID: 32996 RVA: 0x00057DA2 File Offset: 0x00055FA2
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04006D98 RID: 28056
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006D99 RID: 28057
		private Rigidbody2D rigidbody2D;

		// Token: 0x04006D9A RID: 28058
		private GameObject prevGameObject;
	}
}
