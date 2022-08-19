using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x02001099 RID: 4249
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Stores the position of the Rigidbody. Returns Success.")]
	public class GetPosition : Action
	{
		// Token: 0x06007350 RID: 29520 RVA: 0x002AFBB4 File Offset: 0x002ADDB4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007351 RID: 29521 RVA: 0x002AFBF4 File Offset: 0x002ADDF4
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.storeValue.Value = this.rigidbody.position;
			return 2;
		}

		// Token: 0x06007352 RID: 29522 RVA: 0x002AFC27 File Offset: 0x002ADE27
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x04005F06 RID: 24326
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005F07 RID: 24327
		[Tooltip("The position of the Rigidbody")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x04005F08 RID: 24328
		private Rigidbody rigidbody;

		// Token: 0x04005F09 RID: 24329
		private GameObject prevGameObject;
	}
}
