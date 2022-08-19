using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnitySphereCollider
{
	// Token: 0x0200104B RID: 4171
	[TaskCategory("Basic/SphereCollider")]
	[TaskDescription("Stores the center of the SphereCollider. Returns Success.")]
	public class GetCenter : Action
	{
		// Token: 0x0600723A RID: 29242 RVA: 0x002AD7E4 File Offset: 0x002AB9E4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.sphereCollider = defaultGameObject.GetComponent<SphereCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600723B RID: 29243 RVA: 0x002AD824 File Offset: 0x002ABA24
		public override TaskStatus OnUpdate()
		{
			if (this.sphereCollider == null)
			{
				Debug.LogWarning("SphereCollider is null");
				return 1;
			}
			this.storeValue.Value = this.sphereCollider.center;
			return 2;
		}

		// Token: 0x0600723C RID: 29244 RVA: 0x002AD857 File Offset: 0x002ABA57
		public override void OnReset()
		{
			this.storeValue = Vector3.zero;
		}

		// Token: 0x04005E0E RID: 24078
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005E0F RID: 24079
		[Tooltip("The center of the SphereCollider")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x04005E10 RID: 24080
		private SphereCollider sphereCollider;

		// Token: 0x04005E11 RID: 24081
		private GameObject prevGameObject;
	}
}
