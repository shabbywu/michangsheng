using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x020014EE RID: 5358
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Sets the local position of the Transform. Returns Success.")]
	public class SetLocalPosition : Action
	{
		// Token: 0x06007FE5 RID: 32741 RVA: 0x002CAA94 File Offset: 0x002C8C94
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007FE6 RID: 32742 RVA: 0x00056D9E File Offset: 0x00054F9E
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return 1;
			}
			this.targetTransform.localPosition = this.localPosition.Value;
			return 2;
		}

		// Token: 0x06007FE7 RID: 32743 RVA: 0x00056DD1 File Offset: 0x00054FD1
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.localPosition = Vector3.zero;
		}

		// Token: 0x04006CCC RID: 27852
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006CCD RID: 27853
		[Tooltip("The local position of the Transform")]
		public SharedVector3 localPosition;

		// Token: 0x04006CCE RID: 27854
		private Transform targetTransform;

		// Token: 0x04006CCF RID: 27855
		private GameObject prevGameObject;
	}
}
