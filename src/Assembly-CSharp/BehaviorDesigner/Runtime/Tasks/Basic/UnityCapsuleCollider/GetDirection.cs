using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCapsuleCollider
{
	// Token: 0x02001155 RID: 4437
	[TaskCategory("Basic/CapsuleCollider")]
	[TaskDescription("Stores the direction of the CapsuleCollider. Returns Success.")]
	public class GetDirection : Action
	{
		// Token: 0x060075EB RID: 30187 RVA: 0x002B5668 File Offset: 0x002B3868
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.capsuleCollider = defaultGameObject.GetComponent<CapsuleCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060075EC RID: 30188 RVA: 0x002B56A8 File Offset: 0x002B38A8
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

		// Token: 0x060075ED RID: 30189 RVA: 0x002B56DB File Offset: 0x002B38DB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0;
		}

		// Token: 0x04006171 RID: 24945
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006172 RID: 24946
		[Tooltip("The direction of the CapsuleCollider")]
		[RequiredField]
		public SharedInt storeValue;

		// Token: 0x04006173 RID: 24947
		private CapsuleCollider capsuleCollider;

		// Token: 0x04006174 RID: 24948
		private GameObject prevGameObject;
	}
}
