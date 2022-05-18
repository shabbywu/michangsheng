using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody2D
{
	// Token: 0x02001530 RID: 5424
	[TaskCategory("Basic/Rigidbody2D")]
	[TaskDescription("Stores the angular drag of the Rigidbody2D. Returns Success.")]
	public class GetAngularDrag : Action
	{
		// Token: 0x060080BE RID: 32958 RVA: 0x002CB6E4 File Offset: 0x002C98E4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060080BF RID: 32959 RVA: 0x00057ACE File Offset: 0x00055CCE
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return 1;
			}
			this.storeValue.Value = this.rigidbody2D.angularDrag;
			return 2;
		}

		// Token: 0x060080C0 RID: 32960 RVA: 0x00057B01 File Offset: 0x00055D01
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04006D74 RID: 28020
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006D75 RID: 28021
		[Tooltip("The angular drag of the Rigidbody2D")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x04006D76 RID: 28022
		private Rigidbody2D rigidbody2D;

		// Token: 0x04006D77 RID: 28023
		private GameObject prevGameObject;
	}
}
