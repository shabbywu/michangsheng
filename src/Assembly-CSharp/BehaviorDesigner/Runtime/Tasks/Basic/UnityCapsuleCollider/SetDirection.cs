using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCapsuleCollider
{
	// Token: 0x02001618 RID: 5656
	[TaskCategory("Basic/CapsuleCollider")]
	[TaskDescription("Sets the direction of the CapsuleCollider. Returns Success.")]
	public class SetDirection : Action
	{
		// Token: 0x060083F5 RID: 33781 RVA: 0x002CF1A0 File Offset: 0x002CD3A0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.capsuleCollider = defaultGameObject.GetComponent<CapsuleCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060083F6 RID: 33782 RVA: 0x0005B026 File Offset: 0x00059226
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

		// Token: 0x060083F7 RID: 33783 RVA: 0x0005B059 File Offset: 0x00059259
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.direction = 0;
		}

		// Token: 0x040070A4 RID: 28836
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040070A5 RID: 28837
		[Tooltip("The direction of the CapsuleCollider")]
		public SharedInt direction;

		// Token: 0x040070A6 RID: 28838
		private CapsuleCollider capsuleCollider;

		// Token: 0x040070A7 RID: 28839
		private GameObject prevGameObject;
	}
}
