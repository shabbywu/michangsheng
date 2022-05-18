using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x020014DB RID: 5339
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Stores the number of children a Transform has. Returns Success.")]
	public class GetChildCount : Action
	{
		// Token: 0x06007F99 RID: 32665 RVA: 0x002CA50C File Offset: 0x002C870C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007F9A RID: 32666 RVA: 0x00056824 File Offset: 0x00054A24
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return 1;
			}
			this.storeValue.Value = this.targetTransform.childCount;
			return 2;
		}

		// Token: 0x06007F9B RID: 32667 RVA: 0x00056857 File Offset: 0x00054A57
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0;
		}

		// Token: 0x04006C7B RID: 27771
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006C7C RID: 27772
		[Tooltip("The number of children")]
		[RequiredField]
		public SharedInt storeValue;

		// Token: 0x04006C7D RID: 27773
		private Transform targetTransform;

		// Token: 0x04006C7E RID: 27774
		private GameObject prevGameObject;
	}
}
