using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody2D
{
	// Token: 0x02001544 RID: 5444
	[TaskCategory("Basic/Rigidbody2D")]
	[TaskDescription("Forces the Rigidbody2D to sleep at least one frame. Returns Success.")]
	public class Sleep : Conditional
	{
		// Token: 0x0600810E RID: 33038 RVA: 0x002CBBE4 File Offset: 0x002C9DE4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600810F RID: 33039 RVA: 0x00058088 File Offset: 0x00056288
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return 1;
			}
			this.rigidbody2D.Sleep();
			return 2;
		}

		// Token: 0x06008110 RID: 33040 RVA: 0x000580B0 File Offset: 0x000562B0
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04006DC2 RID: 28098
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006DC3 RID: 28099
		private Rigidbody2D rigidbody2D;

		// Token: 0x04006DC4 RID: 28100
		private GameObject prevGameObject;
	}
}
