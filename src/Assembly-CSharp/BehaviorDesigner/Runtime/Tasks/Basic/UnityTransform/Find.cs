using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x020014D8 RID: 5336
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Finds a transform by name. Returns Success.")]
	public class Find : Action
	{
		// Token: 0x06007F8D RID: 32653 RVA: 0x002CA390 File Offset: 0x002C8590
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007F8E RID: 32654 RVA: 0x00056725 File Offset: 0x00054925
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return 1;
			}
			this.storeValue.Value = this.targetTransform.Find(this.transformName.Value);
			return 2;
		}

		// Token: 0x06007F8F RID: 32655 RVA: 0x00056763 File Offset: 0x00054963
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.transformName = null;
			this.storeValue = null;
		}

		// Token: 0x04006C6A RID: 27754
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006C6B RID: 27755
		[Tooltip("The transform name to find")]
		public SharedString transformName;

		// Token: 0x04006C6C RID: 27756
		[Tooltip("The object found by name")]
		[RequiredField]
		public SharedTransform storeValue;

		// Token: 0x04006C6D RID: 27757
		private Transform targetTransform;

		// Token: 0x04006C6E RID: 27758
		private GameObject prevGameObject;
	}
}
