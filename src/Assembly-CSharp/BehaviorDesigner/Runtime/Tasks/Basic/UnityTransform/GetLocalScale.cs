using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x02001027 RID: 4135
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Stores the local scale of the Transform. Returns Success.")]
	public class GetLocalScale : Action
	{
		// Token: 0x060071B7 RID: 29111 RVA: 0x002AC6DC File Offset: 0x002AA8DC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060071B8 RID: 29112 RVA: 0x002AC71C File Offset: 0x002AA91C
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return 1;
			}
			this.storeValue.Value = this.targetTransform.localScale;
			return 2;
		}

		// Token: 0x060071B9 RID: 29113 RVA: 0x002AC74F File Offset: 0x002AA94F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x04005D93 RID: 23955
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005D94 RID: 23956
		[Tooltip("The local scale of the Transform")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x04005D95 RID: 23957
		private Transform targetTransform;

		// Token: 0x04005D96 RID: 23958
		private GameObject prevGameObject;
	}
}
