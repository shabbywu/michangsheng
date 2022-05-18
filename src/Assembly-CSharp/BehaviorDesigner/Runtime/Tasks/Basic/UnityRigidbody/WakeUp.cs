using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x02001569 RID: 5481
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Forces the Rigidbody to wake up. Returns Success.")]
	public class WakeUp : Conditional
	{
		// Token: 0x060081A2 RID: 33186 RVA: 0x002CC62C File Offset: 0x002CA82C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060081A3 RID: 33187 RVA: 0x00058A82 File Offset: 0x00056C82
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.rigidbody.WakeUp();
			return 2;
		}

		// Token: 0x060081A4 RID: 33188 RVA: 0x00058AAA File Offset: 0x00056CAA
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04006E5A RID: 28250
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006E5B RID: 28251
		private Rigidbody rigidbody;

		// Token: 0x04006E5C RID: 28252
		private GameObject prevGameObject;
	}
}
