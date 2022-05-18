using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x020014E5 RID: 5349
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Stores the rotation of the Transform. Returns Success.")]
	public class GetRotation : Action
	{
		// Token: 0x06007FC1 RID: 32705 RVA: 0x002CA78C File Offset: 0x002C898C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007FC2 RID: 32706 RVA: 0x00056B0F File Offset: 0x00054D0F
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return 1;
			}
			this.storeValue.Value = this.targetTransform.rotation;
			return 2;
		}

		// Token: 0x06007FC3 RID: 32707 RVA: 0x00056B42 File Offset: 0x00054D42
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Quaternion.identity;
		}

		// Token: 0x04006CA3 RID: 27811
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006CA4 RID: 27812
		[Tooltip("The rotation of the Transform")]
		[RequiredField]
		public SharedQuaternion storeValue;

		// Token: 0x04006CA5 RID: 27813
		private Transform targetTransform;

		// Token: 0x04006CA6 RID: 27814
		private GameObject prevGameObject;
	}
}
