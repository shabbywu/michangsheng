using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x0200103A RID: 4154
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Sets the rotation of the Transform. Returns Success.")]
	public class SetRotation : Action
	{
		// Token: 0x06007203 RID: 29187 RVA: 0x002AD1E0 File Offset: 0x002AB3E0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007204 RID: 29188 RVA: 0x002AD220 File Offset: 0x002AB420
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return 1;
			}
			this.targetTransform.rotation = this.rotation.Value;
			return 2;
		}

		// Token: 0x06007205 RID: 29189 RVA: 0x002AD253 File Offset: 0x002AB453
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.rotation = Quaternion.identity;
		}

		// Token: 0x04005DE4 RID: 24036
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005DE5 RID: 24037
		[Tooltip("The rotation of the Transform")]
		public SharedQuaternion rotation;

		// Token: 0x04005DE6 RID: 24038
		private Transform targetTransform;

		// Token: 0x04005DE7 RID: 24039
		private GameObject prevGameObject;
	}
}
