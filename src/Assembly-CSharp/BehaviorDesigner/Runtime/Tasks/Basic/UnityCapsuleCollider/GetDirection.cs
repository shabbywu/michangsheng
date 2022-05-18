using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCapsuleCollider
{
	// Token: 0x02001614 RID: 5652
	[TaskCategory("Basic/CapsuleCollider")]
	[TaskDescription("Stores the direction of the CapsuleCollider. Returns Success.")]
	public class GetDirection : Action
	{
		// Token: 0x060083E5 RID: 33765 RVA: 0x002CF0A0 File Offset: 0x002CD2A0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.capsuleCollider = defaultGameObject.GetComponent<CapsuleCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060083E6 RID: 33766 RVA: 0x0005AEFA File Offset: 0x000590FA
		public override TaskStatus OnUpdate()
		{
			if (this.capsuleCollider == null)
			{
				Debug.LogWarning("CapsuleCollider is null");
				return 1;
			}
			this.storeValue.Value = this.capsuleCollider.direction;
			return 2;
		}

		// Token: 0x060083E7 RID: 33767 RVA: 0x0005AF2D File Offset: 0x0005912D
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0;
		}

		// Token: 0x04007094 RID: 28820
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04007095 RID: 28821
		[Tooltip("The direction of the CapsuleCollider")]
		[RequiredField]
		public SharedInt storeValue;

		// Token: 0x04007096 RID: 28822
		private CapsuleCollider capsuleCollider;

		// Token: 0x04007097 RID: 28823
		private GameObject prevGameObject;
	}
}
