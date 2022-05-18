using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x020014ED RID: 5357
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Sets the local euler angles of the Transform. Returns Success.")]
	public class SetLocalEulerAngles : Action
	{
		// Token: 0x06007FE1 RID: 32737 RVA: 0x002CAA54 File Offset: 0x002C8C54
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007FE2 RID: 32738 RVA: 0x00056D52 File Offset: 0x00054F52
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return 1;
			}
			this.targetTransform.localEulerAngles = this.localEulerAngles.Value;
			return 2;
		}

		// Token: 0x06007FE3 RID: 32739 RVA: 0x00056D85 File Offset: 0x00054F85
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.localEulerAngles = Vector3.zero;
		}

		// Token: 0x04006CC8 RID: 27848
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006CC9 RID: 27849
		[Tooltip("The local euler angles of the Transform")]
		public SharedVector3 localEulerAngles;

		// Token: 0x04006CCA RID: 27850
		private Transform targetTransform;

		// Token: 0x04006CCB RID: 27851
		private GameObject prevGameObject;
	}
}
