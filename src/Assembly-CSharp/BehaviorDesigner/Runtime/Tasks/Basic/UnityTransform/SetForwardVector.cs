using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x020014EC RID: 5356
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Sets the forward vector of the Transform. Returns Success.")]
	public class SetForwardVector : Action
	{
		// Token: 0x06007FDD RID: 32733 RVA: 0x002CAA14 File Offset: 0x002C8C14
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007FDE RID: 32734 RVA: 0x00056D06 File Offset: 0x00054F06
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return 1;
			}
			this.targetTransform.forward = this.position.Value;
			return 2;
		}

		// Token: 0x06007FDF RID: 32735 RVA: 0x00056D39 File Offset: 0x00054F39
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.position = Vector3.zero;
		}

		// Token: 0x04006CC4 RID: 27844
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006CC5 RID: 27845
		[Tooltip("The position of the Transform")]
		public SharedVector3 position;

		// Token: 0x04006CC6 RID: 27846
		private Transform targetTransform;

		// Token: 0x04006CC7 RID: 27847
		private GameObject prevGameObject;
	}
}
