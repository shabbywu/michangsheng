using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCapsuleCollider
{
	// Token: 0x02001154 RID: 4436
	[TaskCategory("Basic/CapsuleCollider")]
	[TaskDescription("Stores the center of the CapsuleCollider. Returns Success.")]
	public class GetCenter : Action
	{
		// Token: 0x060075E7 RID: 30183 RVA: 0x002B55DC File Offset: 0x002B37DC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.capsuleCollider = defaultGameObject.GetComponent<CapsuleCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060075E8 RID: 30184 RVA: 0x002B561C File Offset: 0x002B381C
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

		// Token: 0x060075E9 RID: 30185 RVA: 0x002B564F File Offset: 0x002B384F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x0400616D RID: 24941
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400616E RID: 24942
		[Tooltip("The center of the CapsuleCollider")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x0400616F RID: 24943
		private CapsuleCollider capsuleCollider;

		// Token: 0x04006170 RID: 24944
		private GameObject prevGameObject;
	}
}
