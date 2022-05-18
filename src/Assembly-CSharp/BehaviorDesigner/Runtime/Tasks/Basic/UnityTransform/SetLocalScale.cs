using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x020014F0 RID: 5360
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Sets the local scale of the Transform. Returns Success.")]
	public class SetLocalScale : Action
	{
		// Token: 0x06007FED RID: 32749 RVA: 0x002CAB14 File Offset: 0x002C8D14
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007FEE RID: 32750 RVA: 0x00056E36 File Offset: 0x00055036
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return 1;
			}
			this.targetTransform.localScale = this.localScale.Value;
			return 2;
		}

		// Token: 0x06007FEF RID: 32751 RVA: 0x00056E69 File Offset: 0x00055069
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.localScale = Vector3.zero;
		}

		// Token: 0x04006CD4 RID: 27860
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006CD5 RID: 27861
		[Tooltip("The local scale of the Transform")]
		public SharedVector3 localScale;

		// Token: 0x04006CD6 RID: 27862
		private Transform targetTransform;

		// Token: 0x04006CD7 RID: 27863
		private GameObject prevGameObject;
	}
}
