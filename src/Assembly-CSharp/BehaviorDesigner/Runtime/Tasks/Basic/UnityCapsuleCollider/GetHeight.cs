using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCapsuleCollider
{
	// Token: 0x02001615 RID: 5653
	[TaskCategory("Basic/CapsuleCollider")]
	[TaskDescription("Gets the height of the CapsuleCollider. Returns Success.")]
	public class GetHeight : Action
	{
		// Token: 0x060083E9 RID: 33769 RVA: 0x002CF0E0 File Offset: 0x002CD2E0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.capsuleCollider = defaultGameObject.GetComponent<CapsuleCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060083EA RID: 33770 RVA: 0x0005AF42 File Offset: 0x00059142
		public override TaskStatus OnUpdate()
		{
			if (this.capsuleCollider == null)
			{
				Debug.LogWarning("CapsuleCollider is null");
				return 1;
			}
			this.storeValue.Value = this.capsuleCollider.height;
			return 2;
		}

		// Token: 0x060083EB RID: 33771 RVA: 0x0005AF75 File Offset: 0x00059175
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04007098 RID: 28824
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04007099 RID: 28825
		[Tooltip("The height of the CapsuleCollider")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x0400709A RID: 28826
		private CapsuleCollider capsuleCollider;

		// Token: 0x0400709B RID: 28827
		private GameObject prevGameObject;
	}
}
