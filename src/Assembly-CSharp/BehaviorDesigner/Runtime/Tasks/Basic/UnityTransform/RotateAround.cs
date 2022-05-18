using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x020014EA RID: 5354
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Applies a rotation. Returns Success.")]
	public class RotateAround : Action
	{
		// Token: 0x06007FD5 RID: 32725 RVA: 0x002CA940 File Offset: 0x002C8B40
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007FD6 RID: 32726 RVA: 0x002CA980 File Offset: 0x002C8B80
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

		// Token: 0x06007FD7 RID: 32727 RVA: 0x00056C81 File Offset: 0x00054E81
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.point = Vector3.zero;
			this.axis = Vector3.zero;
			this.angle = 0f;
		}

		// Token: 0x04006CBA RID: 27834
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006CBB RID: 27835
		[Tooltip("Point to rotate around")]
		public SharedVector3 point;

		// Token: 0x04006CBC RID: 27836
		[Tooltip("Axis to rotate around")]
		public SharedVector3 axis;

		// Token: 0x04006CBD RID: 27837
		[Tooltip("Amount to rotate")]
		public SharedFloat angle;

		// Token: 0x04006CBE RID: 27838
		private Transform targetTransform;

		// Token: 0x04006CBF RID: 27839
		private GameObject prevGameObject;
	}
}
