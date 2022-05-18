using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnitySphereCollider
{
	// Token: 0x02001505 RID: 5381
	[TaskCategory("Basic/SphereCollider")]
	[TaskDescription("Stores the center of the SphereCollider. Returns Success.")]
	public class GetCenter : Action
	{
		// Token: 0x06008034 RID: 32820 RVA: 0x002CAE60 File Offset: 0x002C9060
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.sphereCollider = defaultGameObject.GetComponent<SphereCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008035 RID: 32821 RVA: 0x00057318 File Offset: 0x00055518
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

		// Token: 0x06008036 RID: 32822 RVA: 0x0005734B File Offset: 0x0005554B
		public override void OnReset()
		{
			this.storeValue = Vector3.zero;
		}

		// Token: 0x04006D0E RID: 27918
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006D0F RID: 27919
		[Tooltip("The center of the SphereCollider")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x04006D10 RID: 27920
		private SphereCollider sphereCollider;

		// Token: 0x04006D11 RID: 27921
		private GameObject prevGameObject;
	}
}
