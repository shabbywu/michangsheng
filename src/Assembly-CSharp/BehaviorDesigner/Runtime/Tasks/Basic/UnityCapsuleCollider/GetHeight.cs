using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCapsuleCollider
{
	// Token: 0x02001156 RID: 4438
	[TaskCategory("Basic/CapsuleCollider")]
	[TaskDescription("Gets the height of the CapsuleCollider. Returns Success.")]
	public class GetHeight : Action
	{
		// Token: 0x060075EF RID: 30191 RVA: 0x002B56F0 File Offset: 0x002B38F0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.capsuleCollider = defaultGameObject.GetComponent<CapsuleCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060075F0 RID: 30192 RVA: 0x002B5730 File Offset: 0x002B3930
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

		// Token: 0x060075F1 RID: 30193 RVA: 0x002B5763 File Offset: 0x002B3963
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04006175 RID: 24949
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006176 RID: 24950
		[Tooltip("The height of the CapsuleCollider")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x04006177 RID: 24951
		private CapsuleCollider capsuleCollider;

		// Token: 0x04006178 RID: 24952
		private GameObject prevGameObject;
	}
}
