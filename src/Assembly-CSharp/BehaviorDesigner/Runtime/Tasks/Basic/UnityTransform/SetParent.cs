using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x02001037 RID: 4151
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Sets the parent of the Transform. Returns Success.")]
	public class SetParent : Action
	{
		// Token: 0x060071F7 RID: 29175 RVA: 0x002AD044 File Offset: 0x002AB244
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060071F8 RID: 29176 RVA: 0x002AD084 File Offset: 0x002AB284
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

		// Token: 0x060071F9 RID: 29177 RVA: 0x002AD0B7 File Offset: 0x002AB2B7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.parent = null;
		}

		// Token: 0x04005DD8 RID: 24024
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005DD9 RID: 24025
		[Tooltip("The parent of the Transform")]
		public SharedTransform parent;

		// Token: 0x04005DDA RID: 24026
		private Transform targetTransform;

		// Token: 0x04005DDB RID: 24027
		private GameObject prevGameObject;
	}
}
