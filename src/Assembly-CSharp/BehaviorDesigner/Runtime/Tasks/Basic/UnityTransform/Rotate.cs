using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x0200102F RID: 4143
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Applies a rotation. Returns Success.")]
	public class Rotate : Action
	{
		// Token: 0x060071D7 RID: 29143 RVA: 0x002ACB84 File Offset: 0x002AAD84
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060071D8 RID: 29144 RVA: 0x002ACBC4 File Offset: 0x002AADC4
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return 1;
			}
			this.targetTransform.Rotate(this.eulerAngles.Value, this.relativeTo);
			return 2;
		}

		// Token: 0x060071D9 RID: 29145 RVA: 0x002ACBFD File Offset: 0x002AADFD
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.eulerAngles = Vector3.zero;
			this.relativeTo = 1;
		}

		// Token: 0x04005DB5 RID: 23989
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005DB6 RID: 23990
		[Tooltip("Amount to rotate")]
		public SharedVector3 eulerAngles;

		// Token: 0x04005DB7 RID: 23991
		[Tooltip("Specifies which axis the rotation is relative to")]
		public Space relativeTo = 1;

		// Token: 0x04005DB8 RID: 23992
		private Transform targetTransform;

		// Token: 0x04005DB9 RID: 23993
		private GameObject prevGameObject;
	}
}
