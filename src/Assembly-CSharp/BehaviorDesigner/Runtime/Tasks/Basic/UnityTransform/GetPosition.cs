using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x02001029 RID: 4137
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Stores the position of the Transform. Returns Success.")]
	public class GetPosition : Action
	{
		// Token: 0x060071BF RID: 29119 RVA: 0x002AC7EC File Offset: 0x002AA9EC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060071C0 RID: 29120 RVA: 0x002AC82C File Offset: 0x002AAA2C
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return 1;
			}
			this.storeValue.Value = this.targetTransform.position;
			return 2;
		}

		// Token: 0x060071C1 RID: 29121 RVA: 0x002AC85F File Offset: 0x002AAA5F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x04005D9B RID: 23963
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005D9C RID: 23964
		[Tooltip("Can the target GameObject be empty?")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x04005D9D RID: 23965
		private Transform targetTransform;

		// Token: 0x04005D9E RID: 23966
		private GameObject prevGameObject;
	}
}
