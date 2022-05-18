using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCapsuleCollider
{
	// Token: 0x02001617 RID: 5655
	[TaskCategory("Basic/CapsuleCollider")]
	[TaskDescription("Sets the center of the CapsuleCollider. Returns Success.")]
	public class SetCenter : Action
	{
		// Token: 0x060083F1 RID: 33777 RVA: 0x002CF160 File Offset: 0x002CD360
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.capsuleCollider = defaultGameObject.GetComponent<CapsuleCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060083F2 RID: 33778 RVA: 0x0005AFDA File Offset: 0x000591DA
		public override TaskStatus OnUpdate()
		{
			if (this.capsuleCollider == null)
			{
				Debug.LogWarning("CapsuleCollider is null");
				return 1;
			}
			this.capsuleCollider.center = this.center.Value;
			return 2;
		}

		// Token: 0x060083F3 RID: 33779 RVA: 0x0005B00D File Offset: 0x0005920D
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.center = Vector3.zero;
		}

		// Token: 0x040070A0 RID: 28832
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040070A1 RID: 28833
		[Tooltip("The center of the CapsuleCollider")]
		public SharedVector3 center;

		// Token: 0x040070A2 RID: 28834
		private CapsuleCollider capsuleCollider;

		// Token: 0x040070A3 RID: 28835
		private GameObject prevGameObject;
	}
}
