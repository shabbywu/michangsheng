using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x020014DC RID: 5340
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Stores the euler angles of the Transform. Returns Success.")]
	public class GetEulerAngles : Action
	{
		// Token: 0x06007F9D RID: 32669 RVA: 0x002CA54C File Offset: 0x002C874C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007F9E RID: 32670 RVA: 0x0005686C File Offset: 0x00054A6C
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return 1;
			}
			this.storeValue.Value = this.targetTransform.eulerAngles;
			return 2;
		}

		// Token: 0x06007F9F RID: 32671 RVA: 0x0005689F File Offset: 0x00054A9F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x04006C7F RID: 27775
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006C80 RID: 27776
		[Tooltip("The euler angles of the Transform")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x04006C81 RID: 27777
		private Transform targetTransform;

		// Token: 0x04006C82 RID: 27778
		private GameObject prevGameObject;
	}
}
