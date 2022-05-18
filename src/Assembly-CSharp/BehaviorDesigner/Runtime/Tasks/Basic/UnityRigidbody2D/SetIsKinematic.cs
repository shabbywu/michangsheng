using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody2D
{
	// Token: 0x02001541 RID: 5441
	[TaskCategory("Basic/Rigidbody2D")]
	[TaskDescription("Sets the is kinematic value of the Rigidbody2D. Returns Success.")]
	public class SetIsKinematic : Action
	{
		// Token: 0x06008102 RID: 33026 RVA: 0x002CBB24 File Offset: 0x002C9D24
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008103 RID: 33027 RVA: 0x00057FA8 File Offset: 0x000561A8
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return 1;
			}
			this.rigidbody2D.isKinematic = this.isKinematic.Value;
			return 2;
		}

		// Token: 0x06008104 RID: 33028 RVA: 0x00057FDB File Offset: 0x000561DB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.isKinematic = false;
		}

		// Token: 0x04006DB6 RID: 28086
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006DB7 RID: 28087
		[Tooltip("The is kinematic value of the Rigidbody2D")]
		public SharedBool isKinematic;

		// Token: 0x04006DB8 RID: 28088
		private Rigidbody2D rigidbody2D;

		// Token: 0x04006DB9 RID: 28089
		private GameObject prevGameObject;
	}
}
