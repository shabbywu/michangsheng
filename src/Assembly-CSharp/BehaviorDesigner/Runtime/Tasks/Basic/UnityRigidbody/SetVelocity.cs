using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x02001566 RID: 5478
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Sets the velocity of the Rigidbody. Returns Success.")]
	public class SetVelocity : Action
	{
		// Token: 0x06008196 RID: 33174 RVA: 0x002CC56C File Offset: 0x002CA76C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008197 RID: 33175 RVA: 0x000589D0 File Offset: 0x00056BD0
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.rigidbody.velocity = this.velocity.Value;
			return 2;
		}

		// Token: 0x06008198 RID: 33176 RVA: 0x00058A03 File Offset: 0x00056C03
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.velocity = Vector3.zero;
		}

		// Token: 0x04006E50 RID: 28240
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006E51 RID: 28241
		[Tooltip("The velocity of the Rigidbody")]
		public SharedVector3 velocity;

		// Token: 0x04006E52 RID: 28242
		private Rigidbody rigidbody;

		// Token: 0x04006E53 RID: 28243
		private GameObject prevGameObject;
	}
}
