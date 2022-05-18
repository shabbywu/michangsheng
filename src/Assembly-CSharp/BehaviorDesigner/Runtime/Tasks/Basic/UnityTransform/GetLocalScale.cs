using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x020014E1 RID: 5345
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Stores the local scale of the Transform. Returns Success.")]
	public class GetLocalScale : Action
	{
		// Token: 0x06007FB1 RID: 32689 RVA: 0x002CA68C File Offset: 0x002C888C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007FB2 RID: 32690 RVA: 0x000569E8 File Offset: 0x00054BE8
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return 1;
			}
			this.storeValue.Value = this.targetTransform.localScale;
			return 2;
		}

		// Token: 0x06007FB3 RID: 32691 RVA: 0x00056A1B File Offset: 0x00054C1B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x04006C93 RID: 27795
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006C94 RID: 27796
		[Tooltip("The local scale of the Transform")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x04006C95 RID: 27797
		private Transform targetTransform;

		// Token: 0x04006C96 RID: 27798
		private GameObject prevGameObject;
	}
}
