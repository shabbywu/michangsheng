using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x020014F2 RID: 5362
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Sets the position of the Transform. Returns Success.")]
	public class SetPosition : Action
	{
		// Token: 0x06007FF5 RID: 32757 RVA: 0x002CAB94 File Offset: 0x002C8D94
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007FF6 RID: 32758 RVA: 0x00056EC5 File Offset: 0x000550C5
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return 1;
			}
			this.targetTransform.position = this.position.Value;
			return 2;
		}

		// Token: 0x06007FF7 RID: 32759 RVA: 0x00056EF8 File Offset: 0x000550F8
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.position = Vector3.zero;
		}

		// Token: 0x04006CDC RID: 27868
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006CDD RID: 27869
		[Tooltip("The position of the Transform")]
		public SharedVector3 position;

		// Token: 0x04006CDE RID: 27870
		private Transform targetTransform;

		// Token: 0x04006CDF RID: 27871
		private GameObject prevGameObject;
	}
}
