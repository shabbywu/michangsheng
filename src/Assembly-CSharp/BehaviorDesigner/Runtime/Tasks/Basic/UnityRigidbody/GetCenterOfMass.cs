using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x02001094 RID: 4244
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Stores the center of mass of the Rigidbody. Returns Success.")]
	public class GetCenterOfMass : Action
	{
		// Token: 0x0600733C RID: 29500 RVA: 0x002AF900 File Offset: 0x002ADB00
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600733D RID: 29501 RVA: 0x002AF940 File Offset: 0x002ADB40
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.storeValue.Value = this.rigidbody.centerOfMass;
			return 2;
		}

		// Token: 0x0600733E RID: 29502 RVA: 0x002AF973 File Offset: 0x002ADB73
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x04005EF2 RID: 24306
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005EF3 RID: 24307
		[Tooltip("The center of mass of the Rigidbody")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x04005EF4 RID: 24308
		private Rigidbody rigidbody;

		// Token: 0x04005EF5 RID: 24309
		private GameObject prevGameObject;
	}
}
