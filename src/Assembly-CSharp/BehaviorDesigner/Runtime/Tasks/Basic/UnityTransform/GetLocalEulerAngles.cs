using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x020014DE RID: 5342
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Stores the local euler angles of the Transform. Returns Success.")]
	public class GetLocalEulerAngles : Action
	{
		// Token: 0x06007FA5 RID: 32677 RVA: 0x002CA5CC File Offset: 0x002C87CC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007FA6 RID: 32678 RVA: 0x00056904 File Offset: 0x00054B04
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return 1;
			}
			this.storeValue.Value = this.targetTransform.localEulerAngles;
			return 2;
		}

		// Token: 0x06007FA7 RID: 32679 RVA: 0x00056937 File Offset: 0x00054B37
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x04006C87 RID: 27783
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006C88 RID: 27784
		[Tooltip("The local euler angles of the Transform")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x04006C89 RID: 27785
		private Transform targetTransform;

		// Token: 0x04006C8A RID: 27786
		private GameObject prevGameObject;
	}
}
