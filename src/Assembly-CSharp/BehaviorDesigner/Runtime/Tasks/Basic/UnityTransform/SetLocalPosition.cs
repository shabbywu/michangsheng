using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x02001034 RID: 4148
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Sets the local position of the Transform. Returns Success.")]
	public class SetLocalPosition : Action
	{
		// Token: 0x060071EB RID: 29163 RVA: 0x002ACEA0 File Offset: 0x002AB0A0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060071EC RID: 29164 RVA: 0x002ACEE0 File Offset: 0x002AB0E0
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

		// Token: 0x060071ED RID: 29165 RVA: 0x002ACF13 File Offset: 0x002AB113
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.localPosition = Vector3.zero;
		}

		// Token: 0x04005DCC RID: 24012
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005DCD RID: 24013
		[Tooltip("The local position of the Transform")]
		public SharedVector3 localPosition;

		// Token: 0x04005DCE RID: 24014
		private Transform targetTransform;

		// Token: 0x04005DCF RID: 24015
		private GameObject prevGameObject;
	}
}
