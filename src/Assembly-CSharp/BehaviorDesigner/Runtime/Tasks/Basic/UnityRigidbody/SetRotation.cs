using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x02001564 RID: 5476
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Stores the rotation of the Rigidbody. Returns Success.")]
	public class SetRotation : Action
	{
		// Token: 0x0600818E RID: 33166 RVA: 0x002CC4EC File Offset: 0x002CA6EC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600818F RID: 33167 RVA: 0x0005893C File Offset: 0x00056B3C
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.rigidbody.rotation = this.rotation.Value;
			return 2;
		}

		// Token: 0x06008190 RID: 33168 RVA: 0x0005896F File Offset: 0x00056B6F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.rotation = Quaternion.identity;
		}

		// Token: 0x04006E48 RID: 28232
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006E49 RID: 28233
		[Tooltip("The rotation of the Rigidbody")]
		public SharedQuaternion rotation;

		// Token: 0x04006E4A RID: 28234
		private Rigidbody rigidbody;

		// Token: 0x04006E4B RID: 28235
		private GameObject prevGameObject;
	}
}
