using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x0200103B RID: 4155
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Sets the up vector of the Transform. Returns Success.")]
	public class SetUpVector : Action
	{
		// Token: 0x06007207 RID: 29191 RVA: 0x002AD26C File Offset: 0x002AB46C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007208 RID: 29192 RVA: 0x002AD2AC File Offset: 0x002AB4AC
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return 1;
			}
			this.targetTransform.up = this.position.Value;
			return 2;
		}

		// Token: 0x06007209 RID: 29193 RVA: 0x002AD2DF File Offset: 0x002AB4DF
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.position = Vector3.zero;
		}

		// Token: 0x04005DE8 RID: 24040
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005DE9 RID: 24041
		[Tooltip("The position of the Transform")]
		public SharedVector3 position;

		// Token: 0x04005DEA RID: 24042
		private Transform targetTransform;

		// Token: 0x04005DEB RID: 24043
		private GameObject prevGameObject;
	}
}
