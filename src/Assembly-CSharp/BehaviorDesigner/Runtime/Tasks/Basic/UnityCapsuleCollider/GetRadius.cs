using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCapsuleCollider
{
	// Token: 0x02001616 RID: 5654
	[TaskCategory("Basic/CapsuleCollider")]
	[TaskDescription("Stores the radius of the CapsuleCollider. Returns Success.")]
	public class GetRadius : Action
	{
		// Token: 0x060083ED RID: 33773 RVA: 0x002CF120 File Offset: 0x002CD320
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.capsuleCollider = defaultGameObject.GetComponent<CapsuleCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060083EE RID: 33774 RVA: 0x0005AF8E File Offset: 0x0005918E
		public override TaskStatus OnUpdate()
		{
			if (this.capsuleCollider == null)
			{
				Debug.LogWarning("CapsuleCollider is null");
				return 1;
			}
			this.storeValue.Value = this.capsuleCollider.radius;
			return 2;
		}

		// Token: 0x060083EF RID: 33775 RVA: 0x0005AFC1 File Offset: 0x000591C1
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x0400709C RID: 28828
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400709D RID: 28829
		[Tooltip("The radius of the CapsuleCollider")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x0400709E RID: 28830
		private CapsuleCollider capsuleCollider;

		// Token: 0x0400709F RID: 28831
		private GameObject prevGameObject;
	}
}
