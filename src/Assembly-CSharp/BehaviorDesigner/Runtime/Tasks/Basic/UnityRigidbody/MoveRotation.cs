using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x0200155A RID: 5466
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Rotates the Rigidbody to the specified rotation. Returns Success.")]
	public class MoveRotation : Action
	{
		// Token: 0x06008166 RID: 33126 RVA: 0x002CC26C File Offset: 0x002CA46C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008167 RID: 33127 RVA: 0x0005865A File Offset: 0x0005685A
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.rigidbody.MoveRotation(this.rotation.Value);
			return 2;
		}

		// Token: 0x06008168 RID: 33128 RVA: 0x0005868D File Offset: 0x0005688D
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.rotation = Quaternion.identity;
		}

		// Token: 0x04006E20 RID: 28192
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006E21 RID: 28193
		[Tooltip("The new rotation of the Rigidbody")]
		public SharedQuaternion rotation;

		// Token: 0x04006E22 RID: 28194
		private Rigidbody rigidbody;

		// Token: 0x04006E23 RID: 28195
		private GameObject prevGameObject;
	}
}
