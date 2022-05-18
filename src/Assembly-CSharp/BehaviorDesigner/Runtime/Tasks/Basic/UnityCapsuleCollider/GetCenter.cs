using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCapsuleCollider
{
	// Token: 0x02001613 RID: 5651
	[TaskCategory("Basic/CapsuleCollider")]
	[TaskDescription("Stores the center of the CapsuleCollider. Returns Success.")]
	public class GetCenter : Action
	{
		// Token: 0x060083E1 RID: 33761 RVA: 0x002CF060 File Offset: 0x002CD260
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.capsuleCollider = defaultGameObject.GetComponent<CapsuleCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060083E2 RID: 33762 RVA: 0x0005AEAE File Offset: 0x000590AE
		public override TaskStatus OnUpdate()
		{
			if (this.capsuleCollider == null)
			{
				Debug.LogWarning("CapsuleCollider is null");
				return 1;
			}
			this.storeValue.Value = this.capsuleCollider.center;
			return 2;
		}

		// Token: 0x060083E3 RID: 33763 RVA: 0x0005AEE1 File Offset: 0x000590E1
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x04007090 RID: 28816
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04007091 RID: 28817
		[Tooltip("The center of the CapsuleCollider")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x04007092 RID: 28818
		private CapsuleCollider capsuleCollider;

		// Token: 0x04007093 RID: 28819
		private GameObject prevGameObject;
	}
}
