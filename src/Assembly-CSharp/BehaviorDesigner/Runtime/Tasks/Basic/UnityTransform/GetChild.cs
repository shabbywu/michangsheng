using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x020014DA RID: 5338
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Stores the transform child at the specified index. Returns Success.")]
	public class GetChild : Action
	{
		// Token: 0x06007F95 RID: 32661 RVA: 0x002CA4CC File Offset: 0x002C86CC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007F96 RID: 32662 RVA: 0x000567CA File Offset: 0x000549CA
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return 1;
			}
			this.storeValue.Value = this.targetTransform.GetChild(this.index.Value);
			return 2;
		}

		// Token: 0x06007F97 RID: 32663 RVA: 0x00056808 File Offset: 0x00054A08
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.index = 0;
			this.storeValue = null;
		}

		// Token: 0x04006C76 RID: 27766
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006C77 RID: 27767
		[Tooltip("The index of the child")]
		public SharedInt index;

		// Token: 0x04006C78 RID: 27768
		[Tooltip("The child of the Transform")]
		[RequiredField]
		public SharedTransform storeValue;

		// Token: 0x04006C79 RID: 27769
		private Transform targetTransform;

		// Token: 0x04006C7A RID: 27770
		private GameObject prevGameObject;
	}
}
