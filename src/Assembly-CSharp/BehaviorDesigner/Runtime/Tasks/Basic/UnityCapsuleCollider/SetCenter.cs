using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCapsuleCollider
{
	// Token: 0x02001158 RID: 4440
	[TaskCategory("Basic/CapsuleCollider")]
	[TaskDescription("Sets the center of the CapsuleCollider. Returns Success.")]
	public class SetCenter : Action
	{
		// Token: 0x060075F7 RID: 30199 RVA: 0x002B5808 File Offset: 0x002B3A08
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.capsuleCollider = defaultGameObject.GetComponent<CapsuleCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060075F8 RID: 30200 RVA: 0x002B5848 File Offset: 0x002B3A48
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

		// Token: 0x060075F9 RID: 30201 RVA: 0x002B587B File Offset: 0x002B3A7B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.center = Vector3.zero;
		}

		// Token: 0x0400617D RID: 24957
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400617E RID: 24958
		[Tooltip("The center of the CapsuleCollider")]
		public SharedVector3 center;

		// Token: 0x0400617F RID: 24959
		private CapsuleCollider capsuleCollider;

		// Token: 0x04006180 RID: 24960
		private GameObject prevGameObject;
	}
}
