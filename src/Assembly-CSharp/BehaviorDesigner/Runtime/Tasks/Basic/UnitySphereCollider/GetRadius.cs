using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnitySphereCollider
{
	// Token: 0x02001506 RID: 5382
	[TaskCategory("Basic/SphereCollider")]
	[TaskDescription("Stores the radius of the SphereCollider. Returns Success.")]
	public class GetRadius : Action
	{
		// Token: 0x06008038 RID: 32824 RVA: 0x002CAEA0 File Offset: 0x002C90A0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.sphereCollider = defaultGameObject.GetComponent<SphereCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008039 RID: 32825 RVA: 0x0005735D File Offset: 0x0005555D
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

		// Token: 0x0600803A RID: 32826 RVA: 0x00057390 File Offset: 0x00055590
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04006D12 RID: 27922
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006D13 RID: 27923
		[Tooltip("The radius of the SphereCollider")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x04006D14 RID: 27924
		private SphereCollider sphereCollider;

		// Token: 0x04006D15 RID: 27925
		private GameObject prevGameObject;
	}
}
