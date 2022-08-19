using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x0200103C RID: 4156
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Moves the transform in the direction and distance of translation. Returns Success.")]
	public class Translate : Action
	{
		// Token: 0x0600720B RID: 29195 RVA: 0x002AD2F8 File Offset: 0x002AB4F8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600720C RID: 29196 RVA: 0x002AD338 File Offset: 0x002AB538
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return 1;
			}
			this.targetTransform.Translate(this.translation.Value, this.relativeTo);
			return 2;
		}

		// Token: 0x0600720D RID: 29197 RVA: 0x002AD371 File Offset: 0x002AB571
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.translation = Vector3.zero;
			this.relativeTo = 1;
		}

		// Token: 0x04005DEC RID: 24044
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005DED RID: 24045
		[Tooltip("Move direction and distance")]
		public SharedVector3 translation;

		// Token: 0x04005DEE RID: 24046
		[Tooltip("Specifies which axis the rotation is relative to")]
		public Space relativeTo = 1;

		// Token: 0x04005DEF RID: 24047
		private Transform targetTransform;

		// Token: 0x04005DF0 RID: 24048
		private GameObject prevGameObject;
	}
}
