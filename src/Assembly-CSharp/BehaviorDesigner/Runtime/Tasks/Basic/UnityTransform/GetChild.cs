using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x02001020 RID: 4128
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Stores the transform child at the specified index. Returns Success.")]
	public class GetChild : Action
	{
		// Token: 0x0600719B RID: 29083 RVA: 0x002AC2FC File Offset: 0x002AA4FC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600719C RID: 29084 RVA: 0x002AC33C File Offset: 0x002AA53C
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return 1;
			}
			this.storeValue.Value = this.targetTransform.GetChild(this.index.Value);
			return 2;
		}

		// Token: 0x0600719D RID: 29085 RVA: 0x002AC37A File Offset: 0x002AA57A
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.index = 0;
			this.storeValue = null;
		}

		// Token: 0x04005D76 RID: 23926
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005D77 RID: 23927
		[Tooltip("The index of the child")]
		public SharedInt index;

		// Token: 0x04005D78 RID: 23928
		[Tooltip("The child of the Transform")]
		[RequiredField]
		public SharedTransform storeValue;

		// Token: 0x04005D79 RID: 23929
		private Transform targetTransform;

		// Token: 0x04005D7A RID: 23930
		private GameObject prevGameObject;
	}
}
