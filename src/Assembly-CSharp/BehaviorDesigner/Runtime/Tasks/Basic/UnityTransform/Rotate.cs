using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x020014E9 RID: 5353
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Applies a rotation. Returns Success.")]
	public class Rotate : Action
	{
		// Token: 0x06007FD1 RID: 32721 RVA: 0x002CA900 File Offset: 0x002C8B00
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007FD2 RID: 32722 RVA: 0x00056C19 File Offset: 0x00054E19
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

		// Token: 0x06007FD3 RID: 32723 RVA: 0x00056C52 File Offset: 0x00054E52
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.eulerAngles = Vector3.zero;
			this.relativeTo = 1;
		}

		// Token: 0x04006CB5 RID: 27829
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006CB6 RID: 27830
		[Tooltip("Amount to rotate")]
		public SharedVector3 eulerAngles;

		// Token: 0x04006CB7 RID: 27831
		[Tooltip("Specifies which axis the rotation is relative to")]
		public Space relativeTo = 1;

		// Token: 0x04006CB8 RID: 27832
		private Transform targetTransform;

		// Token: 0x04006CB9 RID: 27833
		private GameObject prevGameObject;
	}
}
