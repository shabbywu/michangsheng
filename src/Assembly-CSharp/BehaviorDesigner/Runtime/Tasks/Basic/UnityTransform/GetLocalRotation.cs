using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x02001026 RID: 4134
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Stores the local rotation of the Transform. Returns Success.")]
	public class GetLocalRotation : Action
	{
		// Token: 0x060071B3 RID: 29107 RVA: 0x002AC650 File Offset: 0x002AA850
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060071B4 RID: 29108 RVA: 0x002AC690 File Offset: 0x002AA890
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return 1;
			}
			this.storeValue.Value = this.targetTransform.localRotation;
			return 2;
		}

		// Token: 0x060071B5 RID: 29109 RVA: 0x002AC6C3 File Offset: 0x002AA8C3
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Quaternion.identity;
		}

		// Token: 0x04005D8F RID: 23951
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005D90 RID: 23952
		[Tooltip("The local rotation of the Transform")]
		[RequiredField]
		public SharedQuaternion storeValue;

		// Token: 0x04005D91 RID: 23953
		private Transform targetTransform;

		// Token: 0x04005D92 RID: 23954
		private GameObject prevGameObject;
	}
}
