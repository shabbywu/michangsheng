using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody2D
{
	// Token: 0x02001085 RID: 4229
	[TaskCategory("Basic/Rigidbody2D")]
	[TaskDescription("Sets the drag of the Rigidbody2D. Returns Success.")]
	public class SetDrag : Action
	{
		// Token: 0x06007300 RID: 29440 RVA: 0x002AF034 File Offset: 0x002AD234
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007301 RID: 29441 RVA: 0x002AF074 File Offset: 0x002AD274
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return 1;
			}
			this.rigidbody2D.drag = this.drag.Value;
			return 2;
		}

		// Token: 0x06007302 RID: 29442 RVA: 0x002AF0A7 File Offset: 0x002AD2A7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.drag = 0f;
		}

		// Token: 0x04005EAE RID: 24238
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005EAF RID: 24239
		[Tooltip("The drag of the Rigidbody2D")]
		public SharedFloat drag;

		// Token: 0x04005EB0 RID: 24240
		private Rigidbody2D rigidbody2D;

		// Token: 0x04005EB1 RID: 24241
		private GameObject prevGameObject;
	}
}
