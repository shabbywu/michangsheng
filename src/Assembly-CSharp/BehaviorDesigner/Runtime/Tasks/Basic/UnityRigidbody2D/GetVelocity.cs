using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody2D
{
	// Token: 0x0200107E RID: 4222
	[TaskCategory("Basic/Rigidbody2D")]
	[TaskDescription("Stores the velocity of the Rigidbody2D. Returns Success.")]
	public class GetVelocity : Action
	{
		// Token: 0x060072E4 RID: 29412 RVA: 0x002AEC88 File Offset: 0x002ACE88
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060072E5 RID: 29413 RVA: 0x002AECC8 File Offset: 0x002ACEC8
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return 1;
			}
			this.storeValue.Value = this.rigidbody2D.velocity;
			return 2;
		}

		// Token: 0x060072E6 RID: 29414 RVA: 0x002AECFB File Offset: 0x002ACEFB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector2.zero;
		}

		// Token: 0x04005E94 RID: 24212
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005E95 RID: 24213
		[Tooltip("The velocity of the Rigidbody2D")]
		[RequiredField]
		public SharedVector2 storeValue;

		// Token: 0x04005E96 RID: 24214
		private Rigidbody2D rigidbody2D;

		// Token: 0x04005E97 RID: 24215
		private GameObject prevGameObject;
	}
}
