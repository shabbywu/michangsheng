using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityBoxCollider
{
	// Token: 0x0200115E RID: 4446
	[TaskCategory("Basic/BoxCollider")]
	[TaskDescription("Stores the center of the BoxCollider. Returns Success.")]
	public class GetCenter : Action
	{
		// Token: 0x0600760F RID: 30223 RVA: 0x002B5B4C File Offset: 0x002B3D4C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.boxCollider = defaultGameObject.GetComponent<BoxCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007610 RID: 30224 RVA: 0x002B5B8C File Offset: 0x002B3D8C
		public override TaskStatus OnUpdate()
		{
			if (this.boxCollider == null)
			{
				Debug.LogWarning("BoxCollider is null");
				return 1;
			}
			this.storeValue.Value = this.boxCollider.center;
			return 2;
		}

		// Token: 0x06007611 RID: 30225 RVA: 0x002B5BBF File Offset: 0x002B3DBF
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x04006195 RID: 24981
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006196 RID: 24982
		[Tooltip("The center of the BoxCollider")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x04006197 RID: 24983
		private BoxCollider boxCollider;

		// Token: 0x04006198 RID: 24984
		private GameObject prevGameObject;
	}
}
