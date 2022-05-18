using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x02001561 RID: 5473
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Sets the is kinematic value of the Rigidbody. Returns Success.")]
	public class SetIsKinematic : Action
	{
		// Token: 0x06008182 RID: 33154 RVA: 0x002CC42C File Offset: 0x002CA62C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008183 RID: 33155 RVA: 0x0005885C File Offset: 0x00056A5C
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.rigidbody.isKinematic = this.isKinematic.Value;
			return 2;
		}

		// Token: 0x06008184 RID: 33156 RVA: 0x0005888F File Offset: 0x00056A8F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.isKinematic = false;
		}

		// Token: 0x04006E3C RID: 28220
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006E3D RID: 28221
		[Tooltip("The is kinematic value of the Rigidbody")]
		public SharedBool isKinematic;

		// Token: 0x04006E3E RID: 28222
		private Rigidbody rigidbody;

		// Token: 0x04006E3F RID: 28223
		private GameObject prevGameObject;
	}
}
