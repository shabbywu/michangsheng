using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x0200102B RID: 4139
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Stores the rotation of the Transform. Returns Success.")]
	public class GetRotation : Action
	{
		// Token: 0x060071C7 RID: 29127 RVA: 0x002AC904 File Offset: 0x002AAB04
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060071C8 RID: 29128 RVA: 0x002AC944 File Offset: 0x002AAB44
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return 1;
			}
			this.storeValue.Value = this.targetTransform.rotation;
			return 2;
		}

		// Token: 0x060071C9 RID: 29129 RVA: 0x002AC977 File Offset: 0x002AAB77
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Quaternion.identity;
		}

		// Token: 0x04005DA3 RID: 23971
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005DA4 RID: 23972
		[Tooltip("The rotation of the Transform")]
		[RequiredField]
		public SharedQuaternion storeValue;

		// Token: 0x04005DA5 RID: 23973
		private Transform targetTransform;

		// Token: 0x04005DA6 RID: 23974
		private GameObject prevGameObject;
	}
}
