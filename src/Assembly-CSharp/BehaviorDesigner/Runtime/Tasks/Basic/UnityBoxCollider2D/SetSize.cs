using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityBoxCollider2D
{
	// Token: 0x0200115D RID: 4445
	[TaskCategory("Basic/BoxCollider2D")]
	[TaskDescription("Sets the size of the BoxCollider2D. Returns Success.")]
	public class SetSize : Action
	{
		// Token: 0x0600760B RID: 30219 RVA: 0x002B5AC0 File Offset: 0x002B3CC0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.boxCollider2D = defaultGameObject.GetComponent<BoxCollider2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600760C RID: 30220 RVA: 0x002B5B00 File Offset: 0x002B3D00
		public override TaskStatus OnUpdate()
		{
			if (this.boxCollider2D == null)
			{
				Debug.LogWarning("BoxCollider2D is null");
				return 1;
			}
			this.boxCollider2D.size = this.size.Value;
			return 2;
		}

		// Token: 0x0600760D RID: 30221 RVA: 0x002B5B33 File Offset: 0x002B3D33
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.size = Vector2.zero;
		}

		// Token: 0x04006191 RID: 24977
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006192 RID: 24978
		[Tooltip("The size of the BoxCollider2D")]
		public SharedVector2 size;

		// Token: 0x04006193 RID: 24979
		private BoxCollider2D boxCollider2D;

		// Token: 0x04006194 RID: 24980
		private GameObject prevGameObject;
	}
}
