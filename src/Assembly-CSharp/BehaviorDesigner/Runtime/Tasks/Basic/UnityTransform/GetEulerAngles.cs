using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x02001022 RID: 4130
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Stores the euler angles of the Transform. Returns Success.")]
	public class GetEulerAngles : Action
	{
		// Token: 0x060071A3 RID: 29091 RVA: 0x002AC420 File Offset: 0x002AA620
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060071A4 RID: 29092 RVA: 0x002AC460 File Offset: 0x002AA660
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

		// Token: 0x060071A5 RID: 29093 RVA: 0x002AC493 File Offset: 0x002AA693
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x04005D7F RID: 23935
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005D80 RID: 23936
		[Tooltip("The euler angles of the Transform")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x04005D81 RID: 23937
		private Transform targetTransform;

		// Token: 0x04005D82 RID: 23938
		private GameObject prevGameObject;
	}
}
