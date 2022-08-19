using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x02001036 RID: 4150
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Sets the local scale of the Transform. Returns Success.")]
	public class SetLocalScale : Action
	{
		// Token: 0x060071F3 RID: 29171 RVA: 0x002ACFB8 File Offset: 0x002AB1B8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060071F4 RID: 29172 RVA: 0x002ACFF8 File Offset: 0x002AB1F8
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return 1;
			}
			this.targetTransform.localScale = this.localScale.Value;
			return 2;
		}

		// Token: 0x060071F5 RID: 29173 RVA: 0x002AD02B File Offset: 0x002AB22B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.localScale = Vector3.zero;
		}

		// Token: 0x04005DD4 RID: 24020
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005DD5 RID: 24021
		[Tooltip("The local scale of the Transform")]
		public SharedVector3 localScale;

		// Token: 0x04005DD6 RID: 24022
		private Transform targetTransform;

		// Token: 0x04005DD7 RID: 24023
		private GameObject prevGameObject;
	}
}
