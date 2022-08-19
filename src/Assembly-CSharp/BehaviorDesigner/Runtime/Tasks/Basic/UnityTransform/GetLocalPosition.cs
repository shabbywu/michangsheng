using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x02001025 RID: 4133
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Stores the local position of the Transform. Returns Success.")]
	public class GetLocalPosition : Action
	{
		// Token: 0x060071AF RID: 29103 RVA: 0x002AC5C4 File Offset: 0x002AA7C4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060071B0 RID: 29104 RVA: 0x002AC604 File Offset: 0x002AA804
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return 1;
			}
			this.storeValue.Value = this.targetTransform.localPosition;
			return 2;
		}

		// Token: 0x060071B1 RID: 29105 RVA: 0x002AC637 File Offset: 0x002AA837
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x04005D8B RID: 23947
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005D8C RID: 23948
		[Tooltip("The local position of the Transform")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x04005D8D RID: 23949
		private Transform targetTransform;

		// Token: 0x04005D8E RID: 23950
		private GameObject prevGameObject;
	}
}
