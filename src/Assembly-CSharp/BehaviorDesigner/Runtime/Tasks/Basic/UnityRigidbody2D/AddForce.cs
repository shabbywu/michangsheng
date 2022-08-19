using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody2D
{
	// Token: 0x02001073 RID: 4211
	[TaskCategory("Basic/Rigidbody2D")]
	[TaskDescription("Applies a force to the Rigidbody2D. Returns Success.")]
	public class AddForce : Action
	{
		// Token: 0x060072B8 RID: 29368 RVA: 0x002AE66C File Offset: 0x002AC86C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060072B9 RID: 29369 RVA: 0x002AE6AC File Offset: 0x002AC8AC
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return 1;
			}
			this.rigidbody2D.AddForce(this.force.Value);
			return 2;
		}

		// Token: 0x060072BA RID: 29370 RVA: 0x002AE6DF File Offset: 0x002AC8DF
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.force = Vector2.zero;
		}

		// Token: 0x04005E67 RID: 24167
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005E68 RID: 24168
		[Tooltip("The amount of force to apply")]
		public SharedVector2 force;

		// Token: 0x04005E69 RID: 24169
		private Rigidbody2D rigidbody2D;

		// Token: 0x04005E6A RID: 24170
		private GameObject prevGameObject;
	}
}
