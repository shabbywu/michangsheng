using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x020010A5 RID: 4261
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Sets the drag of the Rigidbody. Returns Success.")]
	public class SetDrag : Action
	{
		// Token: 0x06007380 RID: 29568 RVA: 0x002B020C File Offset: 0x002AE40C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007381 RID: 29569 RVA: 0x002B024C File Offset: 0x002AE44C
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.rigidbody.drag = this.drag.Value;
			return 2;
		}

		// Token: 0x06007382 RID: 29570 RVA: 0x002B027F File Offset: 0x002AE47F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.drag = 0f;
		}

		// Token: 0x04005F34 RID: 24372
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005F35 RID: 24373
		[Tooltip("The drag of the Rigidbody")]
		public SharedFloat drag;

		// Token: 0x04005F36 RID: 24374
		private Rigidbody rigidbody;

		// Token: 0x04005F37 RID: 24375
		private GameObject prevGameObject;
	}
}
