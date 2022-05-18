using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody2D
{
	// Token: 0x0200153F RID: 5439
	[TaskCategory("Basic/Rigidbody2D")]
	[TaskDescription("Sets the drag of the Rigidbody2D. Returns Success.")]
	public class SetDrag : Action
	{
		// Token: 0x060080FA RID: 33018 RVA: 0x002CBAA4 File Offset: 0x002C9CA4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060080FB RID: 33019 RVA: 0x00057F10 File Offset: 0x00056110
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

		// Token: 0x060080FC RID: 33020 RVA: 0x00057F43 File Offset: 0x00056143
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.drag = 0f;
		}

		// Token: 0x04006DAE RID: 28078
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006DAF RID: 28079
		[Tooltip("The drag of the Rigidbody2D")]
		public SharedFloat drag;

		// Token: 0x04006DB0 RID: 28080
		private Rigidbody2D rigidbody2D;

		// Token: 0x04006DB1 RID: 28081
		private GameObject prevGameObject;
	}
}
