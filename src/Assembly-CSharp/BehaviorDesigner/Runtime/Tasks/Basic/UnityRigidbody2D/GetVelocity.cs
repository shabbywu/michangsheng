using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody2D
{
	// Token: 0x02001538 RID: 5432
	[TaskCategory("Basic/Rigidbody2D")]
	[TaskDescription("Stores the velocity of the Rigidbody2D. Returns Success.")]
	public class GetVelocity : Action
	{
		// Token: 0x060080DE RID: 32990 RVA: 0x002CB8E4 File Offset: 0x002C9AE4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060080DF RID: 32991 RVA: 0x00057D2A File Offset: 0x00055F2A
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return 1;
			}
			this.storeValue.Value = this.rigidbody2D.velocity;
			return 2;
		}

		// Token: 0x060080E0 RID: 32992 RVA: 0x00057D5D File Offset: 0x00055F5D
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector2.zero;
		}

		// Token: 0x04006D94 RID: 28052
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006D95 RID: 28053
		[Tooltip("The velocity of the Rigidbody2D")]
		[RequiredField]
		public SharedVector2 storeValue;

		// Token: 0x04006D96 RID: 28054
		private Rigidbody2D rigidbody2D;

		// Token: 0x04006D97 RID: 28055
		private GameObject prevGameObject;
	}
}
