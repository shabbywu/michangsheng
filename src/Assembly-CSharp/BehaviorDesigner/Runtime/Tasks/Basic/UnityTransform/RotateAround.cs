using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x02001030 RID: 4144
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Applies a rotation. Returns Success.")]
	public class RotateAround : Action
	{
		// Token: 0x060071DB RID: 29147 RVA: 0x002ACC2C File Offset: 0x002AAE2C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060071DC RID: 29148 RVA: 0x002ACC6C File Offset: 0x002AAE6C
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return 1;
			}
			this.targetTransform.RotateAround(this.point.Value, this.axis.Value, this.angle.Value);
			return 2;
		}

		// Token: 0x060071DD RID: 29149 RVA: 0x002ACCC0 File Offset: 0x002AAEC0
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.point = Vector3.zero;
			this.axis = Vector3.zero;
			this.angle = 0f;
		}

		// Token: 0x04005DBA RID: 23994
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005DBB RID: 23995
		[Tooltip("Point to rotate around")]
		public SharedVector3 point;

		// Token: 0x04005DBC RID: 23996
		[Tooltip("Axis to rotate around")]
		public SharedVector3 axis;

		// Token: 0x04005DBD RID: 23997
		[Tooltip("Amount to rotate")]
		public SharedFloat angle;

		// Token: 0x04005DBE RID: 23998
		private Transform targetTransform;

		// Token: 0x04005DBF RID: 23999
		private GameObject prevGameObject;
	}
}
