using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody2D
{
	// Token: 0x0200153C RID: 5436
	[TaskCategory("Basic/Rigidbody2D")]
	[TaskDescription("Rotates the Rigidbody2D to the specified rotation. Returns Success.")]
	public class MoveRotation : Action
	{
		// Token: 0x060080EE RID: 33006 RVA: 0x002CB9E4 File Offset: 0x002C9BE4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060080EF RID: 33007 RVA: 0x00057E2C File Offset: 0x0005602C
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.rigidbody2D.MoveRotation(this.rotation.Value);
			return 2;
		}

		// Token: 0x060080F0 RID: 33008 RVA: 0x00057E5F File Offset: 0x0005605F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.rotation = 0f;
		}

		// Token: 0x04006DA2 RID: 28066
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006DA3 RID: 28067
		[Tooltip("The new rotation of the Rigidbody")]
		public SharedFloat rotation;

		// Token: 0x04006DA4 RID: 28068
		private Rigidbody2D rigidbody2D;

		// Token: 0x04006DA5 RID: 28069
		private GameObject prevGameObject;
	}
}
