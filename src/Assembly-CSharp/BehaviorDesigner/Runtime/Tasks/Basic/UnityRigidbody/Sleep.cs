using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x02001567 RID: 5479
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Forces the Rigidbody to sleep at least one frame. Returns Success.")]
	public class Sleep : Conditional
	{
		// Token: 0x0600819A RID: 33178 RVA: 0x002CC5AC File Offset: 0x002CA7AC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600819B RID: 33179 RVA: 0x00058A1C File Offset: 0x00056C1C
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.rigidbody.Sleep();
			return 2;
		}

		// Token: 0x0600819C RID: 33180 RVA: 0x00058A44 File Offset: 0x00056C44
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04006E54 RID: 28244
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006E55 RID: 28245
		private Rigidbody rigidbody;

		// Token: 0x04006E56 RID: 28246
		private GameObject prevGameObject;
	}
}
