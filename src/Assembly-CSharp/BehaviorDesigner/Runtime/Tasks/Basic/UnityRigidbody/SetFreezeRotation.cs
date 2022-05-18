using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x02001560 RID: 5472
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Sets the freeze rotation value of the Rigidbody. Returns Success.")]
	public class SetFreezeRotation : Action
	{
		// Token: 0x0600817E RID: 33150 RVA: 0x002CC3EC File Offset: 0x002CA5EC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600817F RID: 33151 RVA: 0x00058814 File Offset: 0x00056A14
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.rigidbody.freezeRotation = this.freezeRotation.Value;
			return 2;
		}

		// Token: 0x06008180 RID: 33152 RVA: 0x00058847 File Offset: 0x00056A47
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.freezeRotation = false;
		}

		// Token: 0x04006E38 RID: 28216
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006E39 RID: 28217
		[Tooltip("The freeze rotation value of the Rigidbody")]
		public SharedBool freezeRotation;

		// Token: 0x04006E3A RID: 28218
		private Rigidbody rigidbody;

		// Token: 0x04006E3B RID: 28219
		private GameObject prevGameObject;
	}
}
