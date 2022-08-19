using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x02001023 RID: 4131
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Stores the forward vector of the Transform. Returns Success.")]
	public class GetForwardVector : Action
	{
		// Token: 0x060071A7 RID: 29095 RVA: 0x002AC4AC File Offset: 0x002AA6AC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060071A8 RID: 29096 RVA: 0x002AC4EC File Offset: 0x002AA6EC
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return 1;
			}
			this.storeValue.Value = this.targetTransform.forward;
			return 2;
		}

		// Token: 0x060071A9 RID: 29097 RVA: 0x002AC51F File Offset: 0x002AA71F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x04005D83 RID: 23939
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005D84 RID: 23940
		[Tooltip("The position of the Transform")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x04005D85 RID: 23941
		private Transform targetTransform;

		// Token: 0x04005D86 RID: 23942
		private GameObject prevGameObject;
	}
}
