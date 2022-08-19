using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnitySphereCollider
{
	// Token: 0x0200104C RID: 4172
	[TaskCategory("Basic/SphereCollider")]
	[TaskDescription("Stores the radius of the SphereCollider. Returns Success.")]
	public class GetRadius : Action
	{
		// Token: 0x0600723E RID: 29246 RVA: 0x002AD86C File Offset: 0x002ABA6C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.sphereCollider = defaultGameObject.GetComponent<SphereCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600723F RID: 29247 RVA: 0x002AD8AC File Offset: 0x002ABAAC
		public override TaskStatus OnUpdate()
		{
			if (this.sphereCollider == null)
			{
				Debug.LogWarning("SphereCollider is null");
				return 1;
			}
			this.storeValue.Value = this.sphereCollider.radius;
			return 2;
		}

		// Token: 0x06007240 RID: 29248 RVA: 0x002AD8DF File Offset: 0x002ABADF
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04005E12 RID: 24082
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005E13 RID: 24083
		[Tooltip("The radius of the SphereCollider")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x04005E14 RID: 24084
		private SphereCollider sphereCollider;

		// Token: 0x04005E15 RID: 24085
		private GameObject prevGameObject;
	}
}
