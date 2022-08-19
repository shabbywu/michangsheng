using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCapsuleCollider
{
	// Token: 0x0200115B RID: 4443
	[TaskCategory("Basic/CapsuleCollider")]
	[TaskDescription("Sets the radius of the CapsuleCollider. Returns Success.")]
	public class SetRadius : Action
	{
		// Token: 0x06007603 RID: 30211 RVA: 0x002B59A8 File Offset: 0x002B3BA8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.capsuleCollider = defaultGameObject.GetComponent<CapsuleCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007604 RID: 30212 RVA: 0x002B59E8 File Offset: 0x002B3BE8
		public override TaskStatus OnUpdate()
		{
			if (this.capsuleCollider == null)
			{
				Debug.LogWarning("CapsuleCollider is null");
				return 1;
			}
			this.capsuleCollider.radius = this.radius.Value;
			return 2;
		}

		// Token: 0x06007605 RID: 30213 RVA: 0x002B5A1B File Offset: 0x002B3C1B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.radius = 0f;
		}

		// Token: 0x04006189 RID: 24969
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400618A RID: 24970
		[Tooltip("The radius of the CapsuleCollider")]
		public SharedFloat radius;

		// Token: 0x0400618B RID: 24971
		private CapsuleCollider capsuleCollider;

		// Token: 0x0400618C RID: 24972
		private GameObject prevGameObject;
	}
}
