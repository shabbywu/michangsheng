using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x020014E2 RID: 5346
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Stores the parent of the Transform. Returns Success.")]
	public class GetParent : Action
	{
		// Token: 0x06007FB5 RID: 32693 RVA: 0x002CA6CC File Offset: 0x002C88CC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007FB6 RID: 32694 RVA: 0x00056A34 File Offset: 0x00054C34
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return 1;
			}
			this.storeValue.Value = this.targetTransform.parent;
			return 2;
		}

		// Token: 0x06007FB7 RID: 32695 RVA: 0x00056A67 File Offset: 0x00054C67
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = null;
		}

		// Token: 0x04006C97 RID: 27799
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006C98 RID: 27800
		[Tooltip("The parent of the Transform")]
		[RequiredField]
		public SharedTransform storeValue;

		// Token: 0x04006C99 RID: 27801
		private Transform targetTransform;

		// Token: 0x04006C9A RID: 27802
		private GameObject prevGameObject;
	}
}
