using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x02001095 RID: 4245
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Stores the drag of the Rigidbody. Returns Success.")]
	public class GetDrag : Action
	{
		// Token: 0x06007340 RID: 29504 RVA: 0x002AF98C File Offset: 0x002ADB8C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007341 RID: 29505 RVA: 0x002AF9CC File Offset: 0x002ADBCC
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.storeValue.Value = this.rigidbody.drag;
			return 2;
		}

		// Token: 0x06007342 RID: 29506 RVA: 0x002AF9FF File Offset: 0x002ADBFF
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04005EF6 RID: 24310
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005EF7 RID: 24311
		[Tooltip("The drag of the Rigidbody")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x04005EF8 RID: 24312
		private Rigidbody rigidbody;

		// Token: 0x04005EF9 RID: 24313
		private GameObject prevGameObject;
	}
}
