using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x02001562 RID: 5474
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Sets the mass of the Rigidbody. Returns Success.")]
	public class SetMass : Action
	{
		// Token: 0x06008186 RID: 33158 RVA: 0x002CC46C File Offset: 0x002CA66C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008187 RID: 33159 RVA: 0x000588A4 File Offset: 0x00056AA4
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.rigidbody.mass = this.mass.Value;
			return 2;
		}

		// Token: 0x06008188 RID: 33160 RVA: 0x000588D7 File Offset: 0x00056AD7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.mass = 0f;
		}

		// Token: 0x04006E40 RID: 28224
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006E41 RID: 28225
		[Tooltip("The mass of the Rigidbody")]
		public SharedFloat mass;

		// Token: 0x04006E42 RID: 28226
		private Rigidbody rigidbody;

		// Token: 0x04006E43 RID: 28227
		private GameObject prevGameObject;
	}
}
