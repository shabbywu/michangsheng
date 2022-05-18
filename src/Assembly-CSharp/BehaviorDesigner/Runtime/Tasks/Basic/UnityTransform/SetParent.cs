using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x020014F1 RID: 5361
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Sets the parent of the Transform. Returns Success.")]
	public class SetParent : Action
	{
		// Token: 0x06007FF1 RID: 32753 RVA: 0x002CAB54 File Offset: 0x002C8D54
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007FF2 RID: 32754 RVA: 0x00056E82 File Offset: 0x00055082
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return 1;
			}
			this.targetTransform.parent = this.parent.Value;
			return 2;
		}

		// Token: 0x06007FF3 RID: 32755 RVA: 0x00056EB5 File Offset: 0x000550B5
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.parent = null;
		}

		// Token: 0x04006CD8 RID: 27864
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006CD9 RID: 27865
		[Tooltip("The parent of the Transform")]
		public SharedTransform parent;

		// Token: 0x04006CDA RID: 27866
		private Transform targetTransform;

		// Token: 0x04006CDB RID: 27867
		private GameObject prevGameObject;
	}
}
