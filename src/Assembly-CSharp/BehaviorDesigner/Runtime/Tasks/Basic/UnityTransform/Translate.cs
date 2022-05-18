using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x020014F6 RID: 5366
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Moves the transform in the direction and distance of translation. Returns Success.")]
	public class Translate : Action
	{
		// Token: 0x06008005 RID: 32773 RVA: 0x002CAC94 File Offset: 0x002C8E94
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008006 RID: 32774 RVA: 0x00056FF5 File Offset: 0x000551F5
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

		// Token: 0x06008007 RID: 32775 RVA: 0x0005702E File Offset: 0x0005522E
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.translation = Vector3.zero;
			this.relativeTo = 1;
		}

		// Token: 0x04006CEC RID: 27884
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006CED RID: 27885
		[Tooltip("Move direction and distance")]
		public SharedVector3 translation;

		// Token: 0x04006CEE RID: 27886
		[Tooltip("Specifies which axis the rotation is relative to")]
		public Space relativeTo = 1;

		// Token: 0x04006CEF RID: 27887
		private Transform targetTransform;

		// Token: 0x04006CF0 RID: 27888
		private GameObject prevGameObject;
	}
}
