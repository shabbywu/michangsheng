using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x02001028 RID: 4136
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Stores the parent of the Transform. Returns Success.")]
	public class GetParent : Action
	{
		// Token: 0x060071BB RID: 29115 RVA: 0x002AC768 File Offset: 0x002AA968
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060071BC RID: 29116 RVA: 0x002AC7A8 File Offset: 0x002AA9A8
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

		// Token: 0x060071BD RID: 29117 RVA: 0x002AC7DB File Offset: 0x002AA9DB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = null;
		}

		// Token: 0x04005D97 RID: 23959
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005D98 RID: 23960
		[Tooltip("The parent of the Transform")]
		[RequiredField]
		public SharedTransform storeValue;

		// Token: 0x04005D99 RID: 23961
		private Transform targetTransform;

		// Token: 0x04005D9A RID: 23962
		private GameObject prevGameObject;
	}
}
