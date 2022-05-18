using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody2D
{
	// Token: 0x02001534 RID: 5428
	[TaskCategory("Basic/Rigidbody2D")]
	[TaskDescription("Stores the is kinematic value of the Rigidbody2D. Returns Success.")]
	public class GetIsKinematic : Action
	{
		// Token: 0x060080CE RID: 32974 RVA: 0x002CB7E4 File Offset: 0x002C99E4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060080CF RID: 32975 RVA: 0x00057BFE File Offset: 0x00055DFE
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return 1;
			}
			this.storeValue.Value = this.rigidbody2D.isKinematic;
			return 2;
		}

		// Token: 0x060080D0 RID: 32976 RVA: 0x00057C31 File Offset: 0x00055E31
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = false;
		}

		// Token: 0x04006D84 RID: 28036
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006D85 RID: 28037
		[Tooltip("The is kinematic value of the Rigidbody2D")]
		[RequiredField]
		public SharedBool storeValue;

		// Token: 0x04006D86 RID: 28038
		private Rigidbody2D rigidbody2D;

		// Token: 0x04006D87 RID: 28039
		private GameObject prevGameObject;
	}
}
