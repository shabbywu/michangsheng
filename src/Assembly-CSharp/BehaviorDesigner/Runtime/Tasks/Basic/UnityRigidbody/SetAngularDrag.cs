using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x0200155B RID: 5467
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Sets the angular drag of the Rigidbody. Returns Success.")]
	public class SetAngularDrag : Action
	{
		// Token: 0x0600816A RID: 33130 RVA: 0x002CC2AC File Offset: 0x002CA4AC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600816B RID: 33131 RVA: 0x000586A6 File Offset: 0x000568A6
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.rigidbody.angularDrag = this.angularDrag.Value;
			return 2;
		}

		// Token: 0x0600816C RID: 33132 RVA: 0x000586D9 File Offset: 0x000568D9
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.angularDrag = 0f;
		}

		// Token: 0x04006E24 RID: 28196
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006E25 RID: 28197
		[Tooltip("The angular drag of the Rigidbody")]
		public SharedFloat angularDrag;

		// Token: 0x04006E26 RID: 28198
		private Rigidbody rigidbody;

		// Token: 0x04006E27 RID: 28199
		private GameObject prevGameObject;
	}
}
