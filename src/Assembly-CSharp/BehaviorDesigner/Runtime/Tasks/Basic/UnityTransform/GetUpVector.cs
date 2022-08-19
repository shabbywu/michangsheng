using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x0200102C RID: 4140
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Stores the up vector of the Transform. Returns Success.")]
	public class GetUpVector : Action
	{
		// Token: 0x060071CB RID: 29131 RVA: 0x002AC990 File Offset: 0x002AAB90
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060071CC RID: 29132 RVA: 0x002AC9D0 File Offset: 0x002AABD0
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return 1;
			}
			this.storeValue.Value = this.targetTransform.up;
			return 2;
		}

		// Token: 0x060071CD RID: 29133 RVA: 0x002ACA03 File Offset: 0x002AAC03
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x04005DA7 RID: 23975
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005DA8 RID: 23976
		[Tooltip("The position of the Transform")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x04005DA9 RID: 23977
		private Transform targetTransform;

		// Token: 0x04005DAA RID: 23978
		private GameObject prevGameObject;
	}
}
