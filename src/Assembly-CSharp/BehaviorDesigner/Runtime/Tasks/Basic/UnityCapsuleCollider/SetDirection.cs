using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCapsuleCollider
{
	// Token: 0x02001159 RID: 4441
	[TaskCategory("Basic/CapsuleCollider")]
	[TaskDescription("Sets the direction of the CapsuleCollider. Returns Success.")]
	public class SetDirection : Action
	{
		// Token: 0x060075FB RID: 30203 RVA: 0x002B5894 File Offset: 0x002B3A94
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.capsuleCollider = defaultGameObject.GetComponent<CapsuleCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060075FC RID: 30204 RVA: 0x002B58D4 File Offset: 0x002B3AD4
		public override TaskStatus OnUpdate()
		{
			if (this.capsuleCollider == null)
			{
				Debug.LogWarning("CapsuleCollider is null");
				return 1;
			}
			this.capsuleCollider.direction = this.direction.Value;
			return 2;
		}

		// Token: 0x060075FD RID: 30205 RVA: 0x002B5907 File Offset: 0x002B3B07
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.direction = 0;
		}

		// Token: 0x04006181 RID: 24961
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006182 RID: 24962
		[Tooltip("The direction of the CapsuleCollider")]
		public SharedInt direction;

		// Token: 0x04006183 RID: 24963
		private CapsuleCollider capsuleCollider;

		// Token: 0x04006184 RID: 24964
		private GameObject prevGameObject;
	}
}
