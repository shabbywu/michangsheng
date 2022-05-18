using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x020014EB RID: 5355
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Sets the euler angles of the Transform. Returns Success.")]
	public class SetEulerAngles : Action
	{
		// Token: 0x06007FD9 RID: 32729 RVA: 0x002CA9D4 File Offset: 0x002C8BD4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007FDA RID: 32730 RVA: 0x00056CBA File Offset: 0x00054EBA
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return 1;
			}
			this.targetTransform.eulerAngles = this.eulerAngles.Value;
			return 2;
		}

		// Token: 0x06007FDB RID: 32731 RVA: 0x00056CED File Offset: 0x00054EED
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.eulerAngles = Vector3.zero;
		}

		// Token: 0x04006CC0 RID: 27840
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006CC1 RID: 27841
		[Tooltip("The euler angles of the Transform")]
		public SharedVector3 eulerAngles;

		// Token: 0x04006CC2 RID: 27842
		private Transform targetTransform;

		// Token: 0x04006CC3 RID: 27843
		private GameObject prevGameObject;
	}
}
