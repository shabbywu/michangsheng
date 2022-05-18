using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x0200155D RID: 5469
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Sets the center of mass of the Rigidbody. Returns Success.")]
	public class SetCenterOfMass : Action
	{
		// Token: 0x06008172 RID: 33138 RVA: 0x002CC32C File Offset: 0x002CA52C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008173 RID: 33139 RVA: 0x0005873E File Offset: 0x0005693E
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.rigidbody.centerOfMass = this.centerOfMass.Value;
			return 2;
		}

		// Token: 0x06008174 RID: 33140 RVA: 0x00058771 File Offset: 0x00056971
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.centerOfMass = Vector3.zero;
		}

		// Token: 0x04006E2C RID: 28204
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006E2D RID: 28205
		[Tooltip("The center of mass of the Rigidbody")]
		public SharedVector3 centerOfMass;

		// Token: 0x04006E2E RID: 28206
		private Rigidbody rigidbody;

		// Token: 0x04006E2F RID: 28207
		private GameObject prevGameObject;
	}
}
