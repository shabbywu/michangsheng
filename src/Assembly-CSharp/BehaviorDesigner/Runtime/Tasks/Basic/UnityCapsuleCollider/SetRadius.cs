using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCapsuleCollider
{
	// Token: 0x0200161A RID: 5658
	[TaskCategory("Basic/CapsuleCollider")]
	[TaskDescription("Sets the radius of the CapsuleCollider. Returns Success.")]
	public class SetRadius : Action
	{
		// Token: 0x060083FD RID: 33789 RVA: 0x002CF220 File Offset: 0x002CD420
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.capsuleCollider = defaultGameObject.GetComponent<CapsuleCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060083FE RID: 33790 RVA: 0x0005B0BA File Offset: 0x000592BA
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

		// Token: 0x060083FF RID: 33791 RVA: 0x0005B0ED File Offset: 0x000592ED
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.radius = 0f;
		}

		// Token: 0x040070AC RID: 28844
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040070AD RID: 28845
		[Tooltip("The radius of the CapsuleCollider")]
		public SharedFloat radius;

		// Token: 0x040070AE RID: 28846
		private CapsuleCollider capsuleCollider;

		// Token: 0x040070AF RID: 28847
		private GameObject prevGameObject;
	}
}
